using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
	[SerializeField] public int levelId;
	[SerializeField] public bool levelActive;
	[SerializeField] GameObject[] enemies;

	void Start() {
		//levelActive = false;
		enemies = new GameObject[transform.childCount];
		for (int i = 0; i < enemies.Length; i++) {
			enemies[i] = transform.GetChild(i).gameObject;
			//enemies[i].GetComponent<EnemyBehaviour>().Sleep();
		}
	}

	public void SwitchTo() {
		levelActive = true;
		for (int i = 0; i < enemies.Length; i++) {
			enemies[i].GetComponent<EnemyBehaviour>().WakeUp();
		}
	}

	public void SwitchFrom() {
		levelActive = false;
		for (int i = 0; i < enemies.Length; i++) {
			enemies[i].GetComponent<EnemyBehaviour>().Sleep();
		}
	}
}