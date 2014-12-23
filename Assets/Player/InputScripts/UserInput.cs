using UnityEngine;
using System.Collections;

public class UserInput : MonoBehaviour {

	public CNJoystick movementJoystick, rotationJoystick;
	public FireButton fireButton;
	public float RotationSpeed = 10f;

	public TankModel tank;
	void Start () {
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
		tank.turret.StartRotate(rotationJoystick.GetAxis("Horizontal"));
	}

	private void GetBodyMovementInput () {
		float movement = movementJoystick.GetAxis("Vertical");
		tank.startMoving (movement);
	}
	
	private void GetRotationInput () {
		float rotation = movementJoystick.GetAxis("Horizontal");
		tank.startRotating (rotation);
	}

	private void GetMouseClick () {
		if (fireButton.isFired) {
			tank.turret.fireProjectile(tank.playerManager.myId);
		}
	}
	

}
