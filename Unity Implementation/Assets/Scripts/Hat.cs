using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))] 	//For visually rendering the hat
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Hat : MonoBehaviour
{
    //Enumeration of every hat type possible
    public enum HatType
    {
        None,
        Indian,
        Jello,
        CandyCannon
    }
    public HatType hatType;

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
    [HideInInspector]
    public CircleCollider2D trigger;

    //Editable values via Inspector
    public float scale;						//Transform scale of the hat
    public Sprite sprite;					//Image for the hat

    public GameObject jelloBlock;
    private GameObject jelloInGame;
    public GameObject cannonPrefab;
    private GameObject cannonInGame;
    private bool canSpawn;

    private Player wearer;				//Reference to who is wearing the hat

    void Awake()
    {
        canSpawn = true;

        //Initialize some variables, load resources, etc.
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingLayerName = "Equipted";

        size = new Vector2(sprite.rect.width / 100, sprite.rect.height / 100);
        position = new Vector2(transform.position.x, transform.position.y);
        boundingRect = new Rect(position.x, position.y, size.x, size.y);
        rotation = transform.rotation.z;
        transform.localScale = new Vector3(scale, scale, scale);

        boxCollider = GetComponent<BoxCollider2D>();
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
        switch (hatType)
        {
            case HatType.None:
                //scale = wearer.scale;
                break;
            case HatType.Indian:
                break;
            case HatType.Jello:
                //scale = wearer.scale;
                break;
        }

        if (wearer)
            transform.position = wearer.hatHolder.position;
    }
    public void StartMechanic()
    {
        if (canSpawn)
        {
            canSpawn = false;
            StartCoroutine("UseMechanic");
        }
      
    }
    public IEnumerator UseMechanic()
    {
        
        Debug.Log("YOLO");
        switch (hatType)
        {
            case HatType.None:
                break;
            case HatType.Jello:
                if (!jelloInGame)
                {
                    jelloInGame = Instantiate(jelloBlock, new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
                }
                else
                {
                    Destroy(jelloInGame);
                    jelloInGame = Instantiate(jelloBlock, new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
                }
                yield return new WaitForSeconds(0.5f);
                canSpawn = true;
                break;
            case HatType.Indian:
                Debug.Log("Hi");
                yield return new WaitForSeconds(2);
                canSpawn = true;
                break;
            default:
                break;
        }
    }

    public void CancelHatMechanic(Hat currentHat)
    {
        switch (hatType)
        {
            case Hat.HatType.None:
                break;
            case HatType.Indian:
            case Hat.HatType.Jello:
                Destroy(currentHat.gameObject);
                break;
            case Hat.HatType.CandyCannon:
                Destroy(currentHat.gameObject);
                break;
            default:
                break;
        }
    }

    public static void AttachToPlayer(Player player, Hat newHat)
    {
        newHat.wearer = player;
        newHat.transform.rotation = new Quaternion(0, 0, 0, 1);
        newHat.rigidbody2D.isKinematic = true;
        newHat.collider2D.enabled = false;
        newHat.transform.position = player.hatHolder.position;
        newHat.transform.parent = player.hatHolder;
    }

    public static void DetachFromPlayer()
    {

    }
}
