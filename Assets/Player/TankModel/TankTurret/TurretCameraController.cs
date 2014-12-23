using UnityEngine;
using System.Collections;

public class TurretCameraController : MonoBehaviour {

	public TankModel tank;
	// Use this for initialization
	void Start () {
		if (tank.networkView.isMine) {
			camera.enabled = true;
		} else {
			camera.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
