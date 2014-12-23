using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	public GUISkin resourceSkin;
	private const int RESOURCE_BAR_HEIGHT = 25;
	private const int USERNAME_HEIGHT = 25;
	private const int USERNAME_WIDTH = 100;
	private const int HEALTH_HEIGHT = 25;
	private const int HEALTH_WIDTH = 100;
	private const int PROJECTILE_HEIGHT = 25;
	private const int PROJECTILE_WIDTH = 100;
	private const int KILL_DEATH_HEIGHT = 25;
	private const int KILL_DEATH_WIDTH = 100;

	PlayerManager playerManager;
	

	public TankModel tank;
	// Use this for initialization
	void Start () {
		playerManager = GameObject.Find ("PlayerManager").GetComponent<PlayerManager>();
	}
	
	// Update is called once per frame
	void OnGUI () {

		drawResourceBar();

	}
    /* This function draws resource bar */
	void drawResourceBar () {
		GUI.skin = resourceSkin;
		GUI.BeginGroup(new Rect(0,0,Screen.width,RESOURCE_BAR_HEIGHT));
		GUI.Box(new Rect(0,0,Screen.width,RESOURCE_BAR_HEIGHT),"");
		GUI.Label (new Rect (0, 0, USERNAME_WIDTH, USERNAME_HEIGHT), playerManager.username);

		int currentHealth = tank.tankData.getCurrentHealth ();
		int maxHealth = tank.tankData.getMaxHealth ();
		string health = "Health: " + currentHealth + "/" + maxHealth;

		GUI.Label (new Rect (USERNAME_WIDTH + 5, 0, HEALTH_WIDTH, HEALTH_HEIGHT), health);

		int projectileNumber = tank.turret.projectileNumber [0];
		string projectile = "Ammo: " + projectileNumber;
		GUI.Label (new Rect (USERNAME_WIDTH + HEALTH_WIDTH + 20, 0, PROJECTILE_WIDTH, PROJECTILE_HEIGHT), projectile);

		int kills = playerManager.kills;
		int deaths = playerManager.deaths;
		string kill_death = "Score: " + kills + " - " + deaths;
		GUI.Label (new Rect (USERNAME_WIDTH + HEALTH_WIDTH + PROJECTILE_WIDTH +20, 0, KILL_DEATH_WIDTH, KILL_DEATH_HEIGHT), kill_death);
		GUI.Label(new Rect(USERNAME_WIDTH + HEALTH_WIDTH + PROJECTILE_WIDTH + KILL_DEATH_WIDTH + 20, 0, 2 * HEALTH_WIDTH, PROJECTILE_HEIGHT), 
		          "IP Address: " + Network.player.ipAddress);

		GUI.EndGroup();

	}
}
