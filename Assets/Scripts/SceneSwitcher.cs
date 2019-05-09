using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
	public void LoadFirstScene() {
		SceneManager.LoadScene("Level1");
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
			inventory.GetKeeper().Set(inventory.GetScore(), timer.GetTotalTime()); //Send info to keeper comp
			Destroy(collision.gameObject);
			LoadNextScene(); //Use when loading the next actual level
		}
	}
}
