using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_Manager : MonoBehaviour {
    public static Level_Manager Instance;
    [HideInInspector]
    public CheckPoint[] checkPoints;
    [HideInInspector]
    public int currentSegment = 0;
    [HideInInspector]
    public Transform spawnPosition;
    [HideInInspector]
    public Segment_Script[] mTheSegments;
    
    [HideInInspector]
    public static int NumberOfPlayers = 1; // set this for now till we handle the mainmenu and setting up 2 players
    [HideInInspector]
    public GameObject Player1, Player2;
	
    void Start () {
        Player1 = GameObject.Find("Player1");

        GameObject[] tempPoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        checkPoints = new CheckPoint[tempPoints.Length];
        for (int i = 0; i < tempPoints.Length; i++)
        {
            checkPoints[i] = tempPoints[i].GetComponent<CheckPoint>();
        }

        if (Player2 = GameObject.Find("Player2"))
        {
            NumberOfPlayers++;
            mTheSegments = new Segment_Script[5];
            mTheSegments[0] = GameObject.Find("Stage1").GetComponent<Segment_Script>();
            mTheSegments[1] = GameObject.Find("Stage2").GetComponent<Segment_Script>();
            mTheSegments[2] = GameObject.Find("Stage3").GetComponent<Segment_Script>();
            mTheSegments[3] = GameObject.Find("Stage4").GetComponent<Segment_Script>();
            mTheSegments[4] = GameObject.Find("Stage5").GetComponent<Segment_Script>();
        }
        else
        {
            mTheSegments = new Segment_Script[3];
            mTheSegments[0] = GameObject.Find("Stage1").GetComponent<Segment_Script>();
            mTheSegments[1] = GameObject.Find("Stage2").GetComponent<Segment_Script>();
            mTheSegments[2] = GameObject.Find("Stage3").GetComponent<Segment_Script>();
        }

        Instance = this;
        
        spawnPosition = mTheSegments[0].defaultSpawn;

        mTheSegments[0].ResetSegments();
        

       // StartCoroutine("BeginLevelPause");
	}
    public void resetCheckPoints()
    {
        foreach (CheckPoint c in checkPoints)
        {
            c.Deactivate();
        }
    }
    public void ChangeSegments(char lastSegment, char newSegment)
    {
        resetCheckPoints();
        int prev = 0;
        int next = 0;
        switch (lastSegment)
        {
            case '1':
                prev = 0;
                break;
            case '2':
                prev = 1;
                break;
            case '3':
                prev = 2;
                break;
            case '4':
                prev = 3;
                break;
            case '5':
                prev = 4;
                break;
            default:
                Debug.Log("the Segments need to be Labeled StageX, where X is the next index");
                break;
        }
        switch (newSegment)
        {
            case '1':
                next = 0;
                break;
            case '2':
                next = 1;
                break;
            case '3':
                next = 2;
                break;
            case '4':
                next = 3;
                break;
            case '5':
                next = 4;
                break;
            default:
                Debug.Log("the Segments need to be Labeled StageX, where X is the next index");
                break;
        }
        Debug.Log("prev " + prev + " next " + next);
        if (prev < next)
        {
           currentSegment++;
            spawnPosition = mTheSegments[next].defaultSpawn;
        }
        else
        {
            currentSegment--;
            spawnPosition = mTheSegments[next].defaultEndSpawn;
        }
        mTheSegments[prev].RemoveSegment();
        mTheSegments[next].SpawnSegment();
       

    }

    public void KillPlayer(Transform pTransform)
    {
        StartCoroutine("TeleportPlayerToSpawn", pTransform);
    }
    public void setSpawnPos(Transform cTransform)
    {
        spawnPosition = cTransform;
    }
    private IEnumerator BeginLevelPause()
    {
        Debug.Log("Running the pause");
        //can display a Get ready or some kind of declaration
        yield return new WaitForSeconds(5);
        Debug.Log("Stopping the pause");
        
    }

    public IEnumerator TeleportPlayerToSpawn(Transform pTransform)
    {
      
        float warpSpeed = 30;
        Instantiate(Player.spawningEffect, pTransform.position, Quaternion.identity);

        SpriteRenderer renderer = pTransform.gameObject.GetComponent<SpriteRenderer>();
        Collider2D col = pTransform.gameObject.GetComponent<BoxCollider2D>();
        Rigidbody2D rig = pTransform.gameObject.GetComponent<Rigidbody2D>();

        renderer.enabled = false;
        col.enabled = false;
        rig.isKinematic = true;


        while (true)
        {
          
            if (TeleportScript.porterOverride)
                break;
            pTransform.position = Vector3.Lerp(pTransform.position,
                 spawnPosition.position,
                 warpSpeed * Time.deltaTime);
            if (pTransform.position == spawnPosition.position)
            {
                break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (!TeleportScript.porterOverride)
        {
            Instantiate(Player.spawningEffect, pTransform.position, Quaternion.identity);
            col.enabled = true;
            rig.isKinematic = false;
            yield return new WaitForSeconds(0.1f);
            renderer.enabled = true;

        }
    
    }
}
