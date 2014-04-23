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
	private bool faceLeft;//if player is facing left true

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
		forceNetX = PhysicsEngine.HorizontalNetForce(moveForce, coeff, mass);
		//sets enemy to face the right
		gameObject.transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
		faceLeft = false;
		player1 = GameObject.Find("Player1");
		try{
			player2 = GameObject.Find("Player2");
		}
		catch(UnityException e)
		{
			Debug.Log(e);
			twoPlayers = false;
		}
		chaseArea = 5.0f;


		if(player2 == null)
			twoPlayers = false; //only one player
		else
			twoPlayers = true;
	}
	
	// Update is called once per frame
	void Update () {
	
		//CheckTwoPlayerDistance();
		gameObject.transform.Translate(new Vector3(forceNetX,0,0) * Time.deltaTime);
		//Debug.DrawRay(gameObject.transform.position,Vector3.forward);
		if(currentState == State.PATROL)
		{
			if(gameObject.transform.position.x >= startPos.x+limit || gameObject.transform.position.x <= startPos.x-limit)
			{
				forceNetX *= -1;
				faceLeft = !faceLeft;
				gameObject.transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
			}
			//check if player is within distance
			//if true, set currentState to State.Chase
			if(twoPlayers)
			{
				CheckTwoPlayerDistance();

				if(p1Dist <= chaseArea || p2Dist <= chaseArea)
					currentState = State.CHASE;

			}
			else
			{
				CheckOnePlayerDistance();
				if(p1Dist <= chaseArea)
					currentState = State.CHASE;
			}
		}
		if(currentState == State.CHASE)
		{
			//if player is outside of distance set state to State.TOSTART
			//else move in direction of player
			if(twoPlayers)
			{
				CheckTwoPlayerDistance();
				if(p1Dist <= chaseArea || p2Dist <= chaseArea)//if still within chase area
				{
					#region Chase Player 1
					if(p1Dist <= p2Dist)
					{
						//chase player 1
						Debug.Log("chasing p1");
						if(player1.transform.position.x < gameObject.transform.position.x)//if p1 is to the left
						{
							Debug.Log("player 1 left");
							if(!faceLeft)//if enemy not facing left
							{
								faceLeft = true;
								forceNetX *= -1;
								gameObject.transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
							}
						}
						else if(player1.transform.position.x > gameObject.transform.position.x)//if p1 is to the right
						{
							if(faceLeft)
							{
								faceLeft = false;
								forceNetX *= -1;
								gameObject.transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
							}
						}
					}
					#endregion
					#region Chase Player 2
					else if(p1Dist > p2Dist)
					{
							//chase player 2
						Debug.Log("chasing p2");
						if(player2.transform.position.x < gameObject.transform.position.x)//if p2 is to the left
						{
							if(!faceLeft)//if enemy not facing left
							{
								faceLeft = true;
								forceNetX *= -1;
								gameObject.transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
							}
						}
						else if(player2.transform.position.x > gameObject.transform.position.x)//if p2 is to the right
						{
							if(faceLeft)
							{
								faceLeft = true;
								forceNetX *= -1;
								gameObject.transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
							}
						}
					}
					#endregion
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
					if(player1.transform.position.x < gameObject.transform.position.x)//if p1 is to the left
					{
						if(!faceLeft)//if enemy not facing left
						{
							faceLeft = true;
							forceNetX *= -1;
							gameObject.transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
						}
					}
					else if(player1.transform.position.x > gameObject.transform.position.x)//if p1 is to the right
					{
						if(faceLeft)
						{
							faceLeft = false;
							forceNetX *= -1;
							gameObject.transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
						}
					}
			
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
			#region Checking to Chase
			if(twoPlayers)
			{
				CheckTwoPlayerDistance();
				if(p1Dist <= chaseArea || p2Dist <= chaseArea)
					currentState = State.CHASE;
			}
			else if(!twoPlayers)
			{
				CheckOnePlayerDistance();
				if(p1Dist <= chaseArea)
					currentState = State.CHASE;
			}
			#endregion
			if(Vector3.Distance(startPos,gameObject.transform.position)>= -0.2 && Vector3.Distance(startPos,gameObject.transform.position)<= 0.2)
			{
				currentState = State.PATROL;
			}
			if(startPos.x < gameObject.transform.position.x)//if startPos is to the left
			{
				if(!faceLeft)
				{
					faceLeft = true;
					forceNetX *= -1;
					gameObject.transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
				}
			}
			else if(startPos.x > gameObject.transform.position.x)//if start pos is to the right
			{
				if(faceLeft)
				{
					faceLeft = false;
					forceNetX *= -1;
					gameObject.transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
				}
			}
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
		/*if(c.gameObject.tag == "Ground")
		{
			forceNetX *=-1;
		}*/
	}
}
