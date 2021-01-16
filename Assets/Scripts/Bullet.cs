using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct bulletType {
	public string bulletName;
	public float speed;

    public bulletType (string n, float s) {
		this.bulletName = n;
		this.speed = s;
    }
}

public static class Bullets {
	public static int PLASMA = 	0;
	public static int FIRE = 	1;
	public static int METAL = 	2;
	public static int ACID = 	3;

    public static bulletType[] bullets = new bulletType[] {
        //            	Name	 	Speed
        new bulletType("Plasma", 	150f),
        new bulletType("Fire", 		100f),
        new bulletType("Metal", 	200f),
        new bulletType("Acid", 		120f),
    };
}

public class Bullet : MonoBehaviour {
	private string bulletName;
	private float speed;
	int currentBulletType;
	Player player = null;
	float deathTime = 3;

	void FixedUpdate() {
		deathTime -= Time.deltaTime;
		if (deathTime <= 0) { Destroy(gameObject); }
	}

	public void SetBulletType(Player p, int i) {
		player = p;
		currentBulletType = i;
		bulletType cbt = Bullets.bullets[currentBulletType];
		this.bulletName = cbt.bulletName;
		this.speed = cbt.speed;
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Energy") {
			player.AddEnergy(1);
			Destroy(collision.gameObject);
			Destroy(gameObject);
		}

		// if (collision.gameObject.tag == "Chunk" || collision.gameObject.GetComponent<Meteor>()) {
		// 	Destroy(gameObject);
		// }
	}
	private void OnCollisionStay(Collision collision) {
		if (collision.gameObject.tag == "Chunk" || collision.gameObject.GetComponent<Meteor>()) {
			Destroy(gameObject);
		}
	}

	public float GetSpeed() { return this.speed; }
}
