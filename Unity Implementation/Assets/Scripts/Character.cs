using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public Vector2 size;
    public Vector2 position;
    private float speed;

    public Texture2D sprite;

	// Use this for initialization
	void Start () {
        sprite = Resources.Load<Texture2D>("sprite") as Texture2D;
        position = transform.position;
        size = new Vector2(sprite.width, sprite.height);
        speed = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
        position.x = 0;

        transform.Translate(position.x + speed * Time.deltaTime, position.y * Time.deltaTime, 0);
	}
}
