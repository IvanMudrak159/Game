using System.Collections;
using UnityEngine;

public class ShieldPowerUp : PowerUp
{
	public GameObject ShieldColliderEnabled;
	public Health health;
	private void Awake()
	{
		lifeTime = PlayerPrefs.GetFloat("Shield", 5);
	}
	protected override void Action()
	{
		ShieldColliderEnabled.SetActive(true);
		health.protectEnabled = true;
		Destroy(gameObject);
	}
}
