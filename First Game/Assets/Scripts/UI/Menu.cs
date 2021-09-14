using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	public GameObject pausePanel;
	public GameObject MenuCanvas;
	public GameObject GameCanvas;
	public GameObject SwitchGameObject;
	public Text timerText;
	public Text InformationText;
	public Text MoneyText;
	public Text HighscoreText;
	public int moneyRewardValue = 15;
	private int click = 0;
	private void Awake()
	{
		AdManager.AddCoins += UpdateText;
		Application.targetFrameRate = 60;
		PlayerPrefs.GetInt("Level", 0);
		Time.timeScale = 0;
		if (MoneyText != null)
		{
			MoneyText.text = "Money: " + PlayerPrefs.GetInt("Money", 0).ToString();
		}
		if(HighscoreText != null)
		{
			HighscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore", 0).ToString();
		}
	}
	public void UpdateText()
	{
		PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + moneyRewardValue);
		MoneyText.text = "Money: " + PlayerPrefs.GetInt("Money", 0).ToString();
	}
	public void Vibration(bool shit)
	{
		int i = PlayerPrefs.GetInt("Vibration", 0);
		if (i % 2 == 0)
		{
			PlayerPrefs.SetInt("Duration", 100);
		}
		else
		{
			PlayerPrefs.SetInt("Duration", 0);
		}
	}
	public void Music()
	{
		int i = PlayerPrefs.GetInt("Music", 0);
		if (i % 2 == 0)
		{
			PlayerPrefs.SetInt("Duration", 100);
		}
		else
		{
			PlayerPrefs.SetInt("Duration", 0);
		}
	}
	public void CountClicks(string key)
	{
		PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key, 0) + 1);
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
			pausePanel.SetActive(false);
			StartCoroutine(C_Timer());
		}
	}
	public void SetLevel(int i)
	{
		PlayerPrefs.SetInt("Level", 0);
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
		SceneManager.LoadScene(PlayerPrefs.GetInt("Level") + 3);
	}
	public void Delete()
	{
		PlayerPrefs.DeleteAll();
	}
	public void StartGame()
	{
		MenuCanvas.SetActive(false);
		Time.timeScale = 1;
		GameCanvas.SetActive(true);
	}
	public IEnumerator C_Timer()
	{
		//Animator timerAnimation = timerText.GetComponent<Animator>();
		timerText.transform.parent.gameObject.SetActive(true);
		for (int i = 3; i >	 0; i--)
		{
			timerText.text = "" + i;
			//timerAnimation.Play("Text Animation");
			yield return new WaitForSecondsRealtime(1f);
		}
		timerText.transform.parent.gameObject.SetActive(false);
		Time.timeScale = 1f;
	}

}
