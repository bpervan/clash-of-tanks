using UnityEngine;
using System.Collections;

public class SphereScript : MonoBehaviour {

	void OnTriggerEnter()
	{
		Vector3 v = new Vector3 (1, 0, 0);
		networkView.RPC ("SetColor", RPCMode.AllBuffered, v);
	}

	void OnTriggerExit()
	{
		Vector3 v = new Vector3 (0, 1, 0);
		networkView.RPC ("SetColor", RPCMode.AllBuffered, v);
	}

	[RPC]
	void SetColor(Vector3 inputColor)
	{
		renderer.material.color = new Color (inputColor.x, inputColor.y, inputColor.z, 1);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
