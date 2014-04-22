using UnityEngine;
using System.Collections;

public class FinalSceneScript : MonoBehaviour {
	public GameObject P1Win;
	public GameObject P2Win;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI () {
		if (FinishFlagScript.name == "Player1") {
			Debug.Log ("poop");
			P1Win.SetActive (true);
		}
		if (FinishFlagScript.name == "Player2") {
			Debug.Log ("poop");
			P1Win.SetActive (true);	
		}
	}
}
