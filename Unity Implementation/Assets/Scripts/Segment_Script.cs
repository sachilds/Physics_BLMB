using UnityEngine;
using System.Collections;
public struct Segment
{
    public Renderer sRenderer;
    public Collider2D sCollider;
    public Transform sTransform;
    public Vector3 sBeginningPos;
  
    public float sSpeed;
}


public class Segment_Script : MonoBehaviour {
    public Transform defaultSpawn;
    private Segment[] segments;
  
    public Transform defaultEndSpawn;
	// Use this for initialization
	void Start () {
        
        Collider2D[] tempObjects = gameObject.GetComponentsInChildren<Collider2D>();
        segments = new Segment[tempObjects.Length];
        
        for (int i = 0; i < segments.Length; i++)
		{
            segments[i].sTransform = tempObjects[i].transform;
            segments[i].sBeginningPos = tempObjects[i].transform.localPosition;
           
            if (tempObjects[i].renderer != null)
            {
                segments[i].sRenderer = tempObjects[i].renderer;
                ToggleRenderer(segments[i]);
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
            //s.sCollider.enabled = !s.sCollider.enabled;
        }
    }

    public void ResetSegments()
    {
        for (int i = 0; i < segments.Length; i++)
        {
            segments[i].sTransform.localPosition = segments[i].sBeginningPos;
            if(segments[i].sRenderer)
                segments[i].sRenderer.enabled = true;
        }
    }
    public void SpawnSegment(){
        for (int i = 0; i < segments.Length; i++)
        {
            ToggleRenderer(segments[i]);            
        }
        
    }

    public void RemoveSegment() {
        for (int i = 0; i < segments.Length; i++)
        {
            ToggleRenderer(segments[i]);
        }
       
    }
  
 
	
	
}
