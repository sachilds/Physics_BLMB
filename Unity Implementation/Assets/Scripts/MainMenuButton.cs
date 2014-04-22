using UnityEngine;
using System.Collections;

public class MainMenuButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		Game_Manager.gameState = Game_Manager.GameState.Menu;
		Application.LoadLevel ("MainMenu");
	}
}
