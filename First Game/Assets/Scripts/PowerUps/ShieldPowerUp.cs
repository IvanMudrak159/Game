using System.Collections;
using UnityEngine;

public class ShieldPowerUp : PowerUp
{
	public float activeTime;
	public GameObject ShieldColliderEnabled;
	public Health health;
	private void Awake()
	{
		activeTime = PlayerPrefs.GetFloat("Shield", 5);
	}
	protected override void Action()
	{
		ShieldColliderEnabled.SetActive(true);
		health.protectEnabled = true;
		Destroy(gameObject);
	}
}
