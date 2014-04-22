using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GUIText))]			//Required to draw labels above player's heads
public class PlayerLabel : MonoBehaviour
{
	public Player playerToFollow;				//Transform the label should hover over
	public Vector3 labelOffset = Vector3.up;	//How far to draw label above player
	public Font fontFace;						//Which type face to use
	private int fontSize;						//Size of the GUIText font
	private Color[] fontColor = new Color[2];	//Array of colors for the players text color
	private GUIText guiText;					//Reference of GUIText

	void Start()
	{
        if(!playerToFollow)
            playerToFollow = transform.parent.GetComponent<Player>();					//Label must be a child of the player
		guiText = GetComponent<GUIText>();											//Get reference to GUIText component

		guiText.text = playerToFollow.nickname;

		guiText.alignment = TextAlignment.Center;

		guiText.font = fontFace;

		fontSize = 25;																//Set the font size to 15 initially
		guiText.fontSize = fontSize;												//Set the font size of the player label

		fontColor[0] = new Color(255, 0, 0);										//Player 1 is red
		fontColor[1] = new Color(0, 0, 255);										//Player 2 is blue

		//Get the right color based of what player you are
		if (playerToFollow.playerInput.playerNumber == PlayerNumber.one)
			guiText.color = fontColor[0];
		else if (playerToFollow.playerInput.playerNumber == PlayerNumber.two)
			guiText.color = fontColor[1];
	}

	void Update() 
	{
		//Set the position of the text based on offset and player position
        if(playerToFollow)
            transform.position = Camera.main.WorldToViewportPoint(playerToFollow.transform.position + labelOffset);
	}
}
