using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour
{
    
    // The targets we are following
    public Transform[] target;

    private float ZoomSpeed = 2;
    private float minDistance = 8;
    // The distance in the x-z plane to the target
    public float closestZoom;//Sets the closest distance the camera will go to both players
    private float distance;

    void Start()
    {
        target = new Transform[2];
        target[0] = GameObject.Find("Player1").transform;
        target[1] = GameObject.Find("Player2").transform;
      
        distance = closestZoom;
    }
    void LateUpdate()
    {
        // Early out if we don't have a target
        if (!target[0])
            return;
        //some vector math
        Vector2 disBetweenAB = new Vector3(target[1].position.x - target[0].position.x,
                                           target[1].position.y - target[0].position.y);
        //getting magnitue of player1 to player 2
        float distanceMagnitude = Mathf.Sqrt(Mathf.Pow(disBetweenAB.x, 2) + Mathf.Pow(disBetweenAB.y, 2));
        
        if (distanceMagnitude >= minDistance)
        {
            distance = 30 * (distanceMagnitude / minDistance);
        }
        else
        {
            distance = closestZoom;
        }
        //distance *= 0.95;
        Mathf.Clamp(distance, closestZoom, 1200);
        // sets position to center of the 2 players
        Vector2 center = (target[1].position + target[0].position) / 2;

        //transform.position -= currentRotation * Vector3.forward * distance;


        transform.position = Vector3.Lerp(transform.position, new Vector3(center.x, center.y, -distance), ZoomSpeed * Time.deltaTime);

       
    }
}