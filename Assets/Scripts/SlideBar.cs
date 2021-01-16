using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideBar : MonoBehaviour {
    public Slider slider;
	public GameObject fillColor;

	public void SetMaxValue(float value) {
		slider.maxValue = value;
		slider.value = value;
	}

	public void AddValue(float value) {
		slider.value += value;
	}

    public void SetValue(float value) {
    	slider.value = value;
    }

	public float GetValue() {
		return slider.value;
	}

	public float GetMaxValue() {
		return slider.maxValue;
	}

	public void FillColor(Color32 col) {
		fillColor.GetComponent<Image>().color = col;
	}
}
