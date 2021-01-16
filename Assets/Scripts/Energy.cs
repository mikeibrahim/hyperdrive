using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour {
	Player player;
	float deathTime = 15;

    void Start() {
        player = GameObject.FindObjectOfType<Player>();
    }

	void FixedUpdate() {
		deathTime -= Time.deltaTime;
		if (deathTime <= 0 && player.gameObject.activeInHierarchy) { Destroy(gameObject); }
	}

	private void OnCollisionStay(Collision collision) {
		if (collision.gameObject.tag == "Chunk") {
			Vector3 temp = transform.position;
			temp.x *= 0.75f;
			temp.z += 5;
			transform.position = temp;
		}
	}
}
