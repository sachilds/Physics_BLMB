using UnityEngine;
using System.Collections;

public class PlayerSelect : MonoBehaviour {
	public string level = " ";
	public static string menuStatus = " ";
	

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
