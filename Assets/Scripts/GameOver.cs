using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	private GameObject timeObject;
	private GameObject scoreObject;
	private GameObject keeper;

	// Start is called before the first frame update
	void Start()
    {
		keeper = GameObject.Find("ScoreObject");
		ScoreKeeper sk = keeper.GetComponent<ScoreKeeper>();
		timeObject = GameObject.Find("TotalTimeText");
		scoreObject = GameObject.Find("ScoreText");
		float totalTime = sk.GetTime();
		int score = sk.GetScore();
		string timeText = Timer.FormatString(totalTime, false);
		Destroy(keeper);
		timeObject.GetComponent<Text>().text = timeText;
		scoreObject.GetComponent<Text>().text = "Total Score: " + score;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
