using UnityEngine;
using System.Collections;

public class TextGameOver : MonoBehaviour {
    private int frame = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        frame++;
        if (frame < 12) {
            transform.Translate(new Vector3(0.0f, -0.01f, 0.0f));
        }
        else if (frame < 20) {
            transform.Translate(new Vector3(0.0f, 0.01f, 0.0f));
        }
	}
}
