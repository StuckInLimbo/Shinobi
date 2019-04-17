using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public GameObject textObject;
	public float time;
    // Start is called before the first frame update
    void Start() {
		if (textObject)
			return;
		else
			textObject = GameObject.Find("TimerText");
    }

    // Update is called once per frame
    void Update() {
		time += Time.deltaTime;
		TimeSpan timeSpan = TimeSpan.FromSeconds(time);
		string timeText = "";
		if (time >= 3600.0f) {
			timeText = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
		}
		else if (time >= 600.0f) {
			timeText = string.Format("{1:D2}:{2:D2}.{3:D3}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
		}
		else if (time >= 60.0f) {
			timeText = string.Format("{1:D1}:{2:D2}.{3:D3}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
		}
		else if (time >= 60.0f) {
			timeText = string.Format("{1:D1}:{2:D2}.{3:D3}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
		}
		else {
			timeText = string.Format("{2:D1}.{3:D3}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
		}
		textObject.GetComponent<Text>().text = "" + timeText;
    }
}
