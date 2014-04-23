using UnityEngine;
using System.Collections;

public class PlaySoundEffect : MonoBehaviour {
	public AudioClip dead;
	public AudioClip bounce;
	// Use this for initialization
	void Start () {
		audio.volume *= Game_Manager.soundFXVol;
	}
	
	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.tag == "Sugar")
		{
			audio.PlayOneShot(dead);
		}
		if(c.gameObject.tag == "JelloBlock")
		{
			audio.PlayOneShot(bounce);
		}
	}
}
