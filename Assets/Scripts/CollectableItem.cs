using UnityEngine;

public class CollectableItem : MonoBehaviour {
	[SerializeField] private int value; //Value of CollectableItem
	[SerializeField] private AudioSource collectSound;
	private Inventory inventory; //Player's inventory component

	private void Start() {
		collectSound = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			inventory = other.GetComponent<Inventory>(); //Gets comp from Player
			collectSound.Play();
			Destroy(gameObject); //Removes self from the plane of existence
		}
	}
}
