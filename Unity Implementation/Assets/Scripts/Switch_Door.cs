using UnityEngine;
using System.Collections;

public class Switch_Door : MonoBehaviour {

    public Switch switchController;
    public GameObject go;

	// Use this for initialization
	void Start () {
        if (!switchController)
            Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        if (switchController.isSwitched) {
            go.SetActive(false);
        }
        else {
            go.SetActive(true);
        }
	}
}
