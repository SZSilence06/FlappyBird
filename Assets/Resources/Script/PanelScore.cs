using UnityEngine;
using System.Collections;

public class PanelScore : MonoBehaviour {
    static public Sprite[] sprites = new Sprite[10];
    private int number;

    static public void Initialize() {
        for (int i = 0; i < 10; i++) {
            string assetName = "number_score_0" + i;
            Texture2D texture = Resources.Load(assetName) as Texture2D;
            sprites[i] = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetNumber(int number) {
        this.number = number;
        GetComponent<SpriteRenderer>().sprite = sprites[number];
    }
}
