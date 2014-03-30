using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))] 	//For visually rendering the hat
[RequireComponent(typeof(Rigidbody2D))]		//Temporary for now
[RequireComponent(typeof(BoxCollider2D))]	//Temporary for now
public class Hat : MonoBehaviour 
{
	//Enumeration of every hat type possible
	public enum HatType
	{
		None,
		OneHat,
		AnotherHat,
        Jello_Spawn,
        Candy_Cannon
	}
	public static HatType hatType;

	[HideInInspector]
	public Vector2 size;					//Width and height of the hat (pixels)
	[HideInInspector]
	public Vector2 position;				//X and Y position of the hat
	[HideInInspector]
	public Rect boundingRect;				//Bounds of the hat composed of the x, y, w, h
	[HideInInspector]
	public float rotation;					//Rotation (theta) of the hat
	[HideInInspector]
	public SpriteRenderer spriteRenderer;	//Renderer for the hat
	[HideInInspector]
	public BoxCollider2D boxCollider;

	//Editable values via Inspector
	public int scale;						//Transform scale of the hat
	public Sprite sprite;					//Image for the hat

    private GameObject jelloBlock;
	private Character wearer;				//Reference to who is wearing the hat

	void Awake() 
	{
		//wearer = GameObject.FindWithTag("Player");

		//Initialize some variables, load resources, etc.
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprite;
		
		size = new Vector2(sprite.rect.width / 100, sprite.rect.height / 100);
		position = new Vector2(transform.position.x, transform.position.y);
		boundingRect = new Rect(position.x, position.y, size.x, size.y);
		rotation = transform.rotation.z;
		transform.localScale = new Vector3(scale, scale, scale);
		
		boxCollider = GetComponent<BoxCollider2D>();
		boxCollider.size = new Vector2(size.x, size.y);
		boxCollider.center = new Vector2(0, size.y / 2);

        jelloBlock = Resources.Load("Prefabs/JELLO_BLOCK") as GameObject;
	}
	
	void Update() 
	{
		//==UPDATE VALUES===================================================//
		size = new Vector2(sprite.rect.width, sprite.rect.height);
		position = new Vector2(transform.position.x, transform.position.y);
		boundingRect = new Rect(position.x, position.y, size.x, size.y);
		rotation = transform.rotation.z;
		//==================================================================//

		//Update and apply effects based on the current hat equipted
		switch(hatType)
		{
		case HatType.None:
			//scale = wearer.scale;
			break;
		case HatType.OneHat:
			//scale = wearer.scale;
			break;
		case HatType.AnotherHat:
			//scale = wearer.scale;
			break;
		}
	}

    public void UseMechanic() {

        switch (hatType) {
            case HatType.None:
                break;
            case HatType.OneHat:
                break;
            case HatType.AnotherHat:
                break;
            case HatType.Jello_Spawn:
                // Instantiate Jello
                
                break;
            default:
                break;
        }
    }
}
