using UnityEngine;

public class CollectableItem : MonoBehaviour {
	[SerializeField] private int value; //Value of CollectableItem

	private Inventory inventory; //Player's inventory component

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			inventory = other.GetComponent<Inventory>(); //Gets comp from Player
			inventory.AddScore(value);
			Destroy(gameObject); //Removes self from the plane of existence
		}
	}
}
