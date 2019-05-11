using UnityEngine;


public class Inventory : MonoBehaviour {
	[SerializeField] private ScoreKeeper score;

	private void Start() {
		GameObject scoreObj = GameObject.Find("ScoreObject");
		score = scoreObj.GetComponent<ScoreKeeper>();
	}

	public void AddScore(int value) {
		score.Set(score.GetScore() + value);
	}

	public void SetScore(int value) {
		score.Set(value);
	}

	public int GetScore() {
		return score.GetScore();
	}

	public ScoreKeeper GetKeeper() {
		return score;
	}
}
