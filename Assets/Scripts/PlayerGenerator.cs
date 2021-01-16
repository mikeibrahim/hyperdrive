using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour {
	public Player[] players;
	private int pickedPlayer;

    void Awake() {
		pickedPlayer = PlayerPrefs.GetInt("PlayerCharacter");
        Player playerInst = Instantiate(players[pickedPlayer]);
		playerInst.MakePlayer(Players.players[pickedPlayer]);
    }
}
