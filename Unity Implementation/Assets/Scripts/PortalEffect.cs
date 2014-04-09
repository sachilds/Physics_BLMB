using UnityEngine;
using System.Collections;

public class PortalEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DestroyObject(transform.root.gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, 5));
	}
}
