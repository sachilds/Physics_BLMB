using UnityEngine;
using System.Collections;

public class Boat_Script : MonoBehaviour {

    public float maxSlope;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter2D(Collision2D c) {
        if (c.gameObject.tag == "Water") { 
            foreach(ContactPoint2D contact in c.contacts) {
                if (Vector3.Angle(contact.normal, Vector3.up) < maxSlope) {
                    rigidbody2D.isKinematic = false;
                }
		    }
        }
        else if(c.gameObject.tag == "Ground") {
            rigidbody2D.isKinematic = true;
        }
    }
}
