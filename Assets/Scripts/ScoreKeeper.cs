using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {
	[SerializeField] private int score;
	[SerializeField] private float totalTime;

	private void Start() {
		DontDestroyOnLoad(gameObject);
	}

	public void Set(int _score, float _time) {
		score = _score;
		totalTime = _time;
	}

	public float GetTime() {
		return totalTime;
	}

	public int GetScore() {
		return score;
	}
}
