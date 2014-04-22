using UnityEngine;
using System.Collections;

public class BouyantObject : MonoBehaviour {

    private bool isOnWater;
    public float upwardForce = 11;
    private float maxSlope = 60;

    void FixedUpdate() {
        if (isOnWater)
            rigidbody2D.AddForce(new Vector2(transform.up.x * upwardForce, transform.up.y * upwardForce));
    }

    void OnCollisionEnter2D(Collision2D c) {  
        if (c.gameObject.tag == "Player") { 
            upwardForce = 25;
        }
    }

    void OnCollisionExit2D(Collision2D c) {
        if (c.gameObject.tag == "Player") {
            upwardForce = 11;
        }
    }

    void OnTriggerStay2D(Collider2D c) {
        if (c.tag == "Water") {
            isOnWater = true;
        }
    }

    void OnTriggerExit2D(Collider2D c) {
        if (c.tag == "Water") {
            isOnWater = false;
        }
    }
}
