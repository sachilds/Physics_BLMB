using UnityEngine;
using System.Collections;

public enum CameraState
{
    ONE_PLAYER_FOLLOW,
    TWO_PLAYER_FOLLOW,
    LOCKED,
}
public class CameraController : MonoBehaviour
{
   
    public CameraState camState;
    // The targets the camera is following
    [HideInInspector]
    public Transform[] target;
    private float distance;

    //1Player values
    private float ScrollSpeed = 4;
    public float soloMinDis = 10;
    //private Vector2 origin = new Vector2();

    //2Player values
    private float ZoomSpeed = 2;
    private float minDistance = 8;
    // The distance in the x-z plane to the target
    public float closestZoom;//Sets the closest distance the camera will go to both players
    
    

    void Start()
    {
        target = new Transform[2];
        target[0] = GameObject.Find("Player1").transform;
        if (camState == CameraState.TWO_PLAYER_FOLLOW)
        {
            target[1] = GameObject.Find("Player2").transform;
        }
        else if (camState == CameraState.ONE_PLAYER_FOLLOW)
        {
            transform.position = new Vector3(target[0].transform.position.x, 
                                              target[0].transform.position.y,
                                              transform.position.z);
        }
        distance = closestZoom;
    }
    void LateUpdate()
    {
        // Early out if we don't have a target
        if (!target[0])
        {
            Debug.Log("ERROR::Camera has no Target");
            return;
        }

        Vector2 disBetweenAB;
        float distanceMagnitude;

        switch (camState)
        {

            case CameraState.ONE_PLAYER_FOLLOW:
                float MAX_X = 1.5f;
                float MAX_Y = 1.5f;
                float xMovement = 0;
                float yMovement = 0;
            //Debug.Log("One Player Camera not implemented yet");
                
                disBetweenAB = new Vector2(target[0].position.x - transform.position.x, target[0].position.y - transform.position.y);
               if(disBetweenAB.x > MAX_X)
               {
                   xMovement = disBetweenAB.x - MAX_X;
               }
               else if (disBetweenAB.x < -MAX_X)
               {
                   xMovement = disBetweenAB.x + MAX_X;
               }
               if (disBetweenAB.y > MAX_Y)
               {
                   yMovement = disBetweenAB.y - MAX_Y;
               }
               else if (disBetweenAB.y < -MAX_Y)
               {
                   yMovement = disBetweenAB.y + MAX_Y;
               }

                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + xMovement, 
                                                                                  transform.position.y + yMovement,
                                                                                  -soloMinDis), Time.deltaTime * ScrollSpeed);
              
                break;
            case CameraState.TWO_PLAYER_FOLLOW:
               
                //some vector math
                disBetweenAB = new Vector3(target[1].position.x - target[0].position.x,
                                                   target[1].position.y - target[0].position.y);
                //getting magnitue of player1 to player 2
                distanceMagnitude = Mathf.Sqrt(Mathf.Pow(disBetweenAB.x, 2) + Mathf.Pow(disBetweenAB.y, 2));

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
                break;
            case CameraState.LOCKED:
                break;
            default:
                break;
        }
    }
}