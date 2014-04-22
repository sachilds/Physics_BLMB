using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

    private int maxSlope = 60;
    public bool isSwitched;

    public Sprite onSprite;
    public Sprite offSprite;

    private SpriteRenderer spriteRenderer;

    private Vector2 switchedPos;
    public float offset;

	// Use this for initialization
	void Start () {
        isSwitched = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = offSprite;
	}

    void OnCollisionEnter2D(Collision2D c) {
        if (c.gameObject.tag == "Player") { 
            SwitchIt();
        }
    }

    private void SwitchIt() {
        isSwitched = !isSwitched;
        if (isSwitched)
        {
            spriteRenderer.sprite = onSprite;
        }
        else
        {
            spriteRenderer.sprite = offSprite;
        }
    }
}
