using UnityEngine;
using System.Collections;

public class CandyCloudAI : MonoBehaviour {

	public Vector2 moveForce;           //force applied
	public float limit;                 //candy cloud floats back and forth 
	public GameObject cloud;
	public GameObject sugar;
	public Transform spawnPt;
	private Vector2 forceNet;
	private Vector2 startPos;           //starting position

	public float coeff;                 //coefficent of friction--public for now to play around with
	private float mass;
	private float slopeAngle;           //in rads

	private bool sugarDropped;
	private bool started;//coroutine started

	// Use this for initialization
	void Start () {
		sugarDropped = false;
		started = false;
		mass = 1.0f;
		slopeAngle = 0f;
		forceNet = PhysicsEngine.CalculateNetForce(moveForce, coeff, mass);
		startPos = cloud.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		cloud.transform.Translate(new Vector3(forceNet.x, forceNet.y, 0) * Time.deltaTime);
		if(cloud.transform.position.x >= startPos.x+limit || cloud.transform.position.x <= startPos.x-limit)
		{
			forceNet *= -1;
		}
		if(!sugarDropped && !started)
		{
			started = true;
			StartCoroutine("DropDelay");
		}
		else if(sugarDropped)
		{
			Instantiate(sugar,spawnPt.position,cloud.transform.rotation);
			sugarDropped = false;
		}
	}
	IEnumerator DropDelay()
	{
		yield return new WaitForSeconds(3.0f);
		sugarDropped = true;
		started = false;
	}
	void OnCollisionEnter2D(Collision2D c)
	{
	//TODO:Collision detection with object that can destroy cloud
		//if(c.gameObject.tag == "Sugar")
			//Physics2D.IgnoreLayerCollision(c.gameObject.layer,cloud.layer);
	}

}
