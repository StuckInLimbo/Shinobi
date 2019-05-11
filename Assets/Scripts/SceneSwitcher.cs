using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
	[SerializeField] private int GameOverIndex = 4;

	public void LoadFirstScene() {
		Cursor.visible = false;
		SceneManager.LoadScene("Scene_1");
	}

	public void LoadDebugScene() {
		Cursor.visible = false;
		SceneManager.LoadScene("TestScene");
	}

	public void LoadMainMenu() {
		Cursor.visible = true;
		SceneManager.LoadScene("MainMenu");
	}

	public void LoadNextScene() {
		if((SceneManager.GetActiveScene().buildIndex + 1) == GameOverIndex) {
			Cursor.visible = true;
		}
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
			Timer timer = collision.GetComponent<Timer>(); //Get Timer comp
			Inventory inventory = collision.GetComponent<Inventory>(); //Get inventory comp
			ScoreKeeper sk = inventory.GetKeeper();
			sk.Set(inventory.GetScore(), timer.GetTotalTime()); //Send info to keeper comp
			LoadNextScene(); //Use when loading the next actual level
		}
	}
}
