using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	[SerializeField] private float totalTime;
	[SerializeField] private float timeOnLevel;

    /// Start is called before the first frame update
    void Start() {
		GameObject score = GameObject.Find("ScoreObject");
		if (score != null) {
			totalTime = score.GetComponent<ScoreKeeper>().GetTime();
		}
	}

	public float GetTotalTime() {
		return totalTime;
	}

	public void SetTotalTime(float value) {
		totalTime = value;
	}

	public float GetSplitTime() {
		return timeOnLevel;
	}
	void Update() {
		totalTime += Time.deltaTime;
		timeOnLevel += Time.deltaTime;
	}

	/// Local method to format time, can format in total or split variations
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

	/// Public method to format time, can format in total or split variations
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

	/// Sets split time to 0f
	public void ResetTime() {
		timeOnLevel = 0.0f;
	}
}
