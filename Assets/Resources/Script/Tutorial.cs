using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
    private bool disappear = false;
    private const int FADEOUT_TIME = 15;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (disappear) {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            Color color = renderer.color;
            color.a -= 1.0f / FADEOUT_TIME;
            if (color.a < 0)
            {
                color.a = 0;
            }
            renderer.color = color;
        }
	}

    public void Disappear()  {
        disappear = true;
    }
}
