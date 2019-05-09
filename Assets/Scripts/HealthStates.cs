using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStates : MonoBehaviour {
	[SerializeField] private int hp = 3; //hitpoints
	[SerializeField] private int maxHp = 5; //max hitpoints
	private GameObject healthText;

	private void Awake() {
		healthText = GameObject.Find("Canvas/HealthText");
	}

	public void SetHp(int value) {
		hp = value;
		if (hp > maxHp)
			hp = maxHp;
	}

	public void AddHp(int value) {
		hp += value;
		if (hp > maxHp)
			hp = maxHp;
	}

	public int GetHp() {
		return hp;
	}

	private void FixedUpdate() {
		//Display hp
		healthText.GetComponent<Text>().text = "HP: " + hp;

		//if hp drops below 0, load game over and destroy player
		if(hp <= 0) {
			GameObject.Find("SceneTrigger").GetComponent<SceneSwitcher>().LoadGameOver();
			Destroy(gameObject);
		}
	}
}
