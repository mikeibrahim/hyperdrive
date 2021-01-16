using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct gunType {
	public string gunName;
	public int bulletType;
	public float timeBtw;
	public bool auto;

    public gunType (string n, int b, float t, bool a) {
		this.gunName = n;
		this.bulletType = b;
		this.timeBtw = t;
		this.auto = a;
    }
}

public static class Guns {
	public static int BLASTER = 	0;
	public static int MACHINEGUN = 	1;
	public static int SNIPER = 		2;
	public static int LAZER = 		3;

    public static gunType[] guns = new gunType[] {
        //            	Name		Primary			TbwS	Auto
        new gunType("Blaster",		Bullets.PLASMA, 0.2f, 	false),
        new gunType("Machine Gun",	Bullets.FIRE, 	0.02f, 	true),
        new gunType("Sniper",		Bullets.METAL, 	0.3f, 	false),
        new gunType("Laser",		Bullets.ACID, 	0.03f, 	true)
    };
}

public class Gun : MonoBehaviour {
	[SerializeField] private Bullet[] bullet = null;
	private string gunName;
	private int bulletType;
	private float timeBtw;
	private bool auto;
	int currentGun;
	Transform playerPos;
	Player player;

	public void SetCurrentGun(Player p, int i) {
		player = p;
		currentGun = i;
		gunType cgt = Guns.guns[currentGun];
        gunName = cgt.gunName;
		bulletType = cgt.bulletType;
		timeBtw = cgt.timeBtw;
		auto = cgt.auto;
	}

	public void Shoot() {
		playerPos = transform.parent.gameObject.transform;
		Bullet bulletInst = Instantiate(bullet[bulletType], transform.position, playerPos.rotation);
		bulletInst.SetBulletType(player, Guns.guns[currentGun].bulletType);
		bulletInst.GetComponent<Rigidbody>().AddForce(transform.forward * bulletInst.GetSpeed() * player.GetVelocity());
	}

	public float  GetTimeBtw() { return timeBtw; }

	public bool GetAuto() { return auto; }
}
