using UnityEngine;


public class Inventory : MonoBehaviour {
	[SerializeField] private ScoreKeeper score;

	private void Start() {
		score = GameObject.Find("ScoreObject").GetComponent<ScoreKeeper>();
	}

	public void AddScore(int value) {
		score.Set(score.GetScore() + value);
		score.UpdateScore();
	}

	public void SetScore(int value) {
		score.Set(value);
		score.UpdateScore();
	}

	public int GetScore() {
		return score.GetScore();
	}

	public ScoreKeeper GetKeeper() {
		return score;
	}
}
