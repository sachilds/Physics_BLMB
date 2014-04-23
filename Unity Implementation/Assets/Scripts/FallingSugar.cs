using UnityEngine;
using System.Collections;

public class FallingSugar : MonoBehaviour {
	public GameObject sugar;
	public float coeff;//coefficent of friction--public for now to play around with
	public Vector2 dropForce;//force applied while dropping
	private Vector2 forceNetDrop;//net drop force
	private float mass;
	private bool hitGround;

	// Use this for initialization
	void Start () {
		mass = 1.0f;
		hitGround = false;
		forceNetDrop = CalculateVerticalForce(dropForce,mass);
		Destroy(gameObject,5.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(!hitGround)
			sugar.transform.Translate(new Vector3(forceNetDrop.x,forceNetDrop.y,0)*Time.deltaTime);
	}
	//temp vertical force
	Vector2 CalculateVerticalForce(Vector2 appliedForce, float mass)
	{
		float fw = PhysicsEngine.GRAVITY * mass;//force weight
		float fn = fw + appliedForce.y;//normal force
		
		//Fy net = Fnormal+Fyapplied-Fw
		return new Vector2(0,fn+appliedForce.y-fw);
	}
	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.tag == "Ground")
			hitGround = true;
		//if(c.gameObject.tag == "Cloud")
			//Physics2D.IgnoreLayerCollision(c.gameObject.layer,sugar.layer);
	}
}
