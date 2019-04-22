using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour {
	[SerializeField] private int amountOfLevels;
	[SerializeField] Transform[] camPositions;
	[SerializeField] private int currentLevel;

    // Start is called before the first frame update
    void Start() {
		currentLevel = 0;
		GameObject[] list = GameObject.FindGameObjectsWithTag("CamPos");
		int x = 0;
		camPositions = new Transform[list.Length];
		foreach (GameObject g in list) {
			camPositions[x] = g.transform;
			x++;
		}
		//Array.Reverse(camPositions); //make sure the objects are in order in the scene
		amountOfLevels = camPositions.Length;
    }

	public Transform GetNextStage() {
		currentLevel++;
		return camPositions[currentLevel];
	}
}
