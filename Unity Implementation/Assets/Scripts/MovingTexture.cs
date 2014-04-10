using UnityEngine;
using System.Collections;

public class MovingTexture : MonoBehaviour
{
    private Vector3 initPos;
    public Vector2 waveMove;
    public float speedX, speedY;
    

    // Use this for initialization
    void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
                initPos.x + Mathf.PingPong(Time.time * speedX, waveMove.x), 
                initPos.y + Mathf.PingPong(Time.time * speedY, waveMove.y),
                0);
    }
}