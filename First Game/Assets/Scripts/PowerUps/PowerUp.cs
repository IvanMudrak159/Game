using System.Collections;
using UnityEngine;
public class PowerUp : MonoBehaviour
{
	public Vector3 position;
	public float lifeTime;
	private bool isActive = false;
	private void OnEnable()
	{
		isActive = false;
		StartCoroutine(LifeTimer());
	}
	protected void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isActive = true;
			transform.position = position;
			Action();
		}
	}
	protected IEnumerator LifeTimer()
	{
		yield return new WaitForSeconds(lifeTime);
		Switch();
	}
	private void Switch()
	{
		gameObject.SetActive(isActive);
	}
	protected virtual void Action()
	{

	}
}
