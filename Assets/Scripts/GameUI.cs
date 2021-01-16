using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {
	[SerializeField] private SlideBar energyBar = null;
	[SerializeField] private Color32 energyNull = Color.clear;
	[SerializeField] private Color32 energyFill = Color.clear;
	[SerializeField] private TMP_Text score = null;
	[SerializeField] private GameObject instructions = null;
	bool playerDead = false;

	void Start() { instructions.SetActive(false); }

    void FixedUpdate() {
        energyBar.FillColor(Color.Lerp(energyNull, energyFill, energyBar.GetValue() / energyBar.GetMaxValue()));
    }

	void Update() {
		if (playerDead) {
			if (Input.GetKeyDown(KeyCode.R)) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			} else if (Input.GetKeyDown(KeyCode.H)) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
			}
		}
	}

	public void SetMaxEnergy(float i) { energyBar.SetMaxValue(i); }

	public void AccumulateEnergy(float i) {
		energyBar.AddValue(i);
		if (energyBar.GetValue() >= energyBar.GetMaxValue()) { energyBar.SetValue(energyBar.GetMaxValue()); }
		if (energyBar.GetValue() <= 0) { energyBar.SetValue(0); }
	}

	public void SetScore(float f) { score.text = f.ToString(); }

	public float GetEnergy() { return energyBar.GetValue(); }

	public void PlayerDead() {
		energyBar.gameObject.SetActive(false);
		instructions.SetActive(true);
		playerDead = true;
	}
}
