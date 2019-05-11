using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageHazard : MonoBehaviour {
	[SerializeField] private GameObject parent;

	private void Start() {
		parent = transform.parent.gameObject.transform.parent.gameObject; 
		//because the hazards are setup as /Hazard/Water/Water, /Hazards/Gas/Gas, etc.
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			parent.GetComponent<RespawnPlayer>().Respawn(other);
		}
	}
}
