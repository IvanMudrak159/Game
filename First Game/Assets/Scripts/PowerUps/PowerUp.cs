using System.Collections;
using UnityEngine;
public class PowerUp : MonoBehaviour
{
	public float lifeTime;
	private void OnEnable()
	{
		StartCoroutine(LifeTimer());
	}
	protected void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			MeshRenderer mesh = GetComponent<MeshRenderer>();
			if (mesh != null){
			mesh.enabled = false;
			}
			Action();
		}
	}
	protected IEnumerator LifeTimer()
	{
		yield return new WaitForSeconds(lifeTime);
		gameObject.SetActive(false);
	}
	protected virtual void Action()
	{

	}
}
