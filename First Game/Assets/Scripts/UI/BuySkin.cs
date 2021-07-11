using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySkin : MonoBehaviour
{
	public Button buyButton;
	public Text priceText;
	public int money = 500;
	public GameObject selectedSkin;
	private int lastSkinPrice = 0;
	private Button lastButtonClicked;
	public void Preview( int price, bool isOwned)
	{
		buyButton.gameObject.SetActive(!isOwned);
		lastSkinPrice = price;
		priceText.text = price.ToString();
		selectedSkin.SetActive(false);
		//selectedSkin = skinPreivew;
		selectedSkin.SetActive(true);
	}
	public void buySkin()
	{
		if(money > lastSkinPrice)
		{
			money -= lastSkinPrice;
			
		}
	}
}
