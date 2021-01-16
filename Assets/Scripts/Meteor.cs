using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {
	private Player player;
	[SerializeField] private SpriteRenderer redCircle = null;
	[SerializeField] private Collider coll = null;
	private float maxSize;
	private float growSpeed = 0.5f;
	private Color32 beginColor = new Color32(0, 0, 0, 0);
	private Color32 endColor = new Color32(255, 0, 35, 255);
	private Color32 blowUpColor = new Color32(255, 0, 255, 255);
	private float deathTime = 10;
	float currSize;

    void Start() {
        player = GameObject.FindObjectOfType<Player>();
		transform.localScale = new Vector3(0,0,0);
		maxSize = Random.Range(40, 120);
    }

    void FixedUpdate() {
        if (transform.localScale.x < maxSize) {
			coll.enabled = false;
			float growAmount = Mathf.Lerp(0, maxSize, currSize);
			redCircle.color = Color.Lerp(beginColor, endColor, currSize);
			transform.localScale = new Vector3(growAmount, growAmount, growAmount);
		} else {
			coll.enabled = true;
			redCircle.color = blowUpColor;
		}
		currSize += growSpeed * Time.deltaTime;

		deathTime -= Time.deltaTime;
		if (deathTime <= 0 && player.gameObject.activeInHierarchy) { Destroy(gameObject); }
    }
}
