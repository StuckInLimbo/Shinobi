using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private bool isAwake;
	[SerializeField] private float projectileSpeed = 3f;
	[SerializeField] private float cooldown = 1.5f;
	[SerializeField] private float offset = 0.0f; //when setting, use 1/2 of cooldown
	[SerializeField] private bool facingRight;
	[SerializeField] private bool horizontalShooting;
	[SerializeField] private bool verticalShootUp;

	private Rigidbody2D rBody;
	private float timeStamp;

	private void Awake() {
		timeStamp = Time.time + offset;
	}

	void Shoot() {
		float yPostition, xPosition, yVelocity, xVelocity;
		if (horizontalShooting == false) {
			yPostition = (verticalShootUp ? 0.5f : -0.5f);
			xPosition = 0.0f;
			yVelocity = (facingRight ? projectileSpeed : -projectileSpeed);
			xVelocity = 0.0f;
		}
		else {
			yPostition = 0;
			xPosition = (facingRight ? 0.5f : -0.5f);
			yVelocity = 0.0f;
			xVelocity = (facingRight ? projectileSpeed : -projectileSpeed);
		}

		Vector2 direction = new Vector2(transform.position.x + xPosition, transform.position.y + yPostition);
		GameObject clone = Instantiate(projectilePrefab, direction, Quaternion.identity);
		rBody = clone.GetComponent<Rigidbody2D>();
		rBody.velocity = new Vector2(xVelocity, yVelocity);
	}

	private void FixedUpdate() {
		if (timeStamp <= Time.time && isAwake) {
			timeStamp = Time.time + cooldown;
			Shoot();
		}
	}

	public void WakeUp() {
		isAwake = true;
		timeStamp = Time.time + offset;
	}

	public void Sleep() {
		isAwake = false;
	}
}
