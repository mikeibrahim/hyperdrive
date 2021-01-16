using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorGenerator : MonoBehaviour {
	[SerializeField] private Meteor meteor = null;
	private float minDist = 10;
	private float maxDist = 45;
	private float currDist;
	Player player;
	float spawnInterval;

    void Start() {
		player = GameObject.FindObjectOfType<Player>();
    }

    void FixedUpdate() {
        if (spawnInterval <= 0 && player.gameObject.activeInHierarchy) {
			Meteor meteorInst = Instantiate(meteor);
			Vector3 tempPos = new Vector3(player.transform.position.x, 0, player.transform.position.z + CalcDist());
			meteorInst.transform.position = tempPos;
			NewSpawnInterval();
		} else {
			spawnInterval -= Time.deltaTime;
		}
    }

	private void NewSpawnInterval() {
		spawnInterval = Random.Range(1f, 4f);
	}

	private float CalcDist() {
		return currDist = Random.Range(minDist, maxDist);
	}
}
