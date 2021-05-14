using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
	#region Variables
	private WaitForEndOfFrame waitForEndOfFrame;
	public Transform[] routes;
	public float speedModifier = 0.35f;
	public int anotherSide = 1;//must be private
	public int routeToGo = 0;
	private bool coroutineAllowed = true;
	public float tParam = 0;
	#endregion
	private void Start()
	{
		waitForEndOfFrame  = new WaitForEndOfFrame();
	}

	private void Update()
	{
		if (coroutineAllowed)
		{
 			StartCoroutine(C_GoByRoute(routeToGo));
		}
		if (Input.GetMouseButtonDown(0))
		{
			if (EventSystem.current.IsPointerOverGameObject())
			{
				return;
			}
			anotherSide *= - 1;
		}
	}
	private IEnumerator C_GoByRoute(int routeNumber)
	{
		coroutineAllowed = false;
		Vector3 p0, p1, p2, p3;
		p0 = routes[routeNumber].GetChild(0).position;
		p1 = routes[routeNumber].GetChild(1).position;
		p2 = routes[routeNumber].GetChild(2).position;
		p3 = routes[routeNumber].GetChild(3).position;
		while (tParam <= 1 && tParam >= 0)
		{
			tParam += Time.deltaTime * speedModifier * anotherSide;
			transform.position = Mathf.Pow(1 - tParam, 3) * p0 +
				3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
				3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
				Mathf.Pow(tParam, 3) * p3;
			yield return waitForEndOfFrame;
		}
		tParam = tParam < 0 ? 1 : 0;
		routeToGo = routeToGo % 2 == 1 ? routeToGo - 1 : routeToGo + 1;
		coroutineAllowed = true;
	}
}