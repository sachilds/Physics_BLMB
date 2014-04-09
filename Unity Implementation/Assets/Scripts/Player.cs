using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerInputScript))]
public class Player : Character 
{
    [HideInInspector]
    public static Object spawningEffect;
	[HideInInspector]
	public PlayerInputScript playerInput;	//Reference to the players input script
	public string nickname;					//Nickname to show up above head/scores/lives/etc.

    public Hat currentHat;
    public Hat.HatType hatType;

	void Awake() {
		base.Awake();

		playerInput = GetComponent<PlayerInputScript>();	//Get reference to PlayerInputScript component
        spawningEffect = Resources.Load("Prefabs/portalEffect") as GameObject;
        if (currentHat)
            hatType = currentHat.hatType;
	}

	void Update() {
		base.Update();
        GetMaxHeight();
        rigidbody2D.AddForce(new Vector2(0, -9.8f));
	}

    private void GetMaxHeight() { 
        if (!IsGrounded) {
            if ((rigidbody2D.velocity.y < 0.15f && rigidbody2D.velocity.y > 0) 
                || (rigidbody2D.velocity.y > -0.15f && rigidbody2D.velocity.y < 0)) {
                maxHeight = transform.position.y;
            }
        }
    }

    private void Bounce() {
        IsGrounded = false;
        float Force = PhysicsEngine.CalculateVerticalBounce(maxHeight, mass);
        rigidbody2D.AddForce(new Vector2(0, Force * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D c) {
        // Check to see if it's on a sticky surface
        if (c.tag == "SlipperySurface")
            groundType = GroundType.SLIPPERY;
        else if (c.tag == "StickySurface")
            groundType = GroundType.STICKY;

        Debug.Log("Entered a trigger, tag was: " + c.tag);
    }

    void OnTriggerExit2D(Collider2D c) {
        groundType = GroundType.REGULAR;
        Debug.Log("Left a trigger, tag was: " + c.tag);
    }

    void OnCollisionEnter2D(Collision2D c) {
        if (c.gameObject.tag == "JelloBlock") { 
            foreach(ContactPoint2D contact in c.contacts) {
                if (Vector3.Angle(contact.normal, Vector3.up) < maxSlope)
                {
                    Bounce();
                    Debug.Log("I hit a block");
                }
		    }
        }

    }

    // TEMP till hats are added 
    public void UseHatMechanic() 
    {
        if (currentHat)
            currentHat.UseMechanic();
    }

    public void CancelHatMechanic() 
    {
        if (currentHat)
            currentHat.CancelHatMechanic(currentHat);
    }
}
