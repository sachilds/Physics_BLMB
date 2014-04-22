using UnityEngine;
using System.Collections;

public class PlayerSelect : MonoBehaviour {
	public string level = " ";
	public static string menuStatus = " ";
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown()
	{
		if (level.Equals("Single"))
		{	
			menuStatus="Single";
		}
		if (level.Equals("Coop"))
		{	
			menuStatus="Coop";
		}
		
	}
}
