﻿using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
	[SerializeField] private int score; //Player's current score
	[SerializeField] private int health = 3; //Player's health (WIP)
	[SerializeField] private GameObject scoreObject; //GUI object

	public void AddScore(int value) {
		score += value;
	}

	private void Start() {
		DontDestroyOnLoad(gameObject);
	}

	private void Update() {
		scoreObject.GetComponent<Text>().text = string.Format("Score: {0:D0}", score);
	}
}
