using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	[SerializeField] private EnemyManager[] managers;
	[SerializeField] private int currentLevel;
	[SerializeField] private int maxLevels;

	private void Awake() {
		managers = FindObjectsOfType<EnemyManager>(); //apparently out of order, go figure.
		managers = SortArray(managers); //lets sort this trash
		
		currentLevel = 0; //init
		managers[currentLevel].SwitchTo(); //set current level as active
		maxLevels = managers.Length-1; //make sure we have an upperbounds for the for loop
	}

	///Changes current level from the managers to the next one, 
	///activates and deactivates specific levels
	public void AdvanceLevel() {
		if (currentLevel < maxLevels) {
			managers[currentLevel].SwitchFrom(); //Current level inactive
			managers[currentLevel+1].SwitchTo(); //Next level active
		}
		else if(currentLevel == maxLevels) {
			managers[currentLevel].SwitchFrom(); //Set current level as inactive
		}

		currentLevel++; //Set the next level ID as current 
	}

	///This is one giant mess, but the only consistent way I found of sorting the levels,
	///as unity builds were completely fucked, thanks prefabs : ^)
	EnemyManager[] SortArray(EnemyManager[] arr) {
		EnemyManager level1 = null, level2 = null, level3 = null, level4 = null;
		//Seriously, this method sucked, kill me
		for(int i = 0; i < arr.Length; i++) {
			switch(arr[i].levelId) {
				case 1:
				level1 = arr[i];
				break;
				case 2:
				level2 = arr[i];
				break;
				case 3:
				level3 = arr[i];
				break;
				case 4:
				level4 = arr[i];
				break;
				default:
				level1 = arr[i];
				break;
			}
		}
		EnemyManager[] result = { level1, level2, level3, level4 };
		return result;
	}
}
