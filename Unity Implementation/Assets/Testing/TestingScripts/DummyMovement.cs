using UnityEngine;
using System.Collections;

public class DummyMovement : MonoBehaviour {
	CustomRigidBody mRigidbody;
	int mSpeed = 10;

	void Start(){
		mRigidbody = gameObject.GetComponent<CustomRigidBody>();
	}
	// Update is called once per frame
	void Update () {
		mRigidbody.AddForce(new Vector2(Input.GetAxis("Horizontal") * mSpeed, 0)); 
	}
	//i love boobies

}
