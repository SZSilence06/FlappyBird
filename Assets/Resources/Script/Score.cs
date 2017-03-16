using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
    static public Sprite[] sprites = new Sprite[10];
    private int number;
    private bool disappear = false;
    private const int FADEOUT_TIME = 15;

    static public void Initialize() {
        for (int i = 0; i < 10; i++) {
            string assetName = "font_0" + (48 + i);
            Texture2D texture = Resources.Load(assetName) as Texture2D;
            sprites[i] = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if (disappear) {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            Color color = renderer.color;
            color.a -= 1.0f / FADEOUT_TIME;
            if (color.a < 0) {
                Destroy(this);
            }
            renderer.color = color;
        }
	}

    public void SetNumber(int number) {
        this.number = number;
        GetComponent<SpriteRenderer>().sprite = sprites[number];
    }

    public void Disappear() {
        disappear = true;
    }
}
