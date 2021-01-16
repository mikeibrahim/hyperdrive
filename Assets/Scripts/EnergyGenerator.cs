using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGenerator : MonoBehaviour {
	[SerializeField] private GameObject energy = null;
	private float minDist = 100;
	private float maxDist = 150;
	private float currDist;
	Player player;
	float spawnInterval;

    void Start() {
		player = GameObject.FindObjectOfType<Player>();
    }

    void FixedUpdate() {
        if (spawnInterval <= 0 && player.gameObject.activeInHierarchy) {
			GameObject energyInst = Instantiate(energy);
			Vector3 tempPos = new Vector3(player.transform.position.x, 0, player.transform.position.z + CalcDist());
			energyInst.transform.position = tempPos;
			NewSpawnInterval();
		} else {
			spawnInterval -= Time.deltaTime;
		}
    }

	private void NewSpawnInterval() {
		spawnInterval = Random.Range(2f, 4f);
	}

	private float CalcDist() {
		return currDist = Random.Range(minDist, maxDist);
	}
}
