﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //rbody2d is required for many functions
public class PlayerControl : MonoBehaviour {
	[SerializeField] private float minSpeed = -15f;         //Min Running Speed
	[SerializeField] private float curSpeed = 0f;           //Current Running Speed
	[SerializeField] private float maxSpeed = 15f;          //Max Running Speed
	[SerializeField] private float accelerationRate = 200f;	//Max Running Speed
	[SerializeField] private float jumpForce = 500f;        //Jump Height
	[SerializeField] private float dashDistance = 5f;       //Dash Length
	[SerializeField] private double dashCooldown = 2.5f;	//Dash cooldown in seconds
	[SerializeField] private bool canDoubleJump = true;     //Is player able to double jump?
	[SerializeField] private bool isGrounded = false;		//Whether or not the player is grounded.
	//[SerializeField] private bool canSlide = true;          //Is player able to slide?
	[SerializeField] private bool canDash = true;           //Is player able to dash?
	[SerializeField] private LayerMask layerMask = 256;		// A mask to determine ground to the Controller

	private Rigidbody2D rBody;          //Rigidbody2D component
	private Animator anim;              //Animator component
	private float movement = 0.0f;		//Horizontal axis pressed?
	private bool jump = false;			//Jump button(s) pressed?
	private bool dash = false;			//Dash button(s) pressed?
	//private bool slide = false;			//Slide button(s) pressed?
	private double timeStamp;			//Timestamp for dash cooldown
	private bool lookingRight = true;	//Last direction faced

	void Awake() {
		rBody = GetComponent<Rigidbody2D>(); //Get Rigidbody2D component from Character
		anim = GetComponent<Animator>(); //Get Animator component from Character
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();

		//Checks a .01 unit area beneath the player for objects with the Ground layer
		if (Physics2D.OverlapArea(new Vector2(transform.position.x - 0.55f, transform.position.y - 0.8f),
			new Vector2(transform.position.x + 0.55f, transform.position.y - 0.81f), layerMask)) {
			//Set all ready states to true
			isGrounded = true;
			canDoubleJump = true;
			canDash = true;
			//Set anim states to false
			anim.SetBool("Jumping", false); 
			anim.SetBool("Dashing", false);
		}
		else {
			isGrounded = false;
		}

		//Gets input from player
		jump = Input.GetButtonDown("Jump");
		dash = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.E);
		movement = Input.GetAxis("Horizontal");

		//Sets the last look location, used for dashing in the same direction you are moving
		if (movement < 0 && lookingRight) {
			Flip();
		}
		else if (movement > 0 && !lookingRight) {
			Flip();
		}

		//Speed = linear interp from current velocity to max speed @ acceleration rate, over the duration of deltatime
		curSpeed = Mathf.Lerp(rBody.velocity.x, movement * accelerationRate, 0.15f);
		curSpeed = Mathf.Clamp(curSpeed, minSpeed, maxSpeed); //Clamps velocity to prevent shooting off into space like Team Rocket
		anim.SetFloat("Speed", Mathf.Abs(curSpeed));
		rBody.velocity = (new Vector2(curSpeed, rBody.velocity.y)); //Sets velocity to new velocity, AddForce wasn't working as intended.

		if (jump) { //If player has pushed jump
			if (isGrounded) { //And we are on the ground
				anim.SetBool("Jumping", true);
				rBody.velocity = new Vector2(rBody.velocity.x, 0f); //Cancel any downward/upward movement
				rBody.AddForce(new Vector2(0f, jumpForce)); //Add jumpForce to current velocity
			}
			else { //We aren't on the ground
				if (canDoubleJump) { //We can jump a second time	
					anim.SetBool("Jumping", true);
					canDoubleJump = false; //Make sure we can't jump again
					rBody.velocity = new Vector2(rBody.velocity.x, 0f); //Cancel any downward/upward movement
					rBody.AddForce(new Vector2(0f, jumpForce)); //Add jumpForce to current velocity
				}
			}
		}

		if (dash && canDash && timeStamp <= Time.time) { //If we pressed dash and we can dash and it's not on cooldown

			timeStamp = Time.time + dashCooldown; //Set cooldown timestamp to current time
			anim.SetBool("Dashing", true);
			//Setup point to cast from and what direction we are going to RayCast from
			Vector2 pointToCastFrom = new Vector2(transform.position.x + (lookingRight ? 0.5f : -0.5f), transform.position.y);
			Vector2 direction = (lookingRight ? Vector2.right : Vector2.left);
			//Create RayCast2D at position and angle from before, at dashDistance range
			RaycastHit2D hit = Physics2D.Raycast(pointToCastFrom, direction, dashDistance);

			if (hit.collider == false) { //we didn't hit anything
				//New position is 5 units in front/behind of us
				Vector2 newPos = new Vector2(transform.position.x + (lookingRight ? dashDistance : -dashDistance), transform.position.y);
				//Debug.Log("Dashing from " + transform.position + " to " + newPos);
				transform.position = newPos; //Set new position
			}
			else if (hit.collider) { //hit something
				float newDist = 0;
				//New position is newDist units in front/behind of us
				newDist = Mathf.Abs(hit.point.x - pointToCastFrom.x);
				Vector2 newPos = new Vector2(transform.position.x + (lookingRight ? newDist : -newDist), transform.position.y);
				//Debug.Log("Dashing from " + transform.position + " to " + newPos);
				transform.position = newPos; //Set new position
			}
			canDash = false;
		}
	}

	//Flips the sprite to make it look like the sprite has turned around
	private void Flip() {
		lookingRight = !lookingRight;

		Vector2 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	//Wallhold anim state, might be borked
	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Wall")) {
			anim.SetBool("Holding", true);

			Flip();
		}
	}

	private void OnCollisionExit2D(Collision2D collision) {
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Wall")) {
			anim.SetBool("Holding", false);

			Flip();
		}
	}
}
