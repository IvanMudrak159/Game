using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	public GameObject pausePanel;
	public GameObject MenuCanvas;
	public GameObject GameCanvas;
	public GameObject SwitchGameObject;
	public Text InformationText;
	public Text MoneyPanel;
	public Text HighscoreText;
	private int click = 0;
	private void Awake()
	{
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
	public void SetText(string text)
	{
		InformationText.text = text;
	}
	public void EnableGameObject(bool enable)
	{
		SwitchGameObject.SetActive(enable);
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
