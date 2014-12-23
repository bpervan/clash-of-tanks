using UnityEngine;
using System.Collections;

public class TankTurret : MonoBehaviour {

	private Vector3 destination;
	//private Quaternion targetRotation;
	private float rotation;
	private float shootingAngle, modifier, weaponRechargeTime, currentWeaponRechargeTime;
	public float allowedRotationZ, allowedRotationXmax, allowedRotationXmin, rotationSpeed;
	private bool rotating;
	public GameObject[] projectiles;
	public int[] projectileNumber;
	public GameObject camera;

	// Use this for initialization
	void Start () {
		rotationSpeed = 20f;
		shootingAngle = 25;
		weaponRechargeTime = 1.0f;
		currentWeaponRechargeTime = weaponRechargeTime;
		rotating = false;
		allowedRotationZ = 0;
		allowedRotationXmax = 10;
		allowedRotationXmin = 0; // -10 degrees
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (rotating)
			TurnToTarget ();
		if (currentWeaponRechargeTime < weaponRechargeTime)
			currentWeaponRechargeTime += Time.deltaTime;
	}

	public void StartRotate (float rotation) {
		/*this.destination = destination;
		targetRotation = Quaternion.LookRotation (destination - transform.position);
		targetRotation.eulerAngles = new Vector3 (
			Mathf.Clamp (targetRotation.eulerAngles.x, allowedRotationXmin, allowedRotationXmin), 
			targetRotation.eulerAngles.y, 
			Mathf.Clamp (targetRotation.eulerAngles.z, allowedRotationXmin, allowedRotationXmin));
	
		checkRotationFinished ();

		*/

		if (rotation != 0f) {
			rotating = true;
			this.rotation = rotation;
		} else {
			rotating = false;
		}
	}

	void TurnToTarget () {
//		Quaternion rotation;
//		rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed*Time.deltaTime);
////		rotation.z = 0;
////
////		if (rotation.x > allowedRotationXmax && rotation.x < allowedRotationXmin) {
////			rotation.x = allowedRotationXmax;
////		} else if (rotation.x > allowedRotationXmin) {
////			rotation.x = allowedRotationXmin;
////		}
//
//		transform.rotation = rotation;
//		checkRotationFinished ();

		transform.RotateAround (transform.TransformPoint(Vector3.zero), transform.TransformDirection(Vector3.up), rotation * Time.deltaTime * rotationSpeed);

	}

//	void checkRotationFinished () {
//		if (Mathf.Abs (Quaternion.Angle (transform.rotation, targetRotation)) < shootingAngle) {
//			rotating = false;
//		} else {
//			rotating = true;
//		}
//	}

	public void fireProjectile (int owner) {
		if (currentWeaponRechargeTime >= weaponRechargeTime && projectileNumber[0] > 0) {
			Vector3 spawnPoint = transform.position;
			spawnPoint.y += 1.75f;
			//spawnPoint.z += 3f * transform.position.z;


			GameObject gameObject = (GameObject)Network.Instantiate (projectiles[0], spawnPoint, transform.rotation, 0);
			gameObject.transform.Rotate (new Vector3 (-2, 0, 0));
			Projectile projectile = gameObject.GetComponentInChildren <Projectile> ();
			projectile.networkView.RPC ("setOwner", RPCMode.OthersBuffered, owner);
			projectile.owner = owner;
			projectile.fire ();
			currentWeaponRechargeTime = 0;
			projectileNumber[0]--;
		}
	}

	public void incAmmo (int ammo) {
		projectileNumber [0] += ammo;
	}


}
