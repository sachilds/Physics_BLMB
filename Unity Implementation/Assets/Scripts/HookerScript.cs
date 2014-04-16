using UnityEngine;
using System.Collections;

public class HookerScript : MonoBehaviour {
    private float ropeScale;
    public int numberOfOwner;
    GameObject ownersObject;
	void Start () {
        
	}
    public void setNumOfOwner(int value)
    {
        Debug.Log("Setting number of owner");
        numberOfOwner = value;
        ownersObject = GameObject.Find("Player" + value);
        Debug.Log(ownersObject.name);
    }
   public void OnTriggerEnter2D(Collider2D c)
    {
       GetComponent<Rigidbody2D>().isKinematic = true;
       float d = Vector2.Distance(ownersObject.transform.position, transform.position);
       Debug.Log(d.ToString());
    
   }
}
