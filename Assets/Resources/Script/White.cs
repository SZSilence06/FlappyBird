using UnityEngine;
using System.Collections;

public class White : MonoBehaviour {
    private int frame = 0;
    private const int LIFE = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        frame++;
        Color color = GetComponent<SpriteRenderer>().color;
        if (frame <= LIFE / 2) {
            color.a += 2.0f / LIFE;
        }
        else if (frame <= LIFE) {
            color.a -= 2.0f / LIFE;
        }
        else {
            DestroyObject(this);
        }
        GetComponent<SpriteRenderer>().color = color;
	}
}
