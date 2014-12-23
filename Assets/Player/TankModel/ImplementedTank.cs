using UnityEngine;
using System.Collections;


public class ImplementedTank : ITank
{
	private float _speed;
	private float _rotation;
	private float _turretRotation;  
	private int _maxHealth;
	private int _currentHealth;
	private int _mass;

	public ImplementedTank()
	{
	 _speed = 10;
	 _rotation = 10;
	 _turretRotation=10;
	 _maxHealth = 100;
	 _currentHealth = _maxHealth;
	 _mass = 2000;
	 
	}

	public void setSpeed (float speed)
	{
		_speed = speed;
	}

	public float getSpeed ()
	{
		return _speed;
	}
	
	public void setRotation (float rotation)
	{
		_rotation = rotation;
	}

	public float getRotation()
	{
		return _rotation;
	}
	
	public void setTurretRotation(float turretRotation)
	{
		_turretRotation = turretRotation;
	}

	public float getTurretRotation ()
	{
		return _turretRotation;
	}
	
	public void setMaxHealth(int maxHealth)
	{
		_maxHealth = maxHealth;
		_currentHealth = maxHealth;
	}

	public int getMaxHealth () {
		return _maxHealth;
	}

	public void incHealth(int moreHealth)
	{
		if ((_currentHealth + moreHealth) > _maxHealth) 
						_currentHealth = _maxHealth;
				else
						_currentHealth += moreHealth;

	}

	public void decHealth(int lessHealth)
	{
		if ((_currentHealth - lessHealth) < 0) 
			_currentHealth = 0;
		else
			_currentHealth -= lessHealth;
		
	}

	public int getCurrentHealth()
	{
		return _currentHealth;
	}

	
	public void setMass(int mass)
	{
		_mass = mass;
	}


	public int getMass()
	{
		return _mass;
	}

}
