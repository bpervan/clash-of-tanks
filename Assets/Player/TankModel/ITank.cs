using UnityEngine;
using System.Collections;

public interface ITank
{
	void setSpeed (float speed);
	float getSpeed ();

	void setRotation (float rotation);
	float getRotation();

	void setTurretRotation(float turretRotation);
	float getTurretRotation ();

	void setMaxHealth(int health);
	int getMaxHealth ();

	void incHealth(int moreHealth);
	void decHealth(int lessHealth);
	int getCurrentHealth();

	void setMass(int mass);
	int getMass();
}
