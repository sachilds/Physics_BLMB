﻿using UnityEngine;
using System.Collections;

public class PlayerSelect : MonoBehaviour {
	private bool playerOne;
	private bool playerTwo;
	private bool checker;
	// Use this for initialization
	void Start () {
		playerOne = false;
		playerTwo = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("P1.Jump"))
			playerOne = true;
		if (Input.GetButtonDown ("P2.Jump"))
			playerTwo = true;
		if (Input.GetButtonDown ("P1.HatMechanic"))
			playerOne = true;
		if (Input.GetButtonDown ("P2.HatMechanic"))
			playerTwo = true;

		if(Input.GetButtonDown (
		if (playerOne && playerTwo) {
			//loads two player map
			Application.LoadLevel ("Candyland_2Player");
			Debug.Log ("loads two player map");
		} else {
			//loads single player map
			Debug.Log ("loads single player map");
			Application.LoadLevel ("Candyland_1Player");
		}
	}
}
