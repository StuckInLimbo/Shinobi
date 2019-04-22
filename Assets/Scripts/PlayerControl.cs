using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //rbody2d is required for many functions
public class PlayerControl : MonoBehaviour {
	[SerializeField] private float minSpeed = -15f;             //Min Running Speed
	[SerializeField] private float curSpeed = 0f;               //Current Running Speed
	[SerializeField] private float maxSpeed = 15f;              //Max Running Speed
	[SerializeField] private float accelerationRate = 200f;     //Max Running Speed
	[SerializeField] private float jumpForce = 500f;            //Jump Height
	[SerializeField] private float dashDistance = 5f;			//Dash Length
	[SerializeField] private bool canDoubleJump = true;         //Is player able to double jump?
	[SerializeField] private bool canSlide = true;              //Is player able to slide?
	[SerializeField] private bool canDash = true;               //Is player able to dash?
	[SerializeField] private LayerMask groundLayerMask;         // A mask determining what is ground to the character

	[SerializeField] private bool isGrounded;            //Whether or not the player is grounded.
	private Rigidbody2D rBody;			//Rigidbody2D component
	private bool jump = false;			//Jump button(s) pressed?
	private bool dash = false;			//Dash button(s) pressed?
	private bool slide = false;			//Slide button(s) pressed?
	private double dashCooldown = 1.5;	//Dash cooldown in seconds
	private double timpStamp;			//Timestamp for dash cooldown
	private bool lookingRight = true;	//Last direction faced

	void Awake() {
		rBody = GetComponent<Rigidbody2D>(); //Get Rigidbody2D component from Character
	}

	void Update() {
		//Checks a .01 unit area beneath the player for objects with the Ground layer
		if (Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f),
			new Vector2(transform.position.x + 0.5f, transform.position.y - 0.51f), groundLayerMask)) {
			isGrounded = true;
			canDoubleJump = true;
			canDash = true;
		}
		else {
			isGrounded = false;
		}

		//Gets input from player
		jump = Input.GetButtonDown("Jump"); //Space or W
		dash = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.E);
		
		//Sets the last look location, used for dashing in the same direction you are moving
		if (Input.GetKeyDown(KeyCode.A)) {
			lookingRight = false;
		}
		else if (Input.GetKeyDown(KeyCode.D)) {
			lookingRight = true;
		}
		//Speed = linear interp from current velocity to max speed @ acceleration rate, over the duration of deltatime * 2
		curSpeed = Mathf.Lerp(rBody.velocity.x, (Input.GetAxisRaw("Horizontal") * 50) * accelerationRate * Time.deltaTime, Time.deltaTime * 2);
		curSpeed = Mathf.Clamp(curSpeed, minSpeed, maxSpeed); //Clamps speed to prevent shooting off into space
		rBody.velocity = (new Vector2(curSpeed, rBody.velocity.y)); //Sets velocity to new velocity, AddForce wasn't working as intended.

		if (jump) { //If player has pushed jump
			if (isGrounded) { //And we are on the ground
				rBody.velocity = new Vector2(rBody.velocity.x, 0f); //Cancel any downward/upward movement
				rBody.AddForce(new Vector2(0f, jumpForce)); //Add jumpForce to current velocity
			}
			else { //We aren't on the ground
				if (canDoubleJump) { //We can jump a second time
					canDoubleJump = false; //Make sure we can't jump again
					rBody.velocity = new Vector2(rBody.velocity.x, 0f); //Cancel any downward/upward movement
					rBody.AddForce(new Vector2(0f, jumpForce)); //Add jumpForce to current velocity
				}
			}
		}

		if (dash && canDash && timpStamp <= Time.time) { //If we pressed dash and we can dash and it's not on cooldown

			timpStamp = Time.time + dashCooldown; //Set cooldown timestamp to current time

			//Setup point to cast from and what direction we are going to RayCast from
			Vector2 pointToCastFrom = new Vector2(transform.position.x + ((lookingRight == true) ? 0.5f : -0.5f), transform.position.y);
			Vector2 direction = (lookingRight ? Vector2.right : Vector2.left);
			//Create RayCast2D at position and angle from before, at dashDistance range
			RaycastHit2D hit = Physics2D.Raycast(pointToCastFrom, direction, dashDistance);

			if (hit.collider == false) { //we didn't hit anything
				//New position is 5 units in front/behind of us
				Vector2 newPos = new Vector2(transform.position.x + (lookingRight ? dashDistance : -dashDistance), transform.position.y);
				Debug.Log("Dashing from " + transform.position + " to " + newPos);
				transform.position = newPos; //Set new position
			}
			else if (hit.collider) { //hit something
				float newDist = 0;
				//New position is newDist units in front/behind of us
				newDist = Mathf.Abs(hit.point.x - pointToCastFrom.x);
				Vector2 newPos = new Vector2(transform.position.x + (lookingRight ? newDist : -newDist), transform.position.y);
				Debug.Log("Dashing from " + transform.position + " to " + newPos);
				transform.position = newPos; //Set new position
			}
			canDash = false;
		}
	}
}
