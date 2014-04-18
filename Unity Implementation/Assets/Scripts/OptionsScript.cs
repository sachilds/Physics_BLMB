﻿using UnityEngine;
using System.Collections;

public class OptionsScript : MonoBehaviour {
	private float sliderValFX;//sound effect slider
	private float sliderValBG;//background music slider
	private GUIText title;
	private GUIStyle style;
	// Use this for initialization
	void Start () {
		title = GUIText.FindObjectOfType<GUIText>();
		sliderValFX = 1.0f;
		sliderValBG = 1.0f;
		style = new GUIStyle();
		style.fontSize = 18;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(sliderValBG +"BG Vol");
		Debug.Log(sliderValFX +"FX Vol");
	}
	void OnGUI () {
		// Make a background box
		GUI.BeginGroup(new Rect(Screen.width/2 - 150,Screen.height/2 - 150,300,300));
		GUI.Box(new Rect(0,0,300,300),"Options");
		GUI.Box(new Rect(50,50,200,100),"Background Music Volume");
		sliderValBG = GUI.HorizontalSlider(new Rect(75,100,150,30),sliderValBG,0.0f,1.0f);
		GUI.Box(new Rect(50,170,200,100),"Sound Fx Volume");
		sliderValFX = GUI.HorizontalSlider(new Rect(75,220,150,30),sliderValFX,0.0f,1.0f);
		GUI.EndGroup();

		if(GUI.Button(new Rect(10,10,100,100),"Back to Menu"))
		{
			Application.LoadLevel("MainMenu");
		}

	}
	public float getFXVol()
	{
		return sliderValFX;
	}
	public float getBGVol()
	{
		return sliderValBG;
	}
}
