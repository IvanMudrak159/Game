using UnityEngine;

public class DefaultAttackPool : MonoBehaviour
{
	public GameObject rocketManager;
	private AttackManager rocketPool;
	private Rigidbody rb;
	private MeshRenderer meshRenderer;
	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		rocketPool = rocketManager.GetComponent<AttackManager>();
		meshRenderer = GetComponent<MeshRenderer>();
	}
	private void OnEnable()
	{
		rb.velocity = Vector3.zero;
		transform.position = Vector3.zero;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			TurnOff();
		}
	}
	private void Update()
	{
		if (!meshRenderer.isVisible)
		{
			TurnOff();
		}
	}
	private void TurnOff()
	{
		gameObject.SetActive(false);
		rocketPool.rocketsRigidbodies.Add(rb);
	}

}
