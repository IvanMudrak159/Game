using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILineRenderer : Graphic
{
	public Vector2Int gridSize;

	public List<Vector2> points;
	public UIGridRenderer grid;

	public Text startLabelY;
	public Text firstLabelY;
	public Text lastLabelY;

	private float width;
	private float height;
	private float unitWidth;
	private float unitHeight;

	public float thickness = 10f;
	public float step;

	private int count = 1;
	private Vector2 tempPoint;
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();

		width = rectTransform.rect.width;
		height = rectTransform.rect.height;

		unitWidth = width / (float)gridSize.x;
		unitHeight = height / (float)gridSize.y;

		if(points.Count < 2)
		{
			return;
		}
		for (int i = 0; i < points.Count; i++)
		{
			Vector2 point = points[i];
			DrawVerticesForPoint(point, vh);
		}
		for (int i = 0; i < points.Count - 1; i++)
		{
			int index = i * 2;
			vh.AddTriangle(index + 0, index + 1, index + 3);
			vh.AddTriangle(index + 3, index + 2, index + 0);
		}
	}
	void DrawVerticesForPoint(Vector2 point, VertexHelper vh)
	{
		UIVertex vertex = UIVertex.simpleVert;
		vertex.color = color;

		vertex.position = new Vector3(-thickness / 2, 0);
		vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
		vh.AddVert(vertex);

		vertex.position = new Vector3(thickness / 2, 0);
		vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
		vh.AddVert(vertex);
	}
	private void Update()
	{
		if (points[count].x > gridSize.x || points[count].y > gridSize.y)
		{
			grid.gridSize = new Vector2Int(gridSize.x + 10, gridSize.y + 10);
			gridSize = grid.gridSize;
			lastLabelY.text = gridSize.y.ToString();
			startLabelY.transform.position = new Vector3(startLabelY.transform.position.x, 5 * unitHeight, 0);
			SetVerticesDirty();
		}
		tempPoint.x = points[count].x + step;
		tempPoint.y = points[count].y - step;
		points[count] = tempPoint;
		SetVerticesDirty();
	}

	public void AddValue(float x, float y)
	{
		tempPoint.x = points[count].x + x;
		tempPoint.y = points[count].y + y;
		points.Add(tempPoint);
		points.Add(tempPoint);
		count += 2;
	}
}
