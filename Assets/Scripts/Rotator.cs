using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	// Update is called once per frame
	bool colliding;
	void Start () {
		colliding = false;
	}
	void Update () {
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
	}

	/*void OnTriggerEnter (Collider other) {
		if (other.gameObject.GetComponentInParent<TankModel> () != null && !colliding) {
			colliding = true;
			Destroy(this.gameObject);
			TankModel tank = other.gameObject.GetComponentInParent<TankModel> ();
			if (this.gameObject.name.StartsWith( "AmmoCrate")) {
				int ammo = 10 + Random.Range (0, 11);
				tank.turret.incAmmo(ammo);
			} else if (this.gameObject.name.StartsWith("HealthCrate")) {
				int health = 25 + Random.Range (0, 26);
				tank.tankData.incHealth (health);
			}
		}
	}*/

	public void destroyObject () {
		networkView.RPC ("destroyCrate", RPCMode.AllBuffered);
	}

	[RPC]
	void destroyCrate() {
		Destroy(this.gameObject);
	}
}
