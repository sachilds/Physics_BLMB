using UnityEngine;
using System.Collections;

public class PlaySoundEffect : MonoBehaviour {
	public AudioClip soundFX;
	public string collisionTag;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.tag == collisionTag)
		{
			Debug.Log("play sound effect");
			audio.PlayOneShot(soundFX);
		}
	}
}
