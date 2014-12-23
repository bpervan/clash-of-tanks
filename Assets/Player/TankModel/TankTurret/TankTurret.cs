using UnityEngine;
using System.Collections;

public class TankTurret : MonoBehaviour {

	private Vector3 destination;
	private float rotation;
	private float modifier, weaponRechargeTime, currentWeaponRechargeTime;
	public float rotationSpeed;
	private bool rotating;
	public GameObject[] projectiles;
	public int[] projectileNumber;
	public GameObject cameraPlayer;

	// Use this for initialization
	void Start () {
		rotationSpeed = 20f;
		weaponRechargeTime = 1.0f;
		currentWeaponRechargeTime = weaponRechargeTime;
		rotating = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (rotating)
			TurnToTarget ();
		if (currentWeaponRechargeTime < weaponRechargeTime)
			currentWeaponRechargeTime += Time.deltaTime;
	}

	public void StartRotate (float rotation) {

		if (rotation != 0f) {
			rotating = true;
			this.rotation = rotation;
		} else {
			rotating = false;
		}
	}

	void TurnToTarget () {

		transform.RotateAround (transform.TransformPoint(Vector3.zero), transform.TransformDirection(Vector3.up), rotation * Time.deltaTime * rotationSpeed);

	}

	public void fireProjectile (int owner) {
		if (currentWeaponRechargeTime >= weaponRechargeTime && projectileNumber[0] > 0) {
			Vector3 spawnPoint = transform.position;
			spawnPoint.y += 1.75f;

			GameObject gameObject = (GameObject)Network.Instantiate (projectiles[0], spawnPoint, transform.rotation, 0);
			gameObject.transform.Rotate (new Vector3 (-2, 0, 0));
			Projectile projectile = gameObject.GetComponentInChildren <Projectile> ();
			//projectile.networkView.RPC ("setOwner", RPCMode.AllBuffered, owner);
            projectile.setOwnerLocal(owner);
            //projectile.owner = owner;
			projectile.fire ();
			currentWeaponRechargeTime = 0;
			projectileNumber[0]--;
		}
	}

	public void incAmmo (int ammo) {
		projectileNumber [0] += ammo;
	}


}
