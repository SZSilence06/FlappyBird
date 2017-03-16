using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {
    public static int fadeInTime = 30;
    private int frame = 0;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        frame++;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Color color = renderer.color;
        color.a -= 1.0f / fadeInTime;
        renderer.color = color;
        if (frame > fadeInTime){
            DestroyObject(this);
        }
	}
}
