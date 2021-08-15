using System.Collections;
using UnityEngine;

public class ShieldPowerUp : PowerUp
{
	public GameObject ShieldColliderEnabled;
	public Health health;
	protected override void Action()
	{
		ShieldColliderEnabled.SetActive(true);
		health.protectEnabled = true;
		Destroy(gameObject);
	}
}
