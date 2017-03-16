using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Random.Range(0, 2) == 0) {
            Texture2D texture = Resources.Load("bg_night") as Texture2D;
            GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
