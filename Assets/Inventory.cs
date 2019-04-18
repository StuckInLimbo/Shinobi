using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
	[SerializeField] private int m_Score;
	[SerializeField] private int m_Health;
	[SerializeField] private GameObject m_ScoreObject;

	public void AddScore(int value) {
		m_Score += value;
	}

	private void Update() {
		m_ScoreObject.GetComponent<Text>().text = string.Format("Score: {0}", m_Score);
	}
}
