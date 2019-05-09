using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages all child EnemyBehaviour objects via an array holding all 
/// children of our gameObject
/// </summary>
public class EnemyManager : MonoBehaviour {
	[SerializeField] public int levelId;
	[SerializeField] public bool levelActive;
	[SerializeField] GameObject[] enemies;

	void Start() {
		//levelActive = false;
		//Gets all children, adds them to the array
		enemies = new GameObject[transform.childCount];
		for (int i = 0; i < enemies.Length; i++) {
			enemies[i] = transform.GetChild(i).gameObject;
			//enemies[i].GetComponent<EnemyBehaviour>().Sleep();
		}
	}

	//Sets all enemies to an active state, and sets the active bool to true
	public void SwitchTo() {
		levelActive = true;
		for (int i = 0; i < enemies.Length; i++) {
			enemies[i].GetComponent<EnemyBehaviour>().WakeUp();
		}
	}

	//Sets all enemies to a sleep state, and sets the active bool to false
	public void SwitchFrom() {
		levelActive = false;
		for (int i = 0; i < enemies.Length; i++) {
			enemies[i].GetComponent<EnemyBehaviour>().Sleep();
		}
	}
}