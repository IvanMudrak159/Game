using UnityEngine;
public class Movement : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed = 5f;
	Rigidbody2D rb;
	Touch touch;
	Vector3 touchPosition, whereToMove;
	bool isMoving = false;
	float previousDistanceToTouchPos, currentDistanceToTouchPos;
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	private void Update()
	{
		if (isMoving)
		{
			currentDistanceToTouchPos = (touchPosition - transform.position).magnitude;
		}
		if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Mouse0))
		{
			//touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began)
			{
				previousDistanceToTouchPos = 0;
				currentDistanceToTouchPos = 0;
				isMoving = true;
				//touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
				touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				touchPosition.z = 0; 
				whereToMove = (touchPosition - transform.position).normalized;
				rb.velocity = new Vector2(whereToMove.x, whereToMove.y) * moveSpeed;
			}
		}
		if (currentDistanceToTouchPos > previousDistanceToTouchPos)
		{
			isMoving = false;
			rb.velocity = Vector2.zero;
		}
		if (isMoving)
		{
			previousDistanceToTouchPos = (touchPosition - transform.position).magnitude;
		}
	}
	private void FixedUpdate()
	{
			float angle = Mathf.Atan2(whereToMove.y, whereToMove.x) * Mathf.Rad2Deg - 90f;
			rb.rotation = angle;
	}
}