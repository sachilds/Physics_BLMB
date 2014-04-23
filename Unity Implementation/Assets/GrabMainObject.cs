using UnityEngine;
using System.Collections;

public class GrabMainObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject go = GameObject.Find("Game_Manager");

        if (go) {
            Debug.Log("Heyooo");
            Destroy(go);
            go = (Instantiate(Resources.Load("Prefabs/Game_Manager")) as GameObject);
            go.name = "Game_Manager";
        }
	}
	
}
