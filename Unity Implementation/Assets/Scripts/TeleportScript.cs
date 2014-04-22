using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum TeleporterType
{
    Within_Stage,
    StageTransition,
}

public class TeleportScript : MonoBehaviour {
    public static bool porterOverride = false;
    private Transform MovingTransform;
    private float speed = 15;

    public TeleporterType PorterType;
    public Transform WarpToPortal;
   

    public void OnTriggerEnter2D(Collider2D c)
    {
        if (c.GetComponent<SpriteRenderer>())
        {
            if (c.GetComponent<SpriteRenderer>().enabled)
            {
                if (c.tag == "Player" && PorterType == TeleporterType.Within_Stage)
                {
                    MovingTransform = c.transform;
                    StartCoroutine("TeleportPlayerWithin", c.transform);
                }
                if (c.tag == "Player" && PorterType == TeleporterType.StageTransition)
                {//could not for the life of me convert the char '1' from Stage1 to an int so did this
                    char temp = name[5];
                    char temp2 = WarpToPortal.name[5];
                    Level_Manager.Instance.ChangeSegments(temp, temp2);
                    StartCoroutine("TeleportPlayerToNext", Level_Manager.Instance.Player1.transform);
                    if(Level_Manager.NumberOfPlayers == 2)
                        StartCoroutine("TeleportPlayerToNext", Level_Manager.Instance.Player2.transform);
                }
            }
        }


    }


    
    private IEnumerator TeleportPlayerWithin(Transform pTransform){
        Instantiate(Player.spawningEffect, pTransform.position, Quaternion.identity);
        
        SpriteRenderer renderer = pTransform.gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer[] hRenderer = pTransform.GetComponentsInChildren<SpriteRenderer>();

        Collider2D col = pTransform.gameObject.GetComponent<BoxCollider2D>();
        Rigidbody2D rig = pTransform.gameObject.GetComponent<Rigidbody2D>();
        Debug.Log(col.GetType());
        renderer.enabled = false;
        col.enabled = false;
        rig.isKinematic = true;
        foreach (SpriteRenderer sr in hRenderer)
        {
            sr.enabled = false;
        }

        while (true)
        {
            if(porterOverride)
               break;
            pTransform.position = Vector3.Lerp(pTransform.position,
                 WarpToPortal.transform.position,
                 speed * Time.deltaTime);
            if (pTransform.position == WarpToPortal.position 
                || (Mathf.Abs(pTransform.position.x - WarpToPortal.position.x) < 1f 
                && Mathf.Abs(pTransform.position.y - WarpToPortal.position.y) < 1))
            {
                break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
       if(!porterOverride){
           Instantiate(Player.spawningEffect, pTransform.position, Quaternion.identity);
            col.enabled = true;
            rig.isKinematic = false;
            yield return new WaitForSeconds(0.1f);
            if (!porterOverride) renderer.enabled = true;
            foreach (SpriteRenderer sr in hRenderer)
            {
                sr.enabled = true;
            }

        }
               
            
        
     

        
        

       
            
    }

    private IEnumerator TeleportPlayerToNext(Transform pTransform)
    {
        if(pTransform.GetComponent<Player>().currentHat)
            pTransform.GetComponent<Player>().DetachFromPlayer();
        Vector3 endPos = Level_Manager.Instance.spawnPosition.position;
        Instantiate(Player.spawningEffect, pTransform.position, Quaternion.identity);
        porterOverride = true;
        SpriteRenderer renderer = pTransform.gameObject.GetComponent<SpriteRenderer>();
        Collider2D col = pTransform.gameObject.GetComponent<BoxCollider2D>();
        Rigidbody2D rig = pTransform.gameObject.GetComponent<Rigidbody2D>();

        SpriteRenderer[] hRenderer = pTransform.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sr in hRenderer)
        {
            sr.enabled = false;
        }

        renderer.enabled = false;
        col.enabled = false;
        rig.isKinematic = true;
        while (true)
        {
            pTransform.position = Vector3.Lerp(pTransform.position,
                 endPos,
                 speed * Time.deltaTime);
            if (pTransform.position == endPos)
            {
                Debug.Log("Reached");
                break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }

            Instantiate(Player.spawningEffect, pTransform.position, Quaternion.identity);
            col.enabled = true;
            rig.isKinematic = false;
            yield return new WaitForSeconds(0.3f);
            foreach (SpriteRenderer sr in hRenderer)
            {
                sr.enabled = true;
            }
            renderer.enabled = true;
            porterOverride = false;
     }

    
}
