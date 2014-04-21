using UnityEngine;
using System.Collections;

public class HookerScript : MonoBehaviour {
    private float ropeScale;
    public int numberOfOwner;
    GameObject ownersObject;
    public bool isHooked = false;
	void Start () {
        
	}
    public void setNumOfOwner(int value)
    {
        
        numberOfOwner = value;
        ownersObject = GameObject.Find("Player" + value);
        
    }
   public void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Hookable")
        {
            isHooked = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            GetComponent<Rigidbody2D>().isKinematic = true;
        }
       

    
   }
}
