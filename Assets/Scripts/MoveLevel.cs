using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLevel : MonoBehaviour {
	[SerializeField] LevelManager manager;
	[SerializeField] Transform levelToMoveTo;
	[SerializeField] Transform cameraSpot;
	[SerializeField] Camera mc;

	private void Start() {
		mc = Camera.main;
		levelToMoveTo = transform.GetChild(0);
		cameraSpot = transform.GetChild(1);
		manager = GameObject.Find("Environment").GetComponent<LevelManager>();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag == "Player") {
			Timer timer = collision.gameObject.GetComponent<Timer>();
			Inventory inventory = collision.gameObject.GetComponent<Inventory>();
			inventory.AddScore(Mathf.RoundToInt(timer.GetSplitTime()) * -2);
			manager.AdvanceLevel();
			timer.ResetTime();
			collision.transform.position = levelToMoveTo.position;
			mc.transform.position = cameraSpot.position;
		}
	}
}
