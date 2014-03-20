using UnityEngine;
using System.Collections;
public struct Segment
{
    public Renderer sRenderer;
    public Collider2D sCollider;
    public Transform sTransform;
    public Vector3 sBeginningPos;
    public Vector3 sEndPos;
    public float sSpeed;
    public bool sFinished;

}


public class Segment_Script : MonoBehaviour {
    private Segment[] segments;
  
	// Use this for initialization
	void Start () {
       Collider2D[] tempObjects = gameObject.GetComponentsInChildren<Collider2D>();
       segments = new Segment[tempObjects.Length];

        for (int i = 0; i < segments.Length; i++)
		{
            segments[i].sTransform = tempObjects[i].transform;
            
            segments[i].sBeginningPos = tempObjects[i].transform.localPosition;
            segments[i].sEndPos = new Vector2(tempObjects[i].transform.localPosition.x, tempObjects[i].transform.localPosition.y - 20);
            segments[i].sSpeed = Random.Range(20 , 40);
            
            segments[i].sTransform.localPosition = segments[i].sEndPos;
            
            segments[i].sFinished = false;

            if (tempObjects[i].renderer != null)
            {
                segments[i].sRenderer = tempObjects[i].renderer;
                ToggleRenderer(segments[i]);
            }
            if (!tempObjects[i].collider2D != null)
            {
                segments[i].sCollider = tempObjects[i].collider2D;
                segments[i].sCollider.enabled = false;
            }
            
		}
        


        
    }

    private void ToggleRenderer(Segment s)
    {
        if (s.sRenderer != null)
        {
            s.sRenderer.enabled = !s.sRenderer.enabled;
        }
    }
    private void ToggleCollider(Segment s)
    {
        if (s.sCollider != null)
        {
            s.sCollider.enabled = !s.sCollider.enabled;
        }
    }

    public void ResetSegments()
    {
        for (int i = 0; i < segments.Length; i++)
        {
            segments[i].sTransform.localPosition = segments[i].sBeginningPos;
            segments[i].sRenderer.enabled = true;
            segments[i].sCollider.enabled = true;
            segments[i].sFinished = false;
        }
    }
    public void StartRising(){
        for (int i = 0; i < segments.Length; i++)
        {
            ToggleRenderer(segments[i]);
            segments[i].sFinished = false;
        }
        StartCoroutine("RaisingCoRoutine");
    }

    public void StartDropping() {
        for (int i = 0; i < segments.Length; i++)
        {
            ToggleRenderer(segments[i]);
            segments[i].sFinished = false;
        }
        StartCoroutine("DroppingCoRoutine");
    }
    public IEnumerator DroppingCoRoutine() {
        int count = 0;
        while (count < segments.Length)
        {
            for (int i = 0; i < segments.Length; i++)
            {
                if (!segments[i].sFinished)
                {
                    segments[i].sTransform.localPosition = Vector3.Lerp(segments[i].sTransform.localPosition,
                    segments[i].sEndPos,
                    segments[i].sSpeed * Time.deltaTime);
                }
                if (!segments[i].sFinished && segments[i].sTransform.localPosition == segments[i].sEndPos)
                {
                    Debug.Log(segments[i].sTransform.localPosition.ToString());
                    segments[i].sFinished = true;
                    ++count;
                }

                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        
    }
    public IEnumerator RaisingCoRoutine()
    {

        int count = 0;
        while (count < segments.Length)
        {
            for (int i = 0; i < segments.Length; i++)
            {
                if (!segments[i].sFinished)
                {
                    segments[i].sTransform.localPosition = Vector3.Lerp(segments[i].sTransform.localPosition,
                    segments[i].sBeginningPos,
                    segments[i].sSpeed * Time.deltaTime);
                }
                if (!segments[i].sFinished && segments[i].sTransform.localPosition == segments[i].sBeginningPos)
                {
                    Debug.Log(segments[i].sTransform.localPosition.ToString());
                    segments[i].sFinished = true;
                    segments[i].sCollider.enabled = true;
                    ++count;
                }
               
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        TeleportScript.SpawnPlayer();
       

     

    }
 
	
	
}
