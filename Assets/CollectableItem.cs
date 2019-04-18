using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour {
	[SerializeField] private int m_Value;

	private Inventory inventory;

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			inventory = other.GetComponent<Inventory>();
			inventory.AddScore(m_Value);
			Destroy(gameObject);
		}
	}
}
