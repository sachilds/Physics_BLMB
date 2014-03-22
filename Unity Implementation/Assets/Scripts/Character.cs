using UnityEngine;
using System.Collections;

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

	//Editable values via Inspector
	public int scale;						//Transform scale of the player
	public int walkAcceleration;			//Regular speed of the character (pixels)
	public float walkAirRatio;				//How much air control do we have?
	public int runSpeed;					//Running speed of the character (pixels)
	public float maxSlope;					//Maximum slant the player can climb
	public Sprite spriteSheet;				//Spritesheet of the character

	public virtual void Awake() 
	{
        // TODO remove this later
        PhysicsEngine.TestPhysics();

		//Initialize some variables, load resources, etc.
		if (!spriteSheet)
			spriteSheet = Resources.Load<Sprite>("Sprites/CandySprites");
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = spriteSheet;

		size = new Vector2(spriteSheet.rect.width / 100, spriteSheet.rect.height / 100);
		boundingRect = new Rect(transform.position.x, transform.position.y, size.x, size.y);
		transform.localScale = new Vector3(scale, scale, scale);

		boxCollider = GetComponent<BoxCollider2D>();
		boxCollider.size = new Vector2(size.x, size.y);
		boxCollider.center = new Vector2(0, size.y / 2);
	}

	public virtual void Update() 
	{
		//==UPDATE VALUES===================================================//
		size = new Vector2(spriteSheet.rect.width, spriteSheet.rect.height);
		boundingRect = new Rect(transform.position.x, transform.position.y, size.x, size.y);
		//==================================================================//
	}

	public void MoveHorizontal(float axisValue)
	{
		//Apply force to begin moving character in direction of input if grounded
		if (grounded)
			rigidbody2D.AddForce(new Vector3(axisValue * walkAcceleration * Time.deltaTime, 0));
		//Else, give the player a small ratio of control while they are in the air
		else
			rigidbody2D.AddForce(new Vector3(axisValue * walkAcceleration * walkAirRatio * Time.deltaTime, 0));
	}

	public bool IsGrounded()
	{
		return grounded;
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		foreach(ContactPoint2D contact in collision.contacts)
		{
			if (Vector3.Angle(contact.normal, Vector3.up) < maxSlope)
			{
				grounded = true;
			}
		}
	}
	
	void OnCollisionExit2D()
	{
		grounded = false;
	}
}
