using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
	public List<int> valueList;
	public float xSize = 50f;
	public float yMaximum = 100f;
	public Sprite circleSprite;
    public RectTransform graphContainer;
	private void Awake()
	{
		ShowGraph(valueList);
	}
	private GameObject CreateCircle(Vector2 anchoredPosition)
	{
		GameObject gameObject = new GameObject("Circle", typeof(Image));
		gameObject.transform.SetParent(graphContainer, false);
		gameObject.GetComponent<Image>().sprite = circleSprite;
		RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
		rectTransform.anchoredPosition = anchoredPosition;
		rectTransform.sizeDelta = new Vector2(11, 11);
		rectTransform.anchorMin = new Vector2(0, 0);
		rectTransform.anchorMax = new Vector2(0, 0);
		return gameObject;
	}
	private void ShowGraph(List<int> valueList)
	{
		float graphHeight = graphContainer.sizeDelta.y;
		GameObject lastCircleGameObject = null;
		for (int i = 0; i < valueList.Count; i++)
		{
			float xPosition = i * xSize;
			float yPosition = (valueList[i] / yMaximum) * graphHeight;
			GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
			if(lastCircleGameObject != null)
			{
				CreateDotConnection
					(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
					circleGameObject.GetComponent<RectTransform>().anchoredPosition);
			}
			lastCircleGameObject = circleGameObject;
		}
	}
	private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
	{
		GameObject gameObject = new GameObject("dotconnection", typeof(Image));
		gameObject.transform.SetParent(graphContainer, false);
		gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
		RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
		Vector2 dir = (dotPositionB - dotPositionA).normalized;
		float distance = Vector2.Distance(dotPositionA, dotPositionB);
		rectTransform.sizeDelta = new Vector2(distance, 3f);
		rectTransform.anchorMin = new Vector2(0, 0);
		rectTransform.anchorMax = new Vector2(0, 0);
		rectTransform.anchoredPosition = dotPositionA + dir * distance * 0.5f;
		rectTransform.localEulerAngles = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg * Vector3.forward;
	}
}
