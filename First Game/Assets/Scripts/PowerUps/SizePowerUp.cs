using System.Collections;
using UnityEngine;
public class SizePowerUp : PowerUp
{
    public float sizeChange;
	public Collider playerCollider;
	public Transform playerTransform;
	private void Awake()
	{
		sizeChange = PlayerPrefs.GetFloat("Size", 5);
	}
	protected override void Action()
	{
		StartCoroutine(Resize());
	}
	private IEnumerator Resize()
	{
		playerTransform.localScale -= Vector3.one * sizeChange;
		yield return new WaitForSeconds(lifeTime);
		playerTransform.localScale += Vector3.one * sizeChange;
		gameObject.SetActive(false);
	}
}
