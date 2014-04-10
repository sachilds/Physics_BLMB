using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            Level_Manager.Instance.KillPlayer(c.transform);
        }
    }
}
