using UnityEngine;
public class MoneyPowerUp : PowerUp
{
	public int addMoney;
	protected override void Action()
	{
		PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + addMoney);
		Destroy(gameObject);
	}
}
