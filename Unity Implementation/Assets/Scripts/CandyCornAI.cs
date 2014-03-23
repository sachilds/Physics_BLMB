using UnityEngine;
using System.Collections;

public class CandyCornAI : MonoBehaviour {
	public Vector2 dropForce;//force applied when dropping
	public Vector2 upForce;//force applied when block is returning to startPos
	public GameObject block;

	private Vector2 forceNetDrop;
	private Vector2 forceNetUp;
	private Vector2 startPos;//starting position
	private int waitTime;//drop delay time randomized

	private bool isDropping;
	private bool hitGround;//block has hit ground

	public float coeff;//coefficent of friction--public for now to play around with
	private float mass;
	private float slopeAngle;//in rads

	// Use this for initialization
	void Start () {
		waitTime = Random.Range(2,6);//number between 2 and 5s
		isDropping = false;
		hitGround = false;

		startPos = block.transform.position;
		mass = 3.0f;
		slopeAngle = 0f;

		forceNetDrop = PhysicsEngine.CalculateNetForce(dropForce, coeff, mass);
		forceNetUp = PhysicsEngine.CalculateNetForce(upForce, coeff, mass);

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("isDropping = " +isDropping + " hitGround = " +hitGround);
		Debug.Log("ForceNetDrop.x "+forceNetDrop.x+" forceNetDrop.y "+forceNetDrop.y);
		if(isDropping && !hitGround)
		{
			//physics to make it drop
			block.transform.Translate(new Vector3(forceNetDrop.x,forceNetDrop.y,0)*Time.deltaTime);
		}
		else if(isDropping && hitGround)
		{
			//physics to return it to starting position
			block.transform.Translate(new Vector3(forceNetUp.x,forceNetUp.y,0)*Time.deltaTime);
			if(block.transform.position.y >= startPos.y)
			{
				hitGround = false;
				isDropping = false;
			}
		}
		else if(!isDropping)
		{
			StartCoroutine("DropDelay");
		}
	}
	IEnumerator DropDelay()
	{
		yield return new WaitForSeconds(waitTime);
		isDropping = true;
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		Debug.Log("COLLISION");
		if(c.gameObject.tag == "Ground")
		{
			hitGround = true;
		}
	}

}
