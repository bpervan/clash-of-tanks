﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	private float velocity;
	public int damage;
	private bool moving;
	private volatile int owner;
	public GameObject explosion;

    public int getOwner() {
        return this.owner;
    }

    public void setOwnerLocal(int ownerId) {
        if (networkView.isMine) {
            this.owner = ownerId;
            networkView.RPC("setOwner", RPCMode.All, ownerId);
        }
    }

	// Use this for initialization
	void Start () {
		velocity = 50;
		rigidbody.mass = 0.5f;
		damage = 20;
		owner = 0;

	}

	void FixedUpdate () {
		if (moving)
			move ();
	}

	private void move () {
		rigidbody.MovePosition (transform.position + transform.forward * velocity * Time.deltaTime);
	}

	public void fire () {
		moving = true;
	}

	[RPC]
	public void setOwner(int ownerId) {
		if (this.owner == 0) {
			Debug.Log ("Set projectile id: " + ownerId);
			this.owner = ownerId;
		}
	}

	void OnTriggerEnter (Collider other) {
		//Debug.Log (other.gameObject.name);
		if (!networkView.isMine)
			return;
		if (other.gameObject.GetComponentInParent<TankModel> () == null && 
		    other.gameObject.GetComponent<TankModel>() == null) {
			selfDestroy();
		}
	}

	public void selfDestroy () {
		networkView.RPC ("destroyProjectile", RPCMode.AllBuffered);
		Network.Instantiate (explosion, transform.position, Quaternion.identity, 0);
	}

	[RPC]
	void destroyProjectile () {
		Destroy (gameObject);
	}
}
