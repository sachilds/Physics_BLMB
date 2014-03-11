using UnityEngine;
using System.Collections;

public class CustomRigidBody : MonoBehaviour {
	public float mass;
	public float gravity;
	private bool onGround;
	private Vector2 normalForce;
	// Use this for initialization
	void Start () {
		normalForce = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(0,(mass * gravity +normalForce.y) *Time.deltaTime,0);
		Collider2D coll = this.collider2D;
		Debug.Log(coll.gameObject+" " +coll.name+" "+ coll.gameObject);
	}
	public void AddForce(Vector2 force)
	{
		this.transform.Translate(force.x * Time.deltaTime, force.y * Time.deltaTime,0);
	}
	void OnCollisionEnter2D(Collision2D c)
	{
		Debug.Log("Colliding");
		if(c.gameObject.tag == "Ground")
		{
			normalForce.y = mass * gravity * -1;
		}
	}
	void OnCollisionExit2D(Collision2D c)
	{
		if(c.gameObject.tag == "Ground")
		{
			normalForce.y = 0;
		}
	}
}
