using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public string username;
	public int kills;
	public int deaths;

	public int myId;
	public int lastHitId;

	void Start () {
		username = "";
		kills = 0;
		deaths = 0;

		myId = 0;
		lastHitId = -1;
	}

	void Update () {
		username = "Player " + myId;
	}

}