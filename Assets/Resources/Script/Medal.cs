using UnityEngine;
using System.Collections;

public class Medal : MonoBehaviour {
    static public Sprite[] sprites = new Sprite[4];
    public static int DIAMOND = 0;
    public static int GOLD = 1;
    public static int SILVER = 2;
    public static int BLONDE = 3;

	// Use this for initialization

    public static void Initialize() {
        for (int i = 0; i < 4; i++) {
            string assetName = "medals_" + i;
            Texture2D texture = Resources.Load(assetName) as Texture2D;
            sprites[i] = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetMedal(int medal) {
        GetComponent<SpriteRenderer>().sprite = sprites[medal];
    }
}
