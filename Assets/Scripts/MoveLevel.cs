using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLevel : MonoBehaviour {
	[SerializeField] Transform levelToMoveTo;
	[SerializeField] Transform cameraSpot;
	[SerializeField] Camera mc;

	Timer timer;

	private void Start() {
		mc = Camera.main;
		levelToMoveTo = transform.GetChild(0);
		cameraSpot = transform.GetChild(1);
		timer = GameObject.Find("TimerObject").GetComponent<Timer>();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag == "Player") {
			GameObject player = collision.gameObject;
			player.GetComponent<Inventory>().AddScore(Mathf.RoundToInt(timer.GetSplitTime()) * -2);
			timer.ResetTime();
			player.transform.position = levelToMoveTo.position;
			mc.transform.position = cameraSpot.position;
		}
	}
}
