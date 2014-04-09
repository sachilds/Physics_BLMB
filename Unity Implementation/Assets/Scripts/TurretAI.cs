using UnityEngine;
using System.Collections;

public class TurretAI : MonoBehaviour {
	public GameObject bullet;
	public Transform spawnPt;
    public float fireRate;
	private bool isFiring;
	private bool started;
	// Use this for initialization
	void Start () {
		isFiring = false;
		started = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isFiring )
		{
			isFiring = true;
			StartCoroutine("FireDelay");
		}

	
	}
	IEnumerator FireDelay()
	{
		//instantiate bullet
		Instantiate(bullet,spawnPt.position,spawnPt.rotation);
		yield return new WaitForSeconds(fireRate);
		isFiring = false;
	}
}
