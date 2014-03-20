using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_Manager : MonoBehaviour {
    Segment_Script[] mTheSegments;
    public static Level_Manager Instance;
    public int NumberOfPlayers = 2; // set this for now till we handle the mainmenu and setting up 2 players
    public GameObject Player1, Player2;
	// Use this for initialization
	void Start () {
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");
        Instance = this;
        mTheSegments = new Segment_Script[3];
        mTheSegments[0] = GameObject.Find("Stage1").GetComponent<Segment_Script>();
        mTheSegments[1] = GameObject.Find("Stage2").GetComponent<Segment_Script>();
        mTheSegments[2] = GameObject.Find("Stage3").GetComponent<Segment_Script>();
        
        mTheSegments[0].ResetSegments();

       // StartCoroutine("BeginLevelPause");
	}

    public void ChangeSegments(char lastSegment, char newSegment)
    {
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
            default:
                Debug.Log("the Segments need to be Labeled StageX, where X is the next index");
                break;
        }
        mTheSegments[prev].StartDropping();
        mTheSegments[next].StartRising();
    }

    private IEnumerator BeginLevelPause()
    {
        Debug.Log("Running the pause");
        //can display a Get ready or some kind of declaration
        yield return new WaitForSeconds(5);
        Debug.Log("Stopping the pause");
        
    }

}
