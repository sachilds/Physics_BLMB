using UnityEngine;
using System.Collections;

public class PatrollingEnemyAI : MonoBehaviour {
	public float moveForce;           //force applied
	public float limit; 
	private float forceNetX;
	private Vector2 startPos;           //starting position
	
	public float coeff;                 //coefficent of friction--public for now to play around with
	private float mass;

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
	}
	
	// Update is called once per frame
	void Update () {
	
		gameObject.transform.Translate(new Vector3(forceNetX,0,0) * Time.deltaTime);
		Debug.DrawRay(gameObject.transform.position,Vector3.forward);
		if(currentState == State.PATROL)
		{
			if(gameObject.transform.position.x >= startPos.x+limit || gameObject.transform.position.x <= startPos.x-limit)
			{
				forceNetX *= -1;
				gameObject.transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
			}
			//check if player is within distance
			//if true, set currentState to State.Chase
		}
		if(currentState == State.CHASE)
		{
			//if player is outside of distance set state to State.TOSTART

			//else move in direction of player
		}
		//after chase player returns to start position
		if(currentState == State.TOSTART)
		{

		}
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
