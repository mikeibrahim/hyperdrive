using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {
	// [SerializeField] private Collider boxCollider = null;
	[SerializeField] private Transform spawnPoint = null;
	Player player;
	float deathTime = 15f;
	ChunkGenerator chunkGen;

	void Start() { 
		player = GameObject.FindObjectOfType<Player>();
		chunkGen = GameObject.FindObjectOfType<ChunkGenerator>(); 
	}

	void FixedUpdate() {
		deathTime -= Time.deltaTime;
		if (deathTime <= 0 && player.gameObject.activeInHierarchy) { Destroy(gameObject); }
	}

    private void OnTriggerEnter(Collider other) {
		if (other.GetComponent<Player>()) {
			GameObject chunkInst = Instantiate(chunkGen.GetChunk().gameObject, spawnPoint.position, transform.rotation);
			// GameObject volumeInst = Instantiate(chunkGen.GetVolume(), transform.position, transform.rotation);
		}
	}
}
