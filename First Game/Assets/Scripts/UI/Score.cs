using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public static Score scoreSingleton { get; private set; }
	public delegate void powerUpAction();
	public event powerUpAction powerUpSpawn;

	public UILineRenderer line;

	public Transform[] routes;
	public List<GameObject> points = new List<GameObject>();

	public Text scoreText;
	public Text highScoreText;

	public GameObject rocketManager;

	public int score = 0;
	public int scoreMultiplier = 1;
	public int finalScore;

	public GameObject nextLevelPanel;
	public PowerUpManager powerUpManager;

	private AttackManager rocketPool;
	[SerializeField] private GameObject scorePoint;
	private int powerUpPermission = 2;
	private int scoreLimit = 8;
	private void Awake()
	{
		scoreSingleton = this;
	}
	private void Start()
	{
		PlayerPrefs.GetInt("Money", 0);
		PlayerPrefs.GetInt("HighScore", 0);

		rocketPool = rocketManager.GetComponent<AttackManager>();
		for (int i = 0; i < 8; i++)
		{
			points.Add(Instantiate(scorePoint));
			points[i].SetActive(false);
		}
		SpawnCoins();
	}
	private void Update()
	{
		if (score > powerUpPermission)
		{
			powerUpPermission += 3;
			powerUpSpawn?.Invoke();
		}
	}

	private void SpawnCoins()
	{
		int route = 0;
		for (int i = 0; i < 8; i++)
		{
			if (i > 3)
			{
				route = 1;
			}
			points[i].transform.position = GetPos(route, i * 0.25f % 1);
			points[i].SetActive(true);
		}
	}

	public Vector3 GetPos(int route, float tParam)
	{
		Vector3 p0 = routes[route].GetChild(0).position;
		Vector3 p1 = routes[route].GetChild(1).position;
		Vector3 p2 = routes[route].GetChild(2).position;
		Vector3 p3 = routes[route].GetChild(3).position;
		Vector3 pos = Mathf.Pow(1 - tParam, 3) * p0 +
				3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
				3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
				Mathf.Pow(tParam, 3) * p3;
		return pos;
	}
	public void AddScore()
	{
		rocketPool.scorePoints += scoreMultiplier;
		score += scoreMultiplier;
		scoreText.text = "Score: " + score.ToString() + "/" + finalScore.ToString();
	}
	public void ShowHighScore()
	{
		if(score > PlayerPrefs.GetInt("Highscore"))
		{
			PlayerPrefs.SetInt("Highscore", score);
		}
		highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("ScorePoint"))
		{
			line.AddValue(1f, 1.5f);
			AddScore();
			other.gameObject.SetActive(false);
			PlayerPrefs.SetInt("Money", score + PlayerPrefs.GetInt("Money"));
			if (score >= finalScore)
			{
				Time.timeScale = 0f;
				nextLevelPanel.SetActive(true);
				PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
			}
			if (score >= scoreLimit)
			{
				scoreLimit += 8;
				SpawnCoins();
			}
		}
	}
}
