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
        Boat
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
    public GameObject jelloPrefab;
    private GameObject jelloInGame;
    public GameObject cannonPrefab;
    private GameObject cannonInGame;

    public GameObject boatPrefab;
    private GameObject boatInGame;

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

        if (wearer)
            transform.position = new Vector3(wearer.hatHolder.position.x, wearer.hatHolder.position.y, -0.1f);
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
        switch (hatType)
        {
            case HatType.None:
                break;
            
            case HatType.Jello:
                if (jelloInGame)
                    Destroy(jelloInGame);

                jelloInGame = Instantiate(jelloPrefab, new Vector3(mechanicSpawn.transform.position.x, mechanicSpawn.transform.position.y, 0), mechanicSpawn.rotation) as GameObject;
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
                    
                    if (initialVelocity.y > MAX_CHARGE_Y) {
                        break;
                    }
                    yield return new WaitForSeconds(Time.deltaTime);
                }
                GameObject go = Instantiate(stonePrefab, new Vector3(mechanicSpawn.transform.position.x, mechanicSpawn.transform.position.y, mechanicSpawn.transform.position.y), 
                                           mechanicSpawn.rotation) as GameObject; //new Quaternion(0,0, 0, 1)
                int dir = 0;
                if (wearer.transform.rotation.y == 0)
                {
                    dir = 1;
                }
                else dir = -1;
                
                go.GetComponent<Rigidbody2D>().AddForce(new Vector2(initialVelocity.x * dir, initialVelocity.y));
                go.GetComponent<Rigidbody2D>().AddTorque(25);
                GameObject.Destroy(go, 2);
                yield return new WaitForSeconds(Time.deltaTime);

                canSpawn = true;
                break;

            case HatType.HookerHat:
                GameObject hookInGame = Instantiate(hookPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),
                                          new Quaternion(0, 0, 0, 1)) as GameObject;
                // GameObject player = GameObject.Find("Player" + wearer.name[6]) as GameObject;
           
                dir = 0;
                if (wearer.transform.rotation.y == 0)
                {
                    dir = 1;
                }
                else dir = -1;
               
                hookInGame.GetComponent<Rigidbody2D>().AddForce(new Vector2(450 * dir, 600));
                hookInGame.transform.Rotate(new Vector3(0,0,1), dir * -10);
                hookInGame.transform.localScale = new Vector3(1, 1, 1);
                string temp = wearer.name[6] + " ";
                hookInGame.GetComponent<HookerScript>().setNumOfOwner(int.Parse(temp));
                
                float d = 0;
                float v = 0;
                float t = 0;
                float p = 0;
                float maxTheta = 0;
                //convert to rads
                //  maxTheta *= (Mathf.PI / 180);
                float phaseAngle = 0;
                float newAngle = 0;
                float oldP = p;
                float oldD = d;

                while (Input.GetButton("P" + wearer.name[6] + ".HatMechanic"))
                {
                    if(!hookInGame.GetComponent<HookerScript>().isHooked)//being thrown but not hooked
                    {
                        
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                    else//just got hooked
                    {
                        d = Vector2.Distance(wearer.transform.position, hookInGame.transform.position);
                        v = Mathf.Sqrt(9.81f / d) ;//physics2d.gravity was giving issue
                        t = 0;
                        p = (2 * Mathf.PI) * (float)(Mathf.Sqrt(d / 9.81f)); //physics2d.gravity.y was givving me NAN errors
                        
                        maxTheta = dir * Mathf.Asin((Mathf.Abs(hookInGame.transform.position.x - wearer.transform.position.x))
                                                    / Mathf.Abs(d)); //SOH CAH TOA 
                        //convert to rads
                        //  maxTheta *= (Mathf.PI / 180);
                        phaseAngle = (Mathf.PI / 2.0f); //I feel phase angle is ALWAYS pi/2 if its a pendulum lol
                        newAngle = 0;
                        oldP = p;
                        oldD = d;
                        break;
                    }
                }
                wearer.rigidbody2D.isKinematic = true;
                wearer.rigidbody2D.velocity = Vector2.zero;//kill unity physics with a bat

                //wearer.collider2D.enabled = false;
               
                while (Input.GetButton("P" + wearer.name[6] + ".HatMechanic"))//this seems sloppy but just want to seperate my logic looops i guess
                {
                    if(!wearer.renderer.enabled) //only happens if player died or is being teleported by a stageporter. 
                    {
                        GameObject.DestroyObject(hookInGame);
                        canSpawn = true;
                        yield break;
                    }
                    float ropeClimbSpeed = 4;
                    if (Input.GetButton("P1.Vertical"))
                    {
                        d = d - ((ropeClimbSpeed * Input.GetAxis("P1.Vertical")) * Time.deltaTime);

                    }
                    //if (Input.GetAxis("P1.Horizontal") > 0)
                    //{
                    //    Debug.Log("Moving Right");
                    //    maxTheta += ropeClimbSpeed * Time.deltaTime;
                    //    if (maxTheta > 1)
                    //        maxTheta = 1;
                    //    else if (maxTheta < -1)
                    //        maxTheta = -1;

                    //}
                    if (d != oldD)
                    {
                     
                        float newRatio = d / oldD;
                        //p = (2 * Mathf.PI) * (float)(Mathf.Sqrt(d / 9.81f));
                        
                       // t *= newRatio;
                        v = Mathf.Sqrt(9.8f / d);
                        
                        oldD = d;
                        oldP = p;
                    }
                    //if(dir > 0)
                    //    t = Mathf.Lerp(t, p, Time.deltaTime * 2);
                    //else if (dir < 0)
                    //    t = Mathf.Lerp(t, 0, Time.deltaTime * 2);
                    //t needs to ping pong between 0 and P
                    t += (Time.deltaTime * dir) * 3;
                    //if (t > p)
                    //    dir *= -1;
                    //else if (t < 0)
                    //    dir *= -1;
                    newAngle = maxTheta * Mathf.Sin(v * t + phaseAngle);
                    
                   // Debug.Log("new angle " + newAngle);
                    
                    float tempX = Mathf.Sin(newAngle) * d;
                    float tempy = Mathf.Cos(newAngle) * d;

                    wearer.transform.position = new Vector2(hookInGame.transform.position.x - tempX, hookInGame.transform.position.y - tempy);


                    //Debug.DrawLine(hookInGame.transform.position, wearer.transform.position);
                    //Debug.DrawLine(hookInGame.transform.position, new Vector2(hookInGame.transform.position.x, wearer.transform.position.y));
                    //Debug.DrawLine(wearer.transform.position, new Vector2(hookInGame.transform.position.x, wearer.transform.position.y));

                    yield return new WaitForSeconds(Time.deltaTime);
                }
                GameObject.DestroyObject(hookInGame);
                canSpawn = true;
                wearer.rigidbody2D.isKinematic = false;
                //wearer.collider2D.enabled = true;
                break;

            
            case HatType.Lollicopter:
                wearer.SendMessage("Lollicopter");
                yield return new WaitForSeconds(0.7f);
                canSpawn = true;
                break;

            case HatType.Boat:
                if(boatInGame)
                    Destroy(boatInGame);

                wearer.onBoat = false;
                wearer.ridingBoat = null;
                boatInGame = Instantiate(boatPrefab, new Vector3(mechanicSpawn.transform.position.x, mechanicSpawn.transform.position.y, 0), mechanicSpawn.rotation) as GameObject;
                yield return new WaitForSeconds(0.5f);
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
        newHat.transform.rotation = player.transform.rotation;
        //newHat.transform.rotation = new Quaternion(0, 0, 0, 1);
        newHat.rigidbody2D.isKinematic = true;
        Collider2D[] col = newHat.GetComponents<Collider2D>();
        foreach (Collider2D c in col )
        {
            c.enabled = false;
        }
        //newHat.collider2D.enabled = false;
        newHat.transform.position = player.hatHolder.position;
        newHat.transform.parent = player.hatHolder;
    }

    public static void DetachFromPlayer(Player player)
    {
        player.currentHat.wearer = null;
        player.currentHat.rigidbody2D.isKinematic = false;
        player.currentHat.collider2D.enabled = true;
        player.currentHat.transform.position = player.hatHolder.position;
        player.currentHat.transform.parent = null;
    }
}
