﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour {
	[SerializeField] private GameObject respawnPoint;

	private void Start() {
		GameObject respawnPoint = transform.GetChild(0).gameObject;
	}

	public void Respawn(Collider2D other) {
		other.GetComponent<HealthStates>().AddHp(-1);
		other.transform.position = respawnPoint.transform.position;
	}
}
