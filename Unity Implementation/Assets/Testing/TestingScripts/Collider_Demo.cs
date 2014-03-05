using UnityEngine;
using System.Collections;

public class Collider_Demo : MonoBehaviour {

    public enum MaterialType { 
        Regular, Slippery, Sticky, Bouncy
    }

    public Vector2 size;
    public Vector2 center;
    public MaterialType material;

    public Character sprite;

	// Use this for initialization
	void Start () {
        size = sprite.size;
        center = Vector2.zero;
        material = MaterialType.Regular;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
