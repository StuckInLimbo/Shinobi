using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour {
	[SerializeField] private float m_MinSpeed = -20f;		//Min Running Speed
	[SerializeField] private float m_Speed = 0f;			//Current Running Speed
	[SerializeField] private float m_MaxSpeed = 20f;		//Max Running Speed
    [SerializeField] private float m_JumpPower = 500f;		//Jump Height
    [SerializeField] private bool m_CanDoubleJump = true;   //Is player able to double jump?
	[SerializeField] private bool m_CanSlide = true;        //Is player able to slide?
	[SerializeField] private bool m_CanDash = true;			//Is player able to dash?
	[SerializeField] private bool m_AirControl = false;     // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;      // A mask determining what is ground to the character

	private bool m_Grounded;            // Whether or not the player is grounded.
	private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private Animator m_Anim;
	private Rigidbody2D m_Rigidbody2D;
	private bool m_Jump = false;

	void Awake() {
		m_GroundCheck = transform.Find("GroundCheck");
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Update() {
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].gameObject != gameObject) {
				m_Grounded = true;
				m_CanDoubleJump = true;
				//m_TimesJumped = 0;
			}
		}
		m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
		//GetAxis("Horizontal") ranges from -1.0f to 1.0f, that is too little
		m_Speed = (CrossPlatformInputManager.GetAxis("Horizontal") * 10) + (10f * Time.deltaTime);

		//m_Speed = Mathf.Clamp(m_Speed, m_MinSpeed, m_MaxSpeed); //clamp between -20 and 20

		m_Rigidbody2D.velocity = new Vector2(m_Speed, m_Rigidbody2D.velocity.y);

		if (m_Jump) {
			if (m_Grounded) {
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpPower));
				m_CanDoubleJump = true;
			}
			else {
				if (m_CanDoubleJump) {
					m_CanDoubleJump = false;
					m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
					m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpPower));
				}
			}
		}
	}
}
