using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
	[SerializeField] private int score;
	[SerializeField] private float totalTime;
	[SerializeField] private Text scoreText;

	private void Start() {
		DontDestroyOnLoad(gameObject);
		scoreText = GameObject.Find("Canvas/ScoreText").GetComponent<Text>();
	}

	public void Set(int _score, float _time) {
		score = _score;
		totalTime = _time;
	}

	public void Set(int _score) {
		score = _score;
		totalTime = 0;
	}

	public void Set(float _time) {
		score = 0;
		totalTime = _time;
	}

	public float GetTime() {
		return totalTime;
	}

	public int GetScore() {
		return score;
	}

	public void UpdateScore() {
		scoreText.text = string.Format("Score: {0:D0}", score);
	}
}
