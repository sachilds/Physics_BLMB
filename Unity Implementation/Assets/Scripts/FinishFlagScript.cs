using UnityEngine;
using System.Collections;

public class FinishFlagScript : MonoBehaviour {
	public static string mName;
	// Use this for initialization
	void Start () {
		mName = " ";
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		Game_Manager.gameState = Game_Manager.GameState.GameOver;
		mName = c.gameObject.transform.name;
		Application.LoadLevel("FinalScreen");
	}
}
