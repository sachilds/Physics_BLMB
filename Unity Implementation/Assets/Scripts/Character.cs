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

	//Editable values via Inspector
	public int scale;						//Transform scale of the player
	public int walkSpeed;					//Regular speed of the character (pixels)
	public int runSpeed;					//Running speed of the character (pixels)
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

		//==UPDATE PHYSICS==================================================//

		//Temporary movement stuff for now

//		if (Input.GetButton("Horizontal"))												//If we are pressing the left or right movement buttons
//		{
//			float h = Input.GetAxisRaw("Horizontal");									//Which direction (negative/positive) are we travelling
//			transform.Translate(((float)walkSpeed * h) * Time.deltaTime, 0, 0);			//Translate the player by walkSpeed * direction
//		}

		//==================================================================//

        
	}

	public void MoveHorizontal(float value){
        //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(((float)walkSpeed * value) * Time.deltaTime, 0));
        transform.Translate(((float)walkSpeed * value) * Time.deltaTime, 0, 0);			//Translate the player by walkSpeed * direction
	}
}
