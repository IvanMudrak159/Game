using UnityEngine;
public class MoneyPowerUp : PowerUp
{
	private int addMoney;
	private void Awake()
	{
		addMoney = PlayerPrefs.GetInt("AddMoney", 3);
	}
	protected override void Action()
	{
		PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + addMoney);
		Destroy(gameObject);
	}
}
