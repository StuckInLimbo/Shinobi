﻿using UnityEngine;

public class EnemyProjectile : MonoBehaviour {
	[SerializeField] private string[] list = { "Collectable", "Enemy", "Spawner"};

	private void OnTriggerEnter2D(Collider2D other) {
		
		if (CheckList(other) == true){
			//do nothing
		}
		else if (other.tag == "Player") {
			other.GetComponent<Inventory>().AddScore(-50);
			Destroy(gameObject);
			Debug.Log("Hit player!");
		}
		else {
			Destroy(gameObject);
		}
	}

	private bool CheckList(Collider2D collider) {
		for(int i = 0; i < list.Length; i++) {
			if(list[i].Equals(collider.tag)) {
				return true;
			}
		}
		return false;
	}
}
