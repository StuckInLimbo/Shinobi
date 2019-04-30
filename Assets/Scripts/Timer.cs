using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	private GameObject totalTimeObject;
	private GameObject splitTimeObject;
	private float totalTime;
	private float timeOnLevel;

    // Start is called before the first frame update
    void Start() {
		if (totalTimeObject)
			return;
		else
			totalTimeObject = GameObject.Find("Canvas/TimerText");
		if (splitTimeObject)
			return;
		else
			splitTimeObject = GameObject.Find("Canvas/SplitText");
	}

	public float GetTotalTime() {
		return totalTime;
	}

	public float GetSplitTime() {
		return timeOnLevel;
	}

    // Update is called once per frame
    void Update() {
		totalTime += Time.deltaTime;
		timeOnLevel += Time.deltaTime;

		string totalTimeText = Format(totalTime, false);
		string splitTimeText = Format(timeOnLevel, true);

		splitTimeObject.GetComponent<Text>().text = "" + splitTimeText;
		totalTimeObject.GetComponent<Text>().text = "" + totalTimeText;
    }

	private string Format(float time, bool isSplit) {
		TimeSpan span = TimeSpan.FromSeconds(time);
		string result = (isSplit ? "Split: " : "Total: ");
		if (time >= 3600.0f) {
			result = result + string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
		}
		else if (time >= 600.0f) {
			result = result + string.Format("{1:D2}:{2:D2}.{3:D3}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
		}
		else if (time >= 60.0f) {
			result = result + string.Format("{1:D1}:{2:D2}.{3:D3}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
		}
		else if (time >= 60.0f) {
			result = result + string.Format("{1:D1}:{2:D2}.{3:D3}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
		}
		else {
			result = result + string.Format("{2:D1}.{3:D3}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
		}
		return result;
	}

	public static string FormatString(float time, bool isSplit) {
		TimeSpan span = TimeSpan.FromSeconds(time);
		string result = (isSplit ? "Split: " : "Total: ");
		if (time >= 3600.0f) {
			result = result + string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
		}
		else if (time >= 600.0f) {
			result = result + string.Format("{1:D2}:{2:D2}.{3:D3}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
		}
		else if (time >= 60.0f) {
			result = result + string.Format("{1:D1}:{2:D2}.{3:D3}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
		}
		else if (time >= 60.0f) {
			result = result + string.Format("{1:D1}:{2:D2}.{3:D3}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
		}
		else {
			result = result + string.Format("{2:D1}.{3:D3}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
		}
		return result;
	}

	public void ResetTime() {
		timeOnLevel = 0.0f;
	}
}
