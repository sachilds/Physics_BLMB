using UnityEngine;
using System.Collections;

public class PatrollingEnemyAI : MonoBehaviour {
	public float moveForce;           //force applied
	public float limit; 
	private float forceNetX;
	private Vector2 startPos;           //starting position

	public float coeff;                 //coefficent of friction--public for now to play around with
	private float mass;
	private GameObject player1;
	private GameObject player2;
	private bool twoPlayers;
	private float p1Dist;
	private float p2Dist;
	private float chaseArea; // if player is within distance enemy will chase

	private enum State
	{
		PATROL,
		CHASE,
		TOSTART
	};
	private State currentState;
	// Use this for initialization
	void Start () {
		startPos = gameObject.transform.position;
		currentState = State.PATROL;
		mass = 1.0f;
		forceNetX = PhysicsEngine.HorizontalNetForce(moveForce,coeff,mass);
		//sets enemy to face the right
		gameObject.transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
	
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");
		chaseArea = 5.0f;


		if(player2 == null)
			twoPlayers = false; //only one player
		else
			twoPlayers = true;
	}
	
	// Update is called once per frame
	void Update () {
	
		gameObject.transform.Translate(new Vector3(forceNetX,0,0) * Time.deltaTime);
		Debug.DrawRay(gameObject.transform.position,Vector3.forward);
		if(currentState == State.PATROL)
		{
			Debug.Log("PATROLLING");
			if(gameObject.transform.position.x >= startPos.x+limit || gameObject.transform.position.x <= startPos.x-limit)
			{
				forceNetX *= -1;
				gameObject.transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
			}
			//check if player is within distance
			//if true, set currentState to State.Chase
			if(twoPlayers)
			{
				Debug.Log("TWO PLAYERS");
				CheckTwoPlayerDistance();

				if(p1Dist <= chaseArea || p2Dist <= chaseArea)
					currentState = State.CHASE;

			}
			else
			{
				Debug.Log("ONE PLAYER");
				CheckOnePlayerDistance();
				if(p1Dist <= chaseArea)
					currentState = State.CHASE;
			}
		}
		if(currentState == State.CHASE)
		{
			Debug.Log("CHASING");
			//if player is outside of distance set state to State.TOSTART
			//else move in direction of player
			if(twoPlayers)
			{
				CheckTwoPlayerDistance();
				if(p1Dist <= chaseArea || p2Dist <= chaseArea)//if still within chase area
				{
					if(p1Dist <= p2Dist)
						//chase player 1
						Debug.Log("chasing p1");
					else if(p1Dist > p2Dist)
							//chase player 2
						Debug.Log("chasing p2");
				}
				else
				{
					currentState = State.TOSTART;
				}

			}
			else // if one player
			{
				CheckOnePlayerDistance();
				if(p1Dist <= chaseArea)//if still within chase area
				{
					//chase player 1
					Debug.Log("chasing ONLY p1");
			
				}
				else
				{
					currentState = State.TOSTART;
				}
			}
		}
		//after chase player returns to start position
		if(currentState == State.TOSTART)
		{
			Debug.Log("TO START");


		}
	}

	void CheckTwoPlayerDistance()
	{
		p1Dist = Vector3.Distance(gameObject.transform.position,player1.transform.position);
		p2Dist = Vector3.Distance(gameObject.transform.position,player2.transform.position);
	}
	void CheckOnePlayerDistance()
	{
		p1Dist = Vector3.Distance(gameObject.transform.position,player1.transform.position);
	}
	void OnCollisionEnter2D(Collision2D c)
	{
		Debug.Log("is COlliding");
		/*if(c.gameObject.tag == "Ground")
		{
			forceNetX *=-1;
		}*/
	}
}
