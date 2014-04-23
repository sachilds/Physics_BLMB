using UnityEngine;
using System.Collections;

public class AdjustVolume : MonoBehaviour {

	// Use this for initialization
	void Start () {
		audio.volume *= Game_Manager.backGroundVol;
	}
}
