using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLevel : MonoBehaviour {
	[SerializeField] Transform levelToMoveTo;
	[SerializeField] Camera mc;
	private LevelSwitcher switcher;

	private void Start() {
		switcher = GameObject.Find("SceneManager").GetComponent<LevelSwitcher>();
		mc = Camera.main;
		levelToMoveTo = transform.GetChild(0);
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag == "Player") {
			GameObject player = collision.gameObject;
			player.transform.position = levelToMoveTo.position;
			mc.transform.position = switcher.GetNextStage().position;
		}
	}
}
