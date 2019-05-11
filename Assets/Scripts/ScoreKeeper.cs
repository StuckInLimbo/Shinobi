using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
	[SerializeField] private int totalScore;
	[SerializeField] private float totalTime;

	private void Start() {
		DontDestroyOnLoad(gameObject);
	}

	public void Set(int _score, float _time) {
		totalScore = _score;
		totalTime = _time;
	}

	public void Set(int _score) {
		totalScore = _score;
		totalTime = 0;
	}

	public void Set(float _time) {
		totalScore = 0;
		totalTime = _time;
	}

	public float GetTime() {
		return totalTime;
	}

	public int GetScore() {
		return totalScore;
	}
}
