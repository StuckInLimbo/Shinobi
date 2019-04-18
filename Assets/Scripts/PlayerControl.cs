using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour {
	[SerializeField] private float m_MinSpeed = -20f;		//Min Running Speed
	[SerializeField] private float m_Speed = 0f;			//Current Running Speed
	[SerializeField] private float m_MaxSpeed = 20f;        //Max Running Speed
	[SerializeField] private float m_Acceleration = 200f;    //Max Running Speed
	[SerializeField] private float m_JumpPower = 500f;      //Jump Height
	[SerializeField] private float m_DashDistance = 100f;      //Dash Length
	[SerializeField] private bool m_CanDoubleJump = true;   //Is player able to double jump?
	[SerializeField] private bool m_CanSlide = true;        //Is player able to slide?
	[SerializeField] private bool m_CanDash = true;			//Is player able to dash?
	[SerializeField] private bool m_AirControl = false;     // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;      // A mask determining what is ground to the character

	private bool m_Grounded;            // Whether or not the player is grounded.
	private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private Animator m_Anim;
	private Rigidbody2D m_Rigidbody;
	private bool m_Jump = false;
	private bool m_Dash = false;
	private bool m_Slide = false;
	private double m_DashCooldown = 1.5;
	private double m_TimeStamp;

	void Awake() {
		m_GroundCheck = transform.Find("GroundCheck");
		m_Rigidbody = GetComponent<Rigidbody2D>();
		
	}
	
	void Update() {
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		//Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		//for (int i = 0; i < colliders.Length; i++) {
		//	if (colliders[i].gameObject != gameObject) {
		//		m_Grounded = true;
		//		m_CanDoubleJump = true;
		//	}
		//}

		if(Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f),
			new Vector2(transform.position.x + 0.5f, transform.position.y - 0.51f), m_WhatIsGround)) {
			m_Grounded = true;
			m_CanDoubleJump = true;
			m_CanDash = true;
		}
		else {
			m_Grounded = false;
		}

		m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
		m_Dash = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);

		//GetAxis("Horizontal") ranges from -1.0f to 1.0f, that is too little
		m_Speed = Mathf.Lerp(m_Rigidbody.velocity.x, (Input.GetAxisRaw("Horizontal") * 50) * m_Acceleration * Time.deltaTime, Time.deltaTime * 10);
		m_Speed = Mathf.Clamp(m_Speed, m_MinSpeed, m_MaxSpeed);
		m_Rigidbody.velocity = new Vector2(m_Speed, m_Rigidbody.velocity.y);

		if (m_Jump) {
			if (m_Grounded) {
				m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, 0f);
				m_Rigidbody.AddForce(new Vector2(0f, m_JumpPower));
				m_CanDoubleJump = true;
			}
			else {
				if (m_CanDoubleJump) {
					m_CanDoubleJump = false;
					m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, 0f);
					m_Rigidbody.AddForce(new Vector2(0f, m_JumpPower));
				}
			}
		}
		if(m_Dash && m_CanDash && m_TimeStamp <= Time.time) { //fix

			m_TimeStamp = Time.time + m_DashCooldown;
			Debug.Log("Dash on cooldown!");
			Debug.Log("Current time: " + Time.time + " cooldown timer: " + m_TimeStamp);
		}
	}
}
