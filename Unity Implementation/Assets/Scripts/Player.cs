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

    [HideInInspector]
    public bool hatInRange;
    [HideInInspector]
    public Hat currentHat;
    private Hat closestHat;
    public Transform hatHolder;
    public Hat.HatType hatType;

    //[HideInInspector]
    public bool onBoat;
    //[HideInInspector]
    public GameObject ridingBoat;

    private const int CAP_MAX_HEIGHT = 15;

	void Awake() {
		base.Awake();

		playerInput = GetComponent<PlayerInputScript>();	//Get reference to PlayerInputScript component
        spawningEffect = Resources.Load("Prefabs/PorterEffect") as GameObject;
       
        spawningEffect = Resources.Load("Prefabs/portalEffect") as GameObject;
       
        if (currentHat)
            hatType = currentHat.hatType;
	}

	void Update() {
		base.Update();
        GetMaxHeight();
	}

    void FixedUpdate() {
        rigidbody2D.AddForce(new Vector2(0, PhysicsEngine.GRAVITY));
    }

    // Movement and Caluclation-y stuffs
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
        if (maxHeight > CAP_MAX_HEIGHT)
            maxHeight = CAP_MAX_HEIGHT;
        float Force = PhysicsEngine.CalculateVerticalBounce(maxHeight, mass);
        rigidbody2D.AddForce(new Vector2(0, Force * Time.deltaTime));
    }

    private void Lollicopter() {
        if (!IsGrounded) {
            rigidbody2D.AddForce(new Vector2(0, 400));
        }
    }

    public void Movement(float axisValue) {
        if (onBoat) {
            if (ridingBoat) { // Make sure it exists
                // Send over the value
                ridingBoat.SendMessage("Movement", axisValue);
            }
        }
        else {
            MoveHorizontal(axisValue);
        }
    }

    // Horizontal Movement
	private void MoveHorizontal(float axisValue) {
        if (rigidbody2D.velocity.x >= PhysicsEngine.MAX_VELOCITY || rigidbody2D.velocity.x <= -PhysicsEngine.MAX_VELOCITY) {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.normalized.x * PhysicsEngine.MAX_VELOCITY, rigidbody2D.velocity.y);
        }

        else {
            float coeff = PhysicsEngine.GetCoeff(IsGrounded, groundType);

            // Apply the forces to the object
            if (axisValue < -0.1f) { // going left
                transform.rotation = new Quaternion(0, 180, 0, 1);
                hatHolder.rotation = transform.rotation;
                axisValue *= -400;
                rigidbody2D.AddForce(new Vector2(PhysicsEngine.HorizontalNetForce(axisValue, coeff, mass) * -1 * Time.deltaTime, 0));
            } else if(axisValue > 0.1f) { // going right
                transform.rotation = new Quaternion(0, 0, 0, 1);
                hatHolder.rotation = transform.rotation;
				axisValue *= 400;
                rigidbody2D.AddForce(new Vector2(PhysicsEngine.HorizontalNetForce(axisValue, coeff, mass) * Time.deltaTime, 0));
            }
        }
	}

    // Jump Function
    public void Jump() {
        if (IsGrounded) {
            IsGrounded = false;
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
        }
    }

    // Triggers
    void OnTriggerEnter2D(Collider2D c) {
        // Check to see if it's on a sticky surface
        if (c.tag == "SlipperySurface")
            groundType = GroundType.Slippery;
        else if (c.tag == "StickySurface")
            groundType = GroundType.Sticky;
        else if (c.tag == "Water_Killzone") {
            Debug.Log("Hello Killzone");
            if (onBoat) {
                ridingBoat.SendMessage("ResetBoat");
                onBoat = false;
                ridingBoat = null;
            }
            Level_Manager.Instance.KillPlayer(transform);
        }
        Debug.Log("Entered a trigger, tag was: " + c.tag);
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.tag == "Hat")
        {
            hatInRange = true;
            closestHat = c.gameObject.GetComponent<Hat>();
        }
    }

    void OnTriggerExit2D(Collider2D c) {
        if (c.tag == "Hat")
        {
            hatInRange = false;
        }
        groundType = GroundType.Regular;
        Debug.Log("Left a trigger, tag was: " + c.tag);
    }

    // Collision
    void OnCollisionEnter2D(Collision2D c) {
        if (c.gameObject.tag == "JelloBlock") { 
            foreach(ContactPoint2D contact in c.contacts) {
                if (Vector3.Angle(contact.normal, Vector3.up) < maxSlope) {
                    Bounce();
                }
		    }
        }
    }
   
    // Hats
    public void UseHatMechanic()
    {
        if (currentHat)
        {
           currentHat.StartMechanic();
        }
    }

    public void CancelHatMechanic() 
    {
        if (currentHat)
            currentHat.CancelHatMechanic(currentHat);
    }

    public void AttachToPlayer()
    {
        Debug.Log("Pickup hat.");
        Hat.AttachToPlayer(gameObject.GetComponent<Player>(), closestHat);
        currentHat = closestHat;
        hatType = currentHat.hatType;
    }

    public void DetachFromPlayer()
    {
        Debug.Log("Drop hat.");
        Hat.DetachFromPlayer(gameObject.GetComponent<Player>());
        currentHat = null;
        hatType = Hat.HatType.None;
    }


}
