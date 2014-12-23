using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FireButton : CNAbstractController {

	public bool isFired = false;

	[SerializeField]
	[HideInInspector]
	private float _dragRadius = 1.5f;
	public float DragRadius { get { return _dragRadius; } set { _dragRadius = value; } }

	private bool isHiddenIfNotTweaking;
	private Transform baseTransform;
	private GameObject baseGameObject;

	public override void OnEnable(){
		base.OnEnable ();
		base.Anchor = Anchors.RightTop;
        this.isHiddenIfNotTweaking = false;

		baseTransform = TransformCache.FindChild ("Base");
		baseGameObject = baseTransform.gameObject;
		if (isHiddenIfNotTweaking) {
			baseGameObject.gameObject.SetActive (false);
		} else {
			baseGameObject.gameObject.SetActive(true);		
		}
	}

	protected override void ResetControlState()
	{
		base.ResetControlState();
		// Setting the stick and base local positions back to local zero
		baseTransform.localPosition = Vector3.zero;
	}

	protected override void OnFingerLifted()
	{
		base.OnFingerLifted();
		if (!isHiddenIfNotTweaking) return;
		baseGameObject.gameObject.SetActive(false);
	}

	protected override void OnFingerTouched()
	{
		base.OnFingerTouched();
		if (!isHiddenIfNotTweaking) return;
		baseGameObject.gameObject.SetActive(true);
	}

	// Update is called once per frame
	protected virtual void Update () {
		// Check for touches
		if (TweakIfNeeded())
			return;
		
		Touch currentTouch;
		if (IsTouchCaptured (out currentTouch)) {
			isFired = true;
		} else {
			isFired = false;
		}
	}

	protected override void TweakControl(Vector2 touchPosition){
		Vector3 worldTouchPosition = ParentCamera.ScreenToWorldPoint(touchPosition);
		
		Vector3 differenceVector = (worldTouchPosition - baseTransform.position);
		// If we're out of the drag range
		if (differenceVector.sqrMagnitude >
		    DragRadius * DragRadius)
		{
			differenceVector.Normalize();
		}
		
		// Store calculated axis values to our private variable
		CurrentAxisValues = differenceVector;
		
		// We also fire our event if there are subscribers
		OnControllerMoved(differenceVector);
	}
}
