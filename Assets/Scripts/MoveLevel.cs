using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLevel : MonoBehaviour {
	[SerializeField] Transform levelToMoveTo;
	[SerializeField] Transform cameraSpot;
	[SerializeField] Camera mc;

	Timer time;

	private void Start() {
		mc = Camera.main;
		levelToMoveTo = transform.GetChild(0);
		cameraSpot = transform.GetChild(1);
		time = GameObject.Find("TimerObject").GetComponent<Timer>();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag == "Player") {
			GameObject player = collision.gameObject;
			player.GetComponent<Inventory>().AddScore(Mathf.RoundToInt(time.time) * -2); 
			player.transform.position = levelToMoveTo.position;
			mc.transform.position = cameraSpot.position;
		}
	}
}
