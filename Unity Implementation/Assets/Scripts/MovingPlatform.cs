using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
    public Vector2 DistanceFromOrigin;
    public Vector2 Speed;
    private Vector2 originalPos;

    private bool horizontal, vertical;
	// Use this for initialization
	void Start () {
        originalPos = transform.position;
        if (Speed.x != 0)
            horizontal = true;
        if (Speed.y != 0)
            vertical = true;
        Debug.Log(originalPos.ToString());
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 newPos = new Vector2(originalPos.x + Mathf.PingPong(Time.time * Speed.x, DistanceFromOrigin.x),
                                      originalPos.y + Mathf.PingPong(Time.time * Speed.y, DistanceFromOrigin.y));
        if (horizontal && vertical)
        {
            transform.position = new Vector3(
                 newPos.x,
                 newPos.y,
                 transform.position.z);
        }
        else if (horizontal)
        {
            transform.position = new Vector3(
                 newPos.x,
                 originalPos.y,
                 transform.position.z);
        }
        else if (vertical)
        {
            transform.position = new Vector3(
                 originalPos.x,
                 newPos.y,
                 transform.position.z);
        }
        else
        {
            Debug.Log("No Speeds were set for the Moving Platform");
        }

        
	}
}
