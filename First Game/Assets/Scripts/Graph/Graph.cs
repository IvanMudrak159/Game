using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
	private GameObject lastCircleGameObject = null;
	private int lastValue = 0;
	private int index = 0;
	public float xSize = 50f;
	public float yMaximum = 100f;
	public Sprite circleSprite;
    public RectTransform graphContainer;
	private int counter = 1;
	private float graphHeight = 464.9f;
	private void Awake()
	{
		lastCircleGameObject = CreateCircle(Vector2.zero);
		index++;
	}

	private GameObject CreateCircle(Vector2 anchoredPosition)
	{
		GameObject gameObject = new GameObject("Circle", typeof(Image));
		gameObject.transform.SetParent(graphContainer, false);
		gameObject.GetComponent<Image>().sprite = circleSprite;
		gameObject.GetComponent<Image>().color = new Color32(252, 193, 23, 255);
		RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
		rectTransform.anchoredPosition = anchoredPosition;
		rectTransform.sizeDelta = new Vector2(11, 11);
		rectTransform.anchorMin = new Vector2(0, 0);
		rectTransform.anchorMax = new Vector2(0, 0);
		return gameObject;
	}
	public void ShowGraph(int value)
	{
		lastValue += value;
		float xPosition = index * xSize;
		float yPosition = (lastValue / yMaximum) * graphHeight;
		GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
		if (lastCircleGameObject != null)
		{
			CreateDotConnection
				(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
				circleGameObject.GetComponent<RectTransform>().anchoredPosition, 
				value);
		}
		lastCircleGameObject = circleGameObject;
		index++;
	}
	private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, int value)
	{
		GameObject gameObject = new GameObject("dotconnection", typeof(Image));
		Color32 color;
		if(value < 0)
		{
			color = new Color32(207, 48, 74, 255);
		}
		else
		{
			color = new Color32(2, 192, 118, 255);
		}
		gameObject.GetComponent<Image>().color = color;
		gameObject.transform.SetParent(graphContainer, false);
		gameObject.transform.SetSiblingIndex(0);
		RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
		Vector2 dir = (dotPositionB - dotPositionA).normalized;
		float distance = Vector2.Distance(dotPositionA, dotPositionB);
		rectTransform.sizeDelta = new Vector2(distance, 5f);
		rectTransform.anchorMin = new Vector2(0, 0);
		rectTransform.anchorMax = new Vector2(0, 0);
		rectTransform.anchoredPosition = dotPositionA + dir * distance * 0.5f;
		rectTransform.localEulerAngles = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg * Vector3.forward;
	}
}
