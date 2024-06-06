using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Rigidbody rb;

	public float moveSpeed = 15f;

	public float jumpMaxStrength = 5f;
	[Range (1, 10)] public int holdJumpIncrement = 5;
	public float jumpMilliseconds = 100f;
	public bool holdToJump = false;
	public float jumpStrength = 0f;

	[Range (0f, 1f)] public float airModifier = .9f;
	
	[HideInInspector]
	private bool grounded = true;
	private Vector3 moveDirection;
	private float jumpMillisecondsLeft = 0;

	void SetUp()
	{
		grounded = true;
		jumpMillisecondsLeft = jumpMilliseconds;
		if (holdToJump)
		{
			jumpStrength = 0;
		}
		else
		{
			jumpStrength = jumpMaxStrength;
		}
	}

	void Start()
	{
		SetUp();
	}

	void Jump()
	{
		rb.AddForce(transform.TransformDirection(new Vector3(0, jumpStrength, 0)), ForceMode.VelocityChange);
		grounded = false;
	}

	void Update()
	{
		moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
		if (Input.GetButton("Jump"))
		{
			float milliSecondsElapsed = Time.deltaTime * 1000;
			jumpMillisecondsLeft -= milliSecondsElapsed;
			if (!holdToJump && (jumpMillisecondsLeft > 0))
			{
				Jump();
			}
			else if (jumpMillisecondsLeft > 0)
			{
				jumpStrength += holdJumpIncrement;
				Mathf.Clamp(jumpStrength, 0, jumpMaxStrength);
			}
		}
		else if (holdToJump && Input.GetButtonUp("Jump"))
		{
			Jump();
			jumpStrength = 0;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		SetUp();
	}

	void FixedUpdate()
	{
		rb.MovePosition(rb.position + transform.TransformDirection(moveDirection) * (grounded ? moveSpeed : (moveSpeed * airModifier)) * Time.deltaTime);
	}
}