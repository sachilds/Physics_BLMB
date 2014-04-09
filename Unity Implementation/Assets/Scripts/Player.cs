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


    // TEMP delete later once hats are added
    public GameObject jelloBlock;
    private GameObject jelloInGame;

    public GameObject cannonPrefab;
    private GameObject cannonInGame;

    public Hat.HatType hatType;
    private bool canSpawn;

	void Awake() {
		base.Awake();

		playerInput = GetComponent<PlayerInputScript>();	//Get reference to PlayerInputScript component
        jelloBlock = Resources.Load("Prefabs/JELLO_CUBE") as GameObject;
        spawningEffect = Resources.Load("Prefabs/PorterEffect") as GameObject;
        canSpawn = true;
        hatType = Hat.HatType.Jello_Spawn;
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
        if (c.tag == "SLIPPERY_SURFACE")
            groundType = GroundType.SLIPPERY;
        else if (c.tag == "STICKY_SURFACE")
            groundType = GroundType.STICKY;

        Debug.Log("Entered a trigger, tag was: " + c.tag);
    }

    void OnTriggerExit2D(Collider2D c) {
        groundType = GroundType.REGULAR;
        Debug.Log("Left a trigger, tag was: " + c.tag);
    }

    void OnCollisionEnter2D(Collision2D c) {
        if (c.gameObject.tag == "JELLO_BLOCK") { 
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
    public void UseHatMechanic() {
        switch (hatType)
        {
            case Hat.HatType.None:
                break;
            case Hat.HatType.OneHat:
                break;
            case Hat.HatType.AnotherHat:
                break;
            case Hat.HatType.Jello_Spawn:
                if (jelloBlock && canSpawn) {
                    canSpawn = false;
                    StartCoroutine("Spawn");
                }
                break;
            default:
                break;
        }
    }

    public void CancelHatMechanic() {
        switch (hatType) {
            case Hat.HatType.None:
                break;
            case Hat.HatType.OneHat:
                break;
            case Hat.HatType.AnotherHat:
                break;
            case Hat.HatType.Jello_Spawn:
                Destroy(jelloInGame);
                break;
            case Hat.HatType.Candy_Cannon:
                Destroy(cannonInGame);
                break;
            default:
                break;
        }
    }

    private IEnumerator Spawn() {
        if (hatType == Hat.HatType.Jello_Spawn) {
            if (!jelloInGame) {
                jelloInGame = Instantiate(jelloBlock, new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
            }
            else {
                Destroy(jelloInGame);
                jelloInGame = Instantiate(jelloBlock, new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
            }
            yield return new WaitForSeconds(0.5f);
            canSpawn = true;
        }
        else {
            Debug.Log("I fell through...");
            yield return new WaitForSeconds(0);
        }

    }
}
