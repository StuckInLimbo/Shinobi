using UnityEngine;

public class EnemyProjectile : MonoBehaviour {
	[SerializeField] private int deduction = 50;
	[SerializeField] private string[] tagList = { "Collectable", "Enemy", "Spawner"};

	private void OnTriggerEnter2D(Collider2D other) {
		
		if (CheckList(other) == true){
			//do nothing
		}
		else if (other.tag == "Player") {
			other.GetComponent<Inventory>().AddScore(-deduction);
			other.GetComponent<HealthStates>().AddHp(-1);
			Destroy(gameObject);
			Debug.Log("Hit player!");
		}
		else {
			Destroy(gameObject);
		}
	}

	private bool CheckList(Collider2D collider) {
		for(int i = 0; i < tagList.Length; i++) {
			if(tagList[i].Equals(collider.tag)) {
				return true;
			}
		}
		return false;
	}
}
