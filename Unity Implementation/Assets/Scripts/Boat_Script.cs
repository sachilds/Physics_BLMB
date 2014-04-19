using UnityEngine;
using System.Collections;

public class Boat_Script : MonoBehaviour {

    public float maxSlope;
    public GameObject rider;
    private Player riderScript;
    public float upwardForce = 12.5f;

    private bool isOnWater = false;

    void FixedUpdate() {
        if(isOnWater) 
            rigidbody2D.AddForce(new Vector2(transform.up.x * upwardForce, transform.up.y * upwardForce));

    }

    void OnCollisionEnter2D(Collision2D c) {  
        if (c.gameObject.tag == "Ground") {
            isOnWater = false;
            rigidbody2D.drag = 17f;
        }

        if (c.gameObject.tag == "Player") { 
            if (!rider) {
                foreach(ContactPoint2D contact in c.contacts) {
                    if (Vector3.Angle(contact.normal, Vector3.down) < maxSlope) {
                        SetBoat(c.gameObject);
                    }
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D c) {
        if (c.gameObject.tag == "Player") {
            if (rider && rider == c.gameObject) {
                ResetBoat();
            }
        }
    }

    void OnTriggerStay2D(Collider2D c) {
        if (c.tag == "Water") {
            isOnWater = true;
            rigidbody2D.drag = 5f;
        }
    }

    void OnTriggerExit2D(Collider2D c) {
        if (c.tag == "Water") {
            isOnWater = false;
        }
    }

    private void Movement(float axisValue) {
        if (rigidbody2D.velocity.x >= PhysicsEngine.MAX_VELOCITY || rigidbody2D.velocity.x <= -PhysicsEngine.MAX_VELOCITY) {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.normalized.x * PhysicsEngine.MAX_VELOCITY, rigidbody2D.velocity.y);
        }

        if (rider.rigidbody2D.velocity.x >= PhysicsEngine.MAX_VELOCITY || rider.rigidbody2D.velocity.x <= -PhysicsEngine.MAX_VELOCITY) {
            rider.rigidbody2D.velocity = new Vector2(rider.rigidbody2D.velocity.normalized.x * PhysicsEngine.MAX_VELOCITY, rigidbody2D.velocity.y);
        }


        rider.rigidbody2D.AddForce(new Vector2(axisValue * 10, 0));
        rigidbody2D.AddForce(new Vector2(axisValue * 10, 0));
    }

    private void ResetBoat() {
        rider.rigidbody2D.drag = 0.05f;
        rider = null;
        riderScript.onBoat = false;
        riderScript.ridingBoat = null;
        upwardForce = 10.75f;
    }

    private void SetBoat(GameObject c) {
        rider = c;
        rider.rigidbody2D.drag = 5f;
        riderScript = rider.GetComponent<Player>();
        riderScript.onBoat = true;
        riderScript.ridingBoat = gameObject;

        upwardForce = 25.75f;
    }
}
