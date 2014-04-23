using UnityEngine;
using System.Collections;

public class CandyCloudAI : MonoBehaviour {

	public float moveForce;           //force applied
	public float limit;                 //candy cloud floats back and forth 
	public GameObject cloud;
	public GameObject sugar;
	public Transform spawnPt;
	private float forceNetX;
	private Vector2 startPos;           //starting position

	public float coeff;                 //coefficent of friction--public for now to play around with
	private float mass;
	//private float slopeAngle;           //in rads

	private bool sugarDropped;

	// Use this for initialization
	void Start () {
		sugarDropped = false;
		mass = 1.0f;
		//slopeAngle = 0f;
		forceNetX = PhysicsEngine.HorizontalNetForce(moveForce, coeff, mass);
		startPos = cloud.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		cloud.transform.Translate(new Vector3(forceNetX, 0, 0) * Time.deltaTime);
		if(cloud.transform.position.x >= startPos.x+limit || cloud.transform.position.x <= startPos.x-limit)
		{
			forceNetX *= -1;
		}
		if(!sugarDropped)
		{
			sugarDropped = true;
			StartCoroutine("DropDelay");
		}
	}
	IEnumerator DropDelay()
	{
		yield return new WaitForSeconds(3.0f);
		Instantiate(sugar,spawnPt.position,cloud.transform.rotation);
		sugarDropped = false;
	}
	void OnCollisionEnter2D(Collision2D c)
	{
	//TODO:Collision detection with object that can destroy cloud
		//if(c.gameObject.tag == "Sugar")
			//Physics2D.IgnoreLayerCollision(c.gameObject.layer,cloud.layer);
	}

}
