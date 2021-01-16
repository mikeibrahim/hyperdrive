using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct playerType {
    public string playerName;
	public int gun;
	public float speed;
	public float dashTime;
	public float dashSpeed;

    public playerType (string n, int p, float s, float d, float ds) {
        this.playerName = n;
		this.gun = p;
		this.speed = s;
		this.dashTime = d;
		this.dashSpeed = ds;
    }
}

public static class Players {
	public static int SPACESHIP = 	0;
	public static int FIGHTER = 	1;
	public static int CARRIER = 	2;
	public static int MOTHERSHIP = 	3;

    public static playerType[] players = new playerType[] {
        //            	Name			Gun 				Speed	Dash 	DSpeed
        new playerType("SpaceShip",		Guns.BLASTER,		15f,	3f,		30f),
        new playerType("Fighter",		Guns.MACHINEGUN,	18f,	2f,		60f),
        new playerType("Carrier",		Guns.SNIPER,		13f,	4.5f,	20f),
        new playerType("Mothership",	Guns.LAZER,			17f,	3.5f,	40f),
    };
}

public class Player : MonoBehaviour {
	[SerializeField] private CharacterController controller = null;
	[SerializeField] private Gun gun = null;
	private string playerName;
	private float speed;
	private float dashTime;
	private float dashSpeed;
	float currSpeed;
	float turnSpeed = 15;
	float interTurn;
	bool arrowR;
	bool isDashing;
	float currentTbw;
	GameUI gameUI;
	float score;
	GameObject volume;
	Vector3 noPost= new Vector3(-1.5f, 0, 0);
	Vector3 post = new Vector3(0, 0, 0);

	public void MakePlayer(playerType p) {
		playerName = p.playerName;
		speed = p.speed;
		dashTime = p.dashTime;
		dashSpeed = p.dashSpeed;
		currSpeed = speed;
		gun.SetCurrentGun(this, p.gun);
	}

	void Start() {
		gameUI = GameObject.FindObjectOfType<GameUI>();
		gameUI.SetMaxEnergy(dashTime);

		volume = GameObject.Find("PlayerVolume");
	}

    void Update() {
		score += Mathf.Round(130 * Time.deltaTime);
		gameUI.SetScore(score);

		#region Shoot
		var fullAuto = gun.GetAuto() ? Input.GetKey(KeyCode.Space) : Input.GetKeyDown(KeyCode.Space);
		if (fullAuto && !isDashing && currentTbw <= 0) { 
			gun.Shoot(); 
			currentTbw = gun.GetTimeBtw();
		} else {
			currentTbw -= Time.deltaTime;
		}
		#endregion

		#region Turns
        if (Input.GetKeyDown(KeyCode.RightArrow) && !arrowR) {
			arrowR = true;
			interTurn = 0;
		} else if (Input.GetKeyDown(KeyCode.LeftArrow) && arrowR) {
			arrowR = false;
			interTurn = 0;
		}

		float dir = arrowR ? 45 : -45;
		float currTurn = transform.rotation.y;
		transform.rotation = Quaternion.Euler(0, Mathf.Lerp(currTurn, dir, interTurn), 0);
		interTurn += Time.deltaTime * turnSpeed;
		controller.Move(transform.forward * currSpeed * Time.deltaTime);
		#endregion
		
		#region Clamping
		if (transform.position.x > 17) {
			transform.position = new Vector3(17, transform.position.y, transform.position.z);
		} else if (transform.position.x < -17) {
			transform.position = new Vector3(-17, transform.position.y, transform.position.z);
		}
		#endregion

		#region Dashing
		isDashing = Input.GetKey(KeyCode.LeftShift) && gameUI.GetEnergy() > 0;

		if (isDashing) {
			currSpeed = dashSpeed;
			transform.position = new Vector3(transform.position.x, -10, transform.position.z); 
			gameUI.AccumulateEnergy(-Time.deltaTime);
		} else {
			currSpeed = speed;
			transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		}
		volume.transform.localPosition = Vector3.MoveTowards(
			volume.transform.localPosition, 
			isDashing ? post : noPost, 
			Time.deltaTime * 5);
		#endregion
    }

	void OnControllerColliderHit(ControllerColliderHit collision) {
		if ((collision.gameObject.tag == "Chunk" || collision.gameObject.GetComponent<Meteor>()) && !isDashing) {
			isDashing = false;
			gameUI.PlayerDead();
			volume.transform.localPosition = noPost;
			if (score > PlayerPrefs.GetFloat("HighScore")) {
				PlayerPrefs.SetFloat("HighScore", score);
			}
			gameObject.SetActive(false);
		}
	}

	public void AddEnergy(int i) { gameUI.AccumulateEnergy(i); }

	public float GetVelocity() { return controller.velocity.magnitude; }
}
