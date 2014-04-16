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
        CandyCannon,
        HookerHat,
        Lollicopter,
       
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

    public Transform mechanicSpawn;

    private GameObject stoneInGame;
    public GameObject stonePrefab;
    public GameObject jelloBlock;
    private GameObject jelloInGame;
    public GameObject cannonPrefab;
    private GameObject cannonInGame;
    public GameObject hookPrefab;
    private GameObject hookInGame;
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

            case HatType.HookerHat:

            case HatType.Lollicopter:

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
                float MAX_CHARGE_X = 200;
                float MAX_CHARGE_Y = 350;
                float chargeRate = 15;
                
                Vector2 initialVelocity = new Vector2(25,25);
                while (Input.GetButton("P" + wearer.name[6] + ".HatMechanic"))
                {
                    
                    //plot
                    //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
                   // Debug.DrawLine(transform.root.position, new Vector2(transform.root.position.x + 10, transform.root.position.y + 10), Color.green);
                  
                    initialVelocity.y += chargeRate;
                    if(initialVelocity.x < MAX_CHARGE_X) initialVelocity.x += chargeRate;
                    
                    if (initialVelocity.y > MAX_CHARGE_Y)
                    {
                       
                        break;
                    }
                    yield return new WaitForSeconds(Time.deltaTime);
                }
                GameObject go = Instantiate(stonePrefab, new Vector3(mechanicSpawn.transform.position.x, mechanicSpawn.transform.position.y, mechanicSpawn.transform.position.y), 
                                           new Quaternion(0,0, 0, 1)) as GameObject;
                int dir = 0;
                if (wearer.transform.rotation.y == 0)
                {
                    dir = 1;
                }
                else dir = -1;
                
                go.GetComponent<Rigidbody2D>().AddForce(new Vector2(initialVelocity.x * dir, initialVelocity.y));
                go.GetComponent<Rigidbody2D>().AddTorque(25);
                GameObject.Destroy(go, 2);

                Debug.Log("Broke out");

                yield return new WaitForSeconds(Time.deltaTime);

                canSpawn = true;
                break;

            case HatType.HookerHat:
                GameObject hookInGame = Instantiate(hookPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),
                                          new Quaternion(0, 0, 0, 1)) as GameObject;
                
                //.Debug.D//ebug.Log("Made it");
                dir = 0;
                if (wearer.transform.rotation.y == 0)
                {
                    dir = 1;
                }
                else dir = -1;

                hookInGame.GetComponent<Rigidbody2D>().AddForce(new Vector2(300 * dir, 800));
                hookInGame.transform.Rotate(new Vector3(0,0,1), dir * -45);
                hookInGame.transform.localScale = new Vector3(1, 1, 1);
                string temp = wearer.name[6] + " ";
                hookInGame.GetComponent<HookerScript>().setNumOfOwner(int.Parse(temp));
                
                while (Input.GetButton("P" + wearer.name[6] + ".HatMechanic"))
                {
                    yield return new WaitForSeconds(Time.deltaTime);
                }
                GameObject.DestroyObject(hookInGame);
                canSpawn = true;
                break;

            
            case HatType.Lollicopter:
                wearer.SendMessage("Lollicopter");
                yield return new WaitForSeconds(0.7f);
                canSpawn = true;
                break;
           

            default:
                break;
        }
    }

    //public Vector3 PlotTrajectoryAtTime(Vector2 start, Vector2 startVel, float time)
    //{
    //    // s = vi*t + (at^2)/2
    //    return start + startVel * time + (Physics2D.gravity * Mathf.Pow(time, 2) / 2);
    //}

    //public void PlotTrajectory(Vector2 start, Vector2 startVel, float timeStep, float maxTime)
    //{

    //}
    public void CancelHatMechanic(Hat currentHat)
    {
        switch (hatType)
        {
            case HatType.None:
                break;
            case HatType.Indian:
            case HatType.Jello:
                Destroy(currentHat.gameObject);
                if (jelloInGame)
                    Destroy(jelloInGame);
                break;
            case HatType.CandyCannon:
                Destroy(currentHat.gameObject);
                break;
            case HatType.Lollicopter:
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
