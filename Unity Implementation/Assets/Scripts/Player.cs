using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerInputScript))]
public class Player : Character 
{
	[HideInInspector]
	public PlayerInputScript playerInput;	//Reference to the players input script
	public string nickname;					//Nickname to show up above head/scores/lives/etc.

	void Awake()
	{
		base.Awake();

		playerInput = GetComponent<PlayerInputScript>();	//Get reference to PlayerInputScript component
	}

	void Update()
	{
		base.Update();
	}
}
