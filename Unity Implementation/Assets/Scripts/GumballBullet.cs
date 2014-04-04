using UnityEngine;
using System.Collections;

public class GumballBullet : MonoBehaviour {
	public float shotForce; 
	public float coeff;
	private float mass;
	private float shotForceNet;
	// Use this for initialization
	void Start () {
		Destroy(gameObject,3.0f);
		mass = 1.0f;
		shotForceNet = PhysicsEngine.HorizontalNetForce(shotForce,coeff,mass);
	}
	
	// Update is called once per frame
	void Update () {
	//TODO: figure out how to take into account gravity without screwing up firing
		gameObject.transform.Translate(new Vector3(shotForceNet,0,0)*Time.deltaTime);
	}
	void OnCollisionEnter2D(Collision2D c)
	{
		//if(c.gameObject.tag!= "Turret")
			//Destroy(gameObject); //TODO: uncomment that later
	}
}
