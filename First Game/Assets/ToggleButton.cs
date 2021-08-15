using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
	private string key;
	private Button button;
	public void Awake()
	{
		button = GetComponent<Button>();
		key = gameObject.name + "IsOn";
		ChangeColor();
	}

	private void ChangeColor()
	{
		ColorBlock color = button.colors;
		if (PlayerPrefs.GetInt(key, 1) % 2 == 0)
		{
			color.colorMultiplier = 5; 
			//Off
		}
		else
		{
			color.colorMultiplier = 1;
			//On
		}
		button.colors = color;
	}

	public void ChangeVibration()
	{
		PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) + 1);
		int i = PlayerPrefs.GetInt(key);
		if (i % 2 == 0)
		{
			PlayerPrefs.SetInt("Vibration", 0);
		}
		else
		{
			PlayerPrefs.SetInt("Vibration", 100);
		}
		ChangeColor();
	}
	public void Music()
	{
		int musicInt = PlayerPrefs.GetInt(key, 1);
		musicInt += 1;
		musicInt %= 2;
		Debug.Log(musicInt);
		PlayerPrefs.SetInt(key, musicInt);
		AudioListener.volume = musicInt;
		ChangeColor();
	}
}
