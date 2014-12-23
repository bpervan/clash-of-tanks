using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public float btnX;
	public float btnY;
	public float btnW;
	public float btnH;

	public GameObject playerPrefab;
	public Transform spawnObject;
	public PlayerManager playerManager;

	private GameObject currentTank;

	private HostData[] hostData;

	private string ipAddress = "192.168.1.000";
	
	private TouchScreenKeyboard keyboard;
	private bool joinServerPressed = false;
	public GUIText serverIPText;

	private Vector3 playerLastPosition;

	private static int playerId = 1;

	void StartServer()
	{
		Network.InitializeServer (32, 25001, !Network.HavePublicAddress());
	}

	void spawnPlayer()
	{
		Camera.main.gameObject.SetActive (false);
		GameObject tankModel = (GameObject)Network.Instantiate (playerPrefab, spawnObject.position, Quaternion.identity, 0);

		currentTank = tankModel;
	}

	//Messages
	void OnServerInitialized()
	{
		playerManager.myId = playerId;
		spawnPlayer ();
	}

	void OnConnectedToServer()
	{
		networkView.RPC ("assignMyId", RPCMode.AllBuffered, Vector3.up);
		spawnPlayer ();
	}

	//GUI
	void OnGUI()
	{
		if(!Network.isClient && !Network.isServer){
			if (GUI.Button (new Rect (btnX, btnY, btnW, btnH), "Start Server")) 
			{
				StartServer();
			}
			
			float newW = btnY * 1.2f + btnH;
			if (GUI.Button (new Rect (btnX, newW, btnW, btnH), "Join Server")) 
			{
				this.joinServerPressed = true;
			}
			
			float newX = btnX * 1.5f + btnW;
			float newY = btnY * 1.2f + btnH;
			float newW2 = btnW * 3f;
			float newH = btnH * 0.5f;
			
			if (this.joinServerPressed) 
			{
				ipAddress = GUI.TextField(new Rect(newX, newY, newW2, newH), ipAddress, 15);
				if (GUI.Button(new Rect(newX + newW2 + 15, newY, newW2, newH), "Connect"))
				{
					Network.Connect(ipAddress, 25001);
				}
			}
		}
		
	}

	// Use this for initialization
	void Start () {
		btnX = Screen.width * 0.05f;
		btnY = Screen.width * 0.05f;
		btnW = Screen.width * 0.1f;
		btnH = Screen.width * 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		checkPlayersHealth ();
	}

	private GameObject respawnPlayer (GameObject tankModel) {

		Vector3 spawnPos = findSpawnPosition ();
		tankModel.GetComponent<TankModel> ().tankData.setMaxHealth (100);
		tankModel.GetComponentInChildren<TankTurret> ().projectileNumber [0] = 30;
		tankModel.transform.position = spawnPos;
        tankModel.transform.rotation = Quaternion.identity;
		return tankModel;

	}

	Vector3 findSpawnPosition () {
		float max = 245.0f;
		float min = -245.0f;
		float x = Random.Range(min, max);
		float z = Random.Range(min, max);
		float y = 100;
		
		Vector3 pos = new Vector3(x,y,z);
		
		RaycastHit hit;
		// note that the ray starts at 100 units
		Ray ray = new Ray (pos, Vector3.down);
		while (true) {	
			if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {        
				if (hit.collider     != null && hit.collider.gameObject.name.Equals("Terrain")) {
					Vector3 spawnPos = new Vector3(x, hit.point.y + 3.0f, z);
					return spawnPos;
				}
			}
		}
	}

	void checkPlayersHealth () {

		GameObject tankModel = currentTank;
		if (tankModel == null)
			return;
		if (tankModel.GetComponent<TankModel>().tankData.getCurrentHealth() <= 0) {
			playerManager.deaths += 1;
			Vector3 v = new Vector3 (0, playerManager.lastHitId, 0);
			networkView.RPC("incPlayerKill", RPCMode.AllBuffered, v);
			currentTank = respawnPlayer(tankModel);

		}

	}

	[RPC]
	void assignMyId (Vector3 vec) {
		if (Network.isServer) {
			Debug.Log ("assignMyID:" + vec);
			playerId++;
			Vector3 v = new Vector3(0, playerId, 0);
			networkView.RPC ("setPlayerId", RPCMode.AllBuffered, v);
		}
	}

	[RPC]
	void setPlayerId (Vector3 v) {
		Debug.Log ("Set player id:" + v);
		if (playerManager.myId == 0) {
			playerManager.myId = Mathf.RoundToInt(v.y);
		}
	}

	[RPC]
	void incPlayerKill (Vector3 vec) {
		int id = Mathf.RoundToInt (vec.y);
		Debug.Log ("Set kill id:" + id);
		if (playerManager.myId == id) {
			playerManager.kills += 1;
		}
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Clean up after player " + player);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}
}
