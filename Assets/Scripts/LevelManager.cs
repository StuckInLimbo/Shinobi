using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	[SerializeField] private EnemyManager[] managers;
	[SerializeField] private int currentLevel;
	[SerializeField] private int maxLevels;

	private void Awake() {
		managers = FindObjectsOfType<EnemyManager>();
		managers = SortArray(managers);
		
		currentLevel = 0;
		managers[currentLevel].SwitchTo();
		maxLevels = managers.Length-1;
	}

	public void AdvanceLevel() {
		int nextLevel = currentLevel + 1;

		//Debug.Log("Is Level " + currentLevel + " active? " + managers[currentLevel].levelActive);
		//Debug.Log("Is Level " + nextLevel + " active? " + managers[nextLevel].levelActive);

		if (currentLevel < maxLevels) {
			managers[currentLevel].SwitchFrom();
			managers[nextLevel].SwitchTo();

			//Debug.Log("Is Level " + currentLevel + " active? " + managers[currentLevel].levelActive);
			//Debug.Log("Is Level " + nextLevel + " active? " + managers[nextLevel].levelActive);
		}
		else if(currentLevel == maxLevels) {
			managers[currentLevel].SwitchFrom();
		}

		currentLevel++;
	}

	EnemyManager[] SortArray(EnemyManager[] arr) {
		EnemyManager level1 = null, level2 = null, level3 = null, level4 = null;

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
