using UnityEngine;
using System.Collections;

public class UserInput : MonoBehaviour {

	public CNJoystick movementJoystick, rotationJoystick;
	public FireButton fireButton;
	public float RotationSpeed = 10f;

	public TankModel tank;
	// Use this for initialization
	void Start () {
		//player = GetComponentInParent <Player> ();
		//joystick = transform.root.GetComponent <CNJoystick> ();
		movementJoystick = GameObject.Find ("MovementJoystick").GetComponent<CNJoystick>();
		movementJoystick.SnapsToFinger = true;
		rotationJoystick = GameObject.Find ("RotationJoystick").GetComponent<CNJoystick> ();
		rotationJoystick.SnapsToFinger = true;

		movementJoystick.SnapsToFinger = false;
		rotationJoystick.SnapsToFinger = false;

		fireButton = GameObject.Find ("FireButton").GetComponent<FireButton> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (tank.networkView.isMine) {
			if(Input.GetKeyDown(KeyCode.Escape)){
				Application.Quit();
			}
			GetMouseRotationInput ();
			GetBodyMovementInput ();
			GetRotationInput ();
			GetMouseClick ();
		}
	}

	private void GetMouseRotationInput () {
//		try {
//			if (FindHitObject () != null) {
//				Vector3 hitPoint = FindHitPoint ();
//				tank.turret.StartRotate (hitPoint);
//			}
//		} catch (UnityException ex) {
//			
//		}	

		tank.turret.StartRotate(rotationJoystick.GetAxis("Horizontal"));
	}

//	private GameObject FindHitObject() {
//		Ray ray = tank.turret.camera.camera.ScreenPointToRay(Input.mousePosition);
//		RaycastHit hit;
//		if(Physics.Raycast(ray, out hit)) return hit.collider.gameObject;
//		throw new UnityException();
//	}
//	
//	private Vector3 FindHitPoint() {
//		Ray ray = tank.turret.camera.camera.ScreenPointToRay(Input.mousePosition);
//		RaycastHit hit;
//		if(Physics.Raycast(ray, out hit)) return hit.point;
//		return new Vector3(-999, -999, -999);
//	}

	private void GetBodyMovementInput () {
		//float movement = Input.GetAxis ("Vertical");
		float movement = movementJoystick.GetAxis("Vertical");
		tank.startMoving (movement);
	}
	
	private void GetRotationInput () {
		//float rotation = Input.GetAxis ("Horizontal");
		float rotation = movementJoystick.GetAxis("Horizontal");
		tank.startRotating (rotation);
	}

	private void GetMouseClick () {
		if (fireButton.isFired) {
			tank.turret.fireProjectile(tank.playerManager.myId);
		}
	}
	

}
