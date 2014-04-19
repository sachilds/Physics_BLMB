using UnityEngine;
using System.Collections;

public class Main_Menu_GUI : MonoBehaviour {
		
	public Texture2D onOver;
	public Texture2D onExit;
	public string level = " ";
	public static string menuStatus = " ";

	void Start () {
		
	}
	void Update () {
		
	}
	void OnMouseEnter()
	{
		if(onOver)
			guiTexture.texture = onOver;
		else
			Debug.Log("onOver isn't set.");
	}
	
	void OnMouseDown()
	{
		if (level.Equals("Play"))
		{
			//Debug.Log("Play");
			menuStatus="Play";
		}
		if (level.Equals("Options"))
		{
			//Debug.Log("Options");
			menuStatus="Options";
		}

		if (level.Equals("Quit"))
		{
			//Debug.Log("Quit");
			menuStatus="Quit";
		}
	}
		
	void OnMouseExit()
	{
	if(onExit)
		guiTexture.texture = onExit;
	else
		Debug.Log("onOver isn't set.");
	}
}
