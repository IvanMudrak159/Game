using UnityEngine;

public class DefaultAttackPool : MonoBehaviour
{
	public GameObject rocketManager;
	private AttackManager rocketPool;
	private Rigidbody rb;
	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		rocketPool = rocketManager.GetComponent<AttackManager>();
	}
	private void OnEnable()
	{
		rb.velocity = Vector3.zero;
		transform.position = rocketManager.transform.position;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			gameObject.SetActive(false);
		}
	}
	private void OnBecameInvisible()
	{
		TurnOff();
	}
	private void TurnOff()
	{
		rocketPool.rocketsRigidbodies.Add(rb);
		gameObject.SetActive(false);
	}
}
