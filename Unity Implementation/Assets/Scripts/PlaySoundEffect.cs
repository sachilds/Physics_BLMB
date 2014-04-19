using UnityEngine;
using System.Collections;

public class PlaySoundEffect : MonoBehaviour {
	public AudioClip dead;
	public AudioClip bounce;
	// Use this for initialization
	void Start () {
		
		Debug.Log(audio.volume +" Start SFX VOL");
		audio.volume *= Game_Manager.soundFXVol;
		Debug.Log(audio.volume+" Curr SFX Vol");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.tag == "Sugar")
		{
			Debug.Log("play dead sfx");
			audio.PlayOneShot(dead);
		}
		if(c.gameObject.tag == "JelloBlock")
		{
			Debug.Log("JelloBlock sound");
			audio.PlayOneShot(bounce);
		}
	}
}
