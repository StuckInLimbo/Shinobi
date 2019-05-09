using UnityEngine;


public class Inventory : MonoBehaviour {
	[SerializeField] private ScoreKeeper score;
	[SerializeField] private HealthStates health;

	private void Start() {
		GameObject scoreObj = GameObject.Find("ScoreObject");
		score = scoreObj.GetComponent<ScoreKeeper>();
		health = scoreObj.GetComponent<HealthStates>();
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
