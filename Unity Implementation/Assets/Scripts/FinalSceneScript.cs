using UnityEngine;
using System.Collections;

public class FinalSceneScript : MonoBehaviour {
	public GameObject P1Win;
	public GameObject P2Win;

	void OnGUI () {
		if (FinishFlagScript.mName == "Player1") {
			P1Win.SetActive (true);
		}
		if (FinishFlagScript.mName == "Player2") {
			P1Win.SetActive (true);	
		}
	}
}
