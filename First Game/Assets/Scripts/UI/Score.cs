using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public Transform[] routes;
	public List<GameObject> points = new List<GameObject>();
	public Text scoreText;
	public Text highScoreText;
	public GameObject rocketManager;
	public int score = 0;
	public int scoreMultiplier = 1;
	private AttackManager rocketPool;
	[SerializeField] private GameObject scorePoint;
	public PowerUpManager powerUpManager;
	private int powerUpPermission = 7;
	private int skipCounter = 0;
	private bool isActive = false;
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
	}
	private void Update()
	{
		if (score > powerUpPermission)
		{
			powerUpPermission += 7;
			if (Random.Range(0f, 10f) > 0)
			{
				powerUpManager.PowerUpPool();
			}
		}
		if (!isActive)
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
			isActive = true;
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
		scoreText.text = "Score: " + score.ToString("0");
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
			skipCounter++;
			//was made for skipping every 9-th scorePoint
			if (skipCounter == 9)
			{
				skipCounter = 0;
				return;
			}
			AddScore();
			other.gameObject.SetActive(false);
			if (skipCounter == 8)
			{
				isActive = false;
			}
		}
	}
}
