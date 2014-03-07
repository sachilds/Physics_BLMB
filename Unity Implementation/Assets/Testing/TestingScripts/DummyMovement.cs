using UnityEngine;
using System.Collections;

public class DummyMovement : MonoBehaviour {
	Rigidbody2D mRigidbody;
	int mSpeed = 10;

	void Start(){
		mRigidbody = gameObject.GetComponent<Rigidbody2D>();
	}
	// Update is called once per frame
	void Update () {
		mRigidbody.AddForce(new Vector2(Input.GetAxis("Horizontal") * mSpeed, 0)); 
	}
	//i love boobies

}
