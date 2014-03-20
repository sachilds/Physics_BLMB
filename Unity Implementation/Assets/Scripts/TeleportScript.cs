using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum TeleporterType
{
    Within_Stage,
    StageTransition,
}

public class TeleportScript : MonoBehaviour {
    private static bool porterOverride = false;
    private Transform MovingTransform;
    private float speed = 30;

    public TeleporterType PorterType;
    public Transform WarpToPortal;
    private static bool Standby = false;

    public void OnTriggerEnter2D(Collider2D c)
    {
        if (c.GetComponent<SpriteRenderer>().enabled)
        {
            if (c.tag == "Player" && PorterType == TeleporterType.Within_Stage)
            {
                MovingTransform = c.transform;
                StartCoroutine("TeleportPlayer", c.transform);
            }
            if (c.tag == "Player" && PorterType == TeleporterType.StageTransition)
            {//could not for the life of me convert the char '1' from Stage1 to an int so did this
                char temp = name[5];
                char temp2 = WarpToPortal.name[5];
               Level_Manager.Instance.ChangeSegments(temp, temp2);
               porterOverride = true;
               StartCoroutine("TeleportPlayer", Level_Manager.Instance.Player1.transform);
               StartCoroutine("TeleportPlayer", Level_Manager.Instance.Player2.transform);
            }

        }


    }

    public static void SpawnPlayer()
    {
        
        Standby = false;
    }
    private IEnumerator TeleportPlayer(Transform pTransform){
       
        SpriteRenderer renderer = pTransform.gameObject.GetComponent<SpriteRenderer>();
        Collider2D col = pTransform.gameObject.GetComponent<BoxCollider2D>();
        Rigidbody2D rig = pTransform.gameObject.GetComponent<Rigidbody2D>();

        renderer.enabled = false;
        col.enabled = false;
        rig.isKinematic = true;
      

        while (true)
        {
            Debug.Log("teleporting " + pTransform.ToString() + " to " + PorterType);

            if (porterOverride && PorterType == TeleporterType.Within_Stage)
            {
                break;
            }
                
            pTransform.position = Vector3.Lerp(pTransform.position,
                 WarpToPortal.transform.position,
                 speed * Time.deltaTime);
            if (pTransform.position == WarpToPortal.position)
            {
                break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (PorterType == TeleporterType.Within_Stage && !porterOverride)
        {
            col.enabled = true;
            rig.isKinematic = false;
            yield return new WaitForSeconds(0.1f);
            if (!porterOverride)
            {
               renderer.enabled = true;
            }
        }
        else if (PorterType == TeleporterType.StageTransition)
        {
            Standby = true;
            while (Standby)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                
            }
            col.enabled = true;
            rig.isKinematic = false;
            yield return new WaitForSeconds(0.3f);

            renderer.enabled = true;
            porterOverride = false;
        }

        
        

       
            
    }
}
