using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour {
	[SerializeField] private Text scoreObj;
	[SerializeField] private Text timerObj;
	[SerializeField] private Text healthObj;
	[SerializeField] private Text splitObj;

	private GameObject player;
	private Timer time;
	private Inventory inv;
	private HealthStates health;

	private void Start() {
		scoreObj = GameObject.Find("ScoreText").GetComponent<Text>();
		timerObj = GameObject.Find("TimerText").GetComponent<Text>();
		splitObj = GameObject.Find("SplitText").GetComponent<Text>();
		healthObj = GameObject.Find("HealthText").GetComponent<Text>();
		player = GameObject.Find("NinjaPlayer");
		time = player.GetComponent<Timer>();
		inv = player.GetComponent<Inventory>();
		health = player.GetComponent<HealthStates>();
	}

	private void FixedUpdate() {
		UpdateUi();
	}

	public void UpdateUi() {
		scoreObj.text = string.Format("Score: {0:D0}", inv.GetScore());
		timerObj.text = Timer.FormatString(time.GetTotalTime(), false);
		splitObj.text = Timer.FormatString(time.GetSplitTime(), false);
		healthObj.text = "HP: " + health.GetHp();
	}
}
