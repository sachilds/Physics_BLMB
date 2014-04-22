using UnityEngine;
using System.Collections;

public class FinishFlagScript : MonoBehaviour {
	public static string name;
	// Use this for initialization
	void Start () {
		name = " ";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		Game_Manager.gameState = Game_Manager.GameState.GameOver;
		name = c.gameObject.transform.name;
		Application.LoadLevel("FinalScreen");
	}
}
