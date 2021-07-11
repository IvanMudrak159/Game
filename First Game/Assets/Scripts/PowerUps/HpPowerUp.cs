public class HpPowerUp : PowerUp
{
	public Health health;
	protected override void Action()
	{
		health.health++;
		health.healthText.text = "HP: " + health.health;
		Destroy(gameObject);
	}
}
