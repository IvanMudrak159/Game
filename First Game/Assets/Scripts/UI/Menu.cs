﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	public GameObject pausePanel;
	public GameObject MenuCanvas;
	public GameObject GameCanvas;
	public Text MoneyPanel;
	public Text HighscoreText;
	private int click = 0;
	private void Awake()
	{
		//PlayerPrefs.SetInt("Level", 0);
		PlayerPrefs.GetInt("Level", 0);
		Time.timeScale = 0;
		if (MoneyPanel != null)
		{
			MoneyPanel.text = "Money: " + PlayerPrefs.GetInt("Money", 0).ToString();
		}
		if(HighscoreText != null)
		{
			HighscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore", 0).ToString();
		}
	}
	public void PauseButton()
	{
		click++;
		if(click % 2 == 1)
		{
			Time.timeScale = 0;
			pausePanel.SetActive(true);
		}
		else
		{
			Time.timeScale = 1;
			pausePanel.SetActive(false);
		}
	}
	public void ExitButton()
	{
		Application.Quit();
	}
	public void LoadSceneButton(int i)
	{
		SceneManager.LoadScene(i);
	}
	public void ReloadButton()
	{
		SceneManager.LoadScene(PlayerPrefs.GetInt("Level") + 2);
	}
	public void StartGame()
	{
		MenuCanvas.SetActive(false);
		Time.timeScale = 1;
		GameCanvas.SetActive(true);
	}
}
