using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDisplay : MonoBehaviour
{
	public Text moneyCount;

    public List<int> prices;
	public List<float> upgratedValues;

	public string prefPowerUpName;
	public string prefForSlider;

    public Image powerUpImage;
	public Text priceText;
	public Slider progressSlider;
	public Button buyButton;

	private int indexCounter;
	private int money;
	private void Awake()
	{
		money = PlayerPrefs.GetInt("Money");
	}
	private void Start()
	{
		moneyCount.text = "Money: " + money.ToString();
		indexCounter = PlayerPrefs.GetInt(prefForSlider, 0);
		Change();
	}
	public void Upgrade()
	{
		if (money > prices[indexCounter])
		{
			PlayerPrefs.SetInt("Money", money - prices[indexCounter]);
			money = PlayerPrefs.GetInt("Money");
			moneyCount.text = "Money: " + money.ToString();
			indexCounter++;
			PlayerPrefs.SetFloat(prefPowerUpName, upgratedValues[indexCounter]);
			PlayerPrefs.SetInt(prefForSlider, indexCounter);
			Change();
		}
	}
	private void Change()
	{
		priceText.text = prices[indexCounter].ToString();
		progressSlider.value = 0.167f * indexCounter;
		if(indexCounter == 6)
		{
			priceText.text = "Upgrade Completed!";
			buyButton.interactable = false;
		}
	}
	public void Delete()
	{
		PlayerPrefs.DeleteAll();
	}
}
