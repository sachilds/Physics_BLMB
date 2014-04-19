using UnityEngine;
using System.Collections;

public class AdjustVolume : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("Start Bg Vol "+ audio.volume);
		audio.volume *= Game_Manager.backGroundVol;
		Debug.Log("Curr Bg Vol "+ audio.volume);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
