using System.Collections;
using UnityEngine;

public class ShieldPowerUp : PowerUp
{
	public float shieldTime;
	public GameObject ShieldColliderEnabled;
	public Health health;
	private void Awake()
	{
		shieldTime = PlayerPrefs.GetFloat("Shield", 5);
	}
	protected override void Action()
	{
		StartCoroutine(C_Shield());
	}
	public IEnumerator C_Shield()
	{
		ShieldColliderEnabled.SetActive(true);
		health.protectEnabled = true;
		yield return new WaitForSeconds(shieldTime);
		ShieldColliderEnabled.SetActive(false);
		health.protectEnabled = false;
		gameObject.SetActive(false);
	}
}
