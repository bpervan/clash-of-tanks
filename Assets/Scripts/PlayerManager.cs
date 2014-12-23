using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public string username;
	public int kills;
	public int deaths;

	public int myId;
	public string lastHitId;

	void Start () {
		username = "";
		kills = 0;
		deaths = 0;

		myId = 0;
		lastHitId = "";
	}

	void Update () {
		username = "Player " + myId;
	}

}