using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GUIText))]		//Required to draw labels above player's heads
public class PlayerLabel : MonoBehaviour 
{
	public Transform targetToFollow;			//Transform the label should hover over
	public Vector3 labelOffset = Vector3.up;	//How far to draw label above player

	void Update () 
	{
		transform.position = Camera.main.WorldToViewportPoint(targetToFollow.position + labelOffset);
	}
}
