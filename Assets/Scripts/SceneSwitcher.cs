using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
	public void GotoFirstScene() {
		SceneManager.LoadScene("Level1");
	}

	public void GotoTestScene() {
		Cursor.visible = false;
		SceneManager.LoadScene("TestScene");
	}

	public void GotoMenuScene() {
		Cursor.visible = true;
		SceneManager.LoadScene("MainMenu");
	}

	public void LoadNextScene() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void LoadGameOver() {
		Cursor.visible = true;
		SceneManager.LoadScene("EndGameMenu");
	}

	public void QuitGame() {
		Application.Quit();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag == "Player") {
			GameObject scoreObj = GameObject.Find("ScoreObject");
			GameObject timer = GameObject.Find("TimerObject");
			scoreObj.GetComponent<ScoreKeeper>().Set(collision.GetComponent<Inventory>().GetScore(), timer.GetComponent<Timer>().GetTotalTime());
			//LoadNextScene(); //Use when loading the next actual level
			LoadGameOver();
		}
	}
}
