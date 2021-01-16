using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	[SerializeField] private Image imageFrame = null;
	[SerializeField] private Sprite[] shipImages = null;
	[SerializeField] private Button[] characterArrows = null;
	[SerializeField] private TMP_Text shipStats = null;
	[SerializeField] private TMP_Text gunStats = null;
	[SerializeField] private Button startGame = null;
	[SerializeField] private Button info = null;
	[SerializeField] private GameObject infoPanel = null;
	[SerializeField] private TMP_Text highScore = null;
	int currentImage;
	bool LEFT = false;
	bool RIGHT = true;
	bool infoToggle;

    void Start() {
		highScore.text = PlayerPrefs.GetFloat("HighScore").ToString();
		currentImage = 0;
        imageFrame.sprite = shipImages[currentImage];
		characterArrows[1].onClick.AddListener(delegate{CycleImages(RIGHT);});
		characterArrows[0].onClick.AddListener(delegate{CycleImages(LEFT);});
		startGame.onClick.AddListener(delegate{StartGame();});
		info.onClick.AddListener(delegate{infoPanel.gameObject.SetActive(!infoToggle); infoToggle = !infoToggle;});
		UpdateStats();
    }

	void Update() {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			CycleImages(LEFT);
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			CycleImages(RIGHT);
		}
	}

	private void CycleImages(bool b) {
		currentImage += b ? 1 : -1;
		if (currentImage >= shipImages.Length) { currentImage = 0; }
		if (currentImage < 0) { currentImage = shipImages.Length - 1; }
		imageFrame.sprite = shipImages[currentImage];
		UpdateStats();
	}

	private void UpdateStats() {
		shipStats.text = "Ship: " 			+ 	Players.players[currentImage].playerName 	+ Environment.NewLine + 
					 	"Speed: " 			+ 	Players.players[currentImage].speed 		+ Environment.NewLine + 
					 	"Dash Time: " 		+ 	Players.players[currentImage].dashTime 		+ Environment.NewLine + 
					 	"Dash Speed: " 		+ 	Players.players[currentImage].dashSpeed 	+ Environment.NewLine;
		
		gunStats.text = "Gun: " 				+ 	Guns.guns[currentImage].gunName 		+ Environment.NewLine + 
					 	"Time Between Shots: " 	+ 	Guns.guns[currentImage].timeBtw 		+ Environment.NewLine + 
					 	"Full Auto: " 			+ 	Guns.guns[currentImage].auto 			+ Environment.NewLine + 
					 	"Bullet Speed: " 		+ 	Bullets.bullets[currentImage].speed 	+ Environment.NewLine;
	}

	private void StartGame() {
		PlayerPrefs.SetInt("PlayerCharacter", currentImage);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
