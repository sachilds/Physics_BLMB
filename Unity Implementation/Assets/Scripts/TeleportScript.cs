﻿using UnityEngine;
using System.Collections;

public class TeleportScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnTriggerEnter (Collider c){
        Debug.Log("Made it");

    }
}
