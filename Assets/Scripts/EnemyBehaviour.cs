using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private float projectileSpeed = 3f;
	[SerializeField] private float cooldown = 1.5f;
	[SerializeField] private bool facingRight;

	private Rigidbody2D rBody;
	private float timeStamp;

	void Shoot() {
		Vector2 direction = new Vector2(transform.position.x + (facingRight ? 0.5f : -0.5f), transform.position.y);
		GameObject clone = Instantiate(projectilePrefab, direction, Quaternion.identity);
		rBody = clone.GetComponent<Rigidbody2D>();
		rBody.velocity = new Vector2((facingRight ? projectileSpeed : -projectileSpeed), 0f);
	}

	private void Update() {
		if (timeStamp <= Time.time) {
			timeStamp = Time.time + cooldown;
			Shoot();
		}
	}
}
