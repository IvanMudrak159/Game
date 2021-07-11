using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Skin
{
	public int cost;

	public GameObject skinGameObject;
}

public class SkinManager : MonoBehaviour
{
	public Image[] selectButtons;
    [SerializeField] public Skin[] skins;
	[SerializeField] private GameObject buyButton;
	[SerializeField] private Text moneyText;
	private const string Prefix = "Skin_";
    private const string SelectedSkin = "SelectedSKin";
	private int lastSelectedSkin = 0;

	private void Start()
	{
		int selectedSkin = PlayerPrefs.GetInt(SelectedSkin, 0);
		OnSkinPressed(selectedSkin);
		for (int skinIndex = 0; skinIndex < skins.Length; skinIndex++)
		{
			if (IsUnlocked(skinIndex))
			{
			UpdateButton(skinIndex);
			}
		}
		moneyText.text = "Money: " + PlayerPrefs.GetInt("Money", 0).ToString();
	}

	private void UpdateButton(int skinIndex)
	{
			Color color = selectButtons[skinIndex].color;
			color.a = 255;
			selectButtons[skinIndex].color = color;
			selectButtons[skinIndex].transform.GetChild(0).gameObject.SetActive(false);
	}

	public void SelectSkin(int skinIndex) => PlayerPrefs.SetInt(SelectedSkin, skinIndex);
	public void OnSkinPressed(int skinIndex)
	{
		lastSelectedSkin = skinIndex;

		for (int i = 0; i < skins.Length; i++)
		{
			skins[i].skinGameObject.SetActive(false);
		}
		skins[skinIndex].skinGameObject.SetActive(true);

		if (IsUnlocked(skinIndex))
		{
			SelectSkin(skinIndex);
		}

		buyButton.SetActive(!IsUnlocked(skinIndex));
	}
	public void OnBuyButtonPressed()
	{
		int coins = PlayerPrefs.GetInt("Money", 0);

		//Unlock the skin
		if (coins >= skins[lastSelectedSkin].cost && !IsUnlocked(lastSelectedSkin))
		{
			PlayerPrefs.SetInt("Money", coins - skins[lastSelectedSkin].cost);
			Unlock(lastSelectedSkin);
			SelectSkin(lastSelectedSkin);
			UpdateButton(lastSelectedSkin);
			moneyText.text = "Money: " + PlayerPrefs.GetInt("Money").ToString();
		}
		else
		{
			Debug.Log("Not enough coins...");
		}
	}

	public void Unlock(int skinIndex)
	{
		PlayerPrefs.SetInt(Prefix + skinIndex, 1);
		buyButton.SetActive(false);
	}
    public bool IsUnlocked(int skinIndex) => PlayerPrefs.GetInt(Prefix + skinIndex, 0) == 1;
}


