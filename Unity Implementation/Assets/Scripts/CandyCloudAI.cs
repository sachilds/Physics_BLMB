using UnityEngine;
using System.Collections;

public class CandyCloudAI : MonoBehaviour {

	public Vector2 moveForce;           //force applied
	public float limit;                 //candy cloud floats back and forth 
	public GameObject cloud;
	private Vector2 forceNet;
	private Vector2 startPos;           //starting position

	public float coeff;                 //coefficent of friction--public for now to play around with
	private float mass;
	private float slopeAngle;           //in rads

	// Use this for initialization
	void Start () {
	
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
	}
	//TODO:Collision detection with object that can destroy cloud
}
