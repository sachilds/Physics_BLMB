using UnityEngine;
using System.Collections;

public enum GroundType { 
    REGULAR, SLIPPERY, STICKY
}

[RequireComponent(typeof(SpriteRenderer))] 	//For visually rendering the character
[RequireComponent(typeof(Animation))]		//Stores all animations of the character
[RequireComponent(typeof(Rigidbody2D))]		//Temporary for now
[RequireComponent(typeof(BoxCollider2D))]	//Temporary for now
public class Character : MonoBehaviour 
{
	[HideInInspector]
	public Vector2 size;					//Width and height of the character (pixels)
	[HideInInspector]
	public Rect boundingRect;				//Bounds of the character composed of the x, y, w, h
	[HideInInspector]
	public SpriteRenderer spriteRenderer;	//Renderer for the character
	[HideInInspector]
	public BoxCollider2D boxCollider;

	private bool grounded;					//Checks if the character is grounded
    public const int MAX_VELOCITY = 10;

	//Editable values via Inspector
	public int scale;						//Transform scale of the player
	public int walkAcceleration;			//Regular speed of the character (pixels)
	public float walkAirRatio;				//How much air control do we have?
	public int runSpeed;					//Running speed of the character (pixels)
	public float maxSlope;					//Maximum slant the player can climb
	public Sprite spriteSheet;				//Spritesheet of the character
    public float mass;                      // Mass of the player
    public float jumpForce;                 // The jumping force of the player
    public GroundType groundType;

    // Testing variables
    public float maxHeight;

	public virtual void Awake() 
	{
		//Initialize some variables, load resources, etc.
		if (!spriteSheet)
			spriteSheet = Resources.Load<Sprite>("Sprites/CandySprites");
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = spriteSheet;

		size = new Vector2(spriteSheet.rect.width / 100, spriteSheet.rect.height / 100);
		boundingRect = new Rect(transform.position.x, transform.position.y, size.x, size.y);
		transform.localScale = new Vector3(scale, scale, scale);

		boxCollider = GetComponent<BoxCollider2D>();
	}

	public virtual void Update() 
	{
		//==UPDATE VALUES===================================================//
		size = new Vector2(spriteSheet.rect.width, spriteSheet.rect.height);
		boundingRect = new Rect(transform.position.x, transform.position.y, size.x, size.y);
		//==================================================================//
	}

    // Grounded Property
    public bool IsGrounded {
        get { return grounded; }
        set { grounded = value; }
    }

    // Horizontal Movement TODO may switch this to movement in general, and check within there if player jumped or not?
    // Or maybe have a "Has Jumped" and use that to do vertical physics
	public void MoveHorizontal(float axisValue) {
        if (rigidbody2D.velocity.magnitude > MAX_VELOCITY) {
            rigidbody2D.velocity = rigidbody2D.velocity.normalized * MAX_VELOCITY;
        }
        else {
			//rigidbody2D.
            float coeff = PhysicsEngine.GetCoeff(IsGrounded, groundType);

            // Apply the forces to the object
            if (axisValue < -0.1f) { // going left
                axisValue *= -500;
				//axisValue *= 500;
                rigidbody2D.AddForce(new Vector2(PhysicsEngine.HorizontalNetForce(axisValue, coeff, mass) * -1 * Time.deltaTime, 0));
            } else if(axisValue > 0.1f) { // going right
				//axisValue *= 500;
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
	
    void OnCollisionStay2D(Collision2D collision) {
		foreach(ContactPoint2D contact in collision.contacts) {
			if (Vector3.Angle(contact.normal, Vector3.up) < maxSlope) {
				IsGrounded = true;
            }
		}
	}

	void OnCollisionExit2D() {
        // For some reason it doesn't detect this? - Sarah
		IsGrounded = false;
	}
}
