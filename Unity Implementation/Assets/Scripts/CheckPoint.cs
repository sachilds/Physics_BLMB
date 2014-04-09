using UnityEngine;
using System.Collections;


public class CheckPoint : MonoBehaviour {
    bool isActive = false;
    public Sprite ActiveIcon, DeactiveIcon;
    private Transform spawn;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = DeactiveIcon;
        spawn = GetComponentInChildren<Transform>();
     
    }
	public void Activate(){
        Level_Manager.Instance.resetCheckPoints();
        isActive = true;
        Level_Manager.Instance.setSpawnPos(spawn);
        GetComponent<SpriteRenderer>().sprite = ActiveIcon;
    }
    public void Deactivate(){
        isActive = false;
        GetComponent<SpriteRenderer>().sprite = DeactiveIcon;
    }

    public void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            if (!isActive)
            {
                Activate();
            }
        }
      
    }
}
