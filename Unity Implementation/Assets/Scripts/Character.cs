using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))] 	//For visually rendering the character
[RequireComponent(typeof(Animation))]		//Stores all animations of the character
[RequireComponent(typeof(Rigidbody2D))]		//Temporary for now
[RequireComponent(typeof(BoxCollider2D))]		//Temporary for now
public class Character : MonoBehaviour 
{
	public Vector2 size;					//Width and height of the character (pixels)
	public Vector2 position;				//X and Y position of the character
	public Rect boundingRect;				//Bounds of the character composed of the x, y, w, h
	public float rotation;					//Rotation (theta) of the player
	public int scale;						//Transform scale of the player
	public int walkSpeed;					//Regular speed of the character (pixels)
	public int runSpeed;					//Running speed of the character (pixels)
	public Sprite spriteSheet;				//Spritesheet of the character
	public SpriteRenderer spriteRenderer;	//Renderer for the character
	public BoxCollider2D boxCollider;

	void Awake() 
	{
		//Initialize some variables, load resources, etc.
		walkSpeed = 3;
		runSpeed = 6;

		spriteSheet = Resources.Load<Sprite>("Sprites/CandySprites");
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = spriteSheet;

		size = new Vector2(spriteSheet.rect.width / 100, spriteSheet.rect.height / 100);
		position = new Vector2(transform.position.x, transform.position.y);
		boundingRect = new Rect(position.x, position.y, size.x, size.y);
		rotation = transform.rotation.z;
		scale = 3;
		transform.localScale = new Vector3(scale, scale, scale);

		boxCollider = GetComponent<BoxCollider2D>();
		boxCollider.size = new Vector2(size.x, size.y);
		boxCollider.center = new Vector2(0, size.y / 2);
	}

	void Update() 
	{
		//==UPDATE VALUES===================================================//
		size = new Vector2(spriteSheet.rect.width, spriteSheet.rect.height);
		position = new Vector2(transform.position.x, transform.position.y);
		boundingRect = new Rect(position.x, position.y, size.x, size.y);
		rotation = transform.rotation.z;
		//==================================================================//

		//==UPDATE PHYSICS==================================================//

		//Temporary movement stuff for now

		if (Input.GetButton("Horizontal"))												//If we are pressing the left or right movement buttons
		{
			float h = Input.GetAxisRaw("Horizontal");									//Which direction (negative/positive) are we travelling
			transform.Translate(((float)walkSpeed * h) * Time.deltaTime, 0, 0);			//Translate the player by walkSpeed * direction
		}

		//==================================================================//

        
	}
}
