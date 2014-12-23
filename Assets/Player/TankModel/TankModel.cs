using UnityEngine;
using System.Collections;

public class TankModel : MonoBehaviour {

	public TankBody body;
	public TankTurret turret;
	private bool moving, rotating;
	private volatile bool colliding;
	public float rotation, movement;
	public PlayerManager playerManager;

	public ITank tankData;

	void Start () {
		moving = false;
		rotating = false;
		colliding = false;

		tankData = new ImplementedTank ();
		turret.rotationSpeed = tankData.getTurretRotation ();
		playerManager = GameObject.Find ("PlayerManager").GetComponent<PlayerManager> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (moving)
			move ();
		if (rotating)
			rotateBody ();

	}

	private void move () {
		rigidbody.MovePosition (body.transform.position + body.transform.TransformDirection(0, 0, movement) * body.movementSpeed * Time.deltaTime);

	}

	private void rotateBody () {
		Vector3 angleVelocity = new Vector3 (0, rotation * body.bodyRotationSpeed, 0);
		Quaternion deltaRotation = Quaternion.Euler(angleVelocity* Time.deltaTime);
		rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
	}

	public void startMoving (float movement) {
		if (movement != 0) {
			moving = true;
			this.movement = movement;
		} else {
			moving = false;
		}
	}

	public void startRotating (float rotation) {
		if (rotation != 0) {
			this.rotation = rotation;
			rotating = true;
		} else {
			rotating = false;
		}
	}

	void OnTriggerEnter (Collider other) {

		if (!colliding && networkView.isMine && !other.gameObject.name.Equals ("Terrain")) {
			colliding = true;
			Debug.Log ("Collider name: " + other.gameObject.name);

			if (other.gameObject.GetComponent<Projectile> () != null
					&& other.gameObject.GetComponent<Projectile> ().getOwner() != this.playerManager.myId)
			    	//&& other.gameObject.GetComponent<Projectile> ().owner != 0) {
            {Debug.Log ("Projectile owner: " + other.gameObject.GetComponent<Projectile> ().getOwner());
					int damage = other.gameObject.GetComponent<Projectile> ().damage + Random.Range (-5, 6);
					this.tankData.decHealth (damage);
					this.playerManager.lastHitId = other.gameObject.GetComponent<Projectile> ().getOwner();
					other.gameObject.GetComponent<Projectile> ().selfDestroy ();
			} else if (other.gameObject.name.StartsWith ("AmmoCrate")) {
					other.gameObject.SetActive (false);
					int ammo = 15;
					this.turret.incAmmo (ammo);
					other.gameObject.GetComponent<Rotator> ().destroyObject ();
			} else if (other.gameObject.name.StartsWith ("HealthCrate")) {
					other.gameObject.SetActive (false);
					int health = 10;
					this.tankData.incHealth (health);
					other.gameObject.GetComponent<Rotator> ().destroyObject ();
			}

			colliding = false;

		}
	}
}
