using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	void Update () {
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
	}

	public void destroyObject () {
		networkView.RPC ("destroyCrate", RPCMode.AllBuffered);
	}

	[RPC]
	void destroyCrate() {
		Destroy(this.gameObject);
	}
}
