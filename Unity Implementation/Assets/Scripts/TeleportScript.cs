using UnityEngine;
using System.Collections;

public enum TeleporterType
{
    Within_Stage,
    StageTransition,
}
public class TeleportScript : MonoBehaviour {
    private static bool PorterReady = true;  
    private Transform MovingTransform;
    private float speed = 30;

    public TeleporterType Type;
    public Transform WarpToPortal;


    public void OnTriggerStay2D (Collider2D c){
        
        if (PorterReady && c.tag == "Player"){
            PorterReady = false;
            MovingTransform = c.transform;
            
            StartCoroutine("TeleportPlayer");
        }
    }

    private IEnumerator TeleportPlayer(){
        SpriteRenderer renderer = MovingTransform.gameObject.GetComponent<SpriteRenderer>();
        Collider2D col = MovingTransform.gameObject.GetComponent<BoxCollider2D>();
        Rigidbody2D rig = MovingTransform.gameObject.GetComponent<Rigidbody2D>();

        renderer.enabled = false;
        col.enabled = false;
        rig.isKinematic = true;
        
        while (true)
        {
            Debug.Log("Here");
            MovingTransform.position = Vector3.Lerp(MovingTransform.position,
                 WarpToPortal.transform.position,
                 speed * Time.deltaTime);
            if (MovingTransform.position == WarpToPortal.position)
            {
                break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        renderer.enabled = true;
        col.enabled = true;
        rig.isKinematic = false;

        yield return new WaitForSeconds(1);
        PorterReady = true;
       
            
    }
}
