using UnityEngine;
using System.Collections;
using FlappyBird;

public class LandScript : MonoBehaviour {
    private const int CYCLE = 24;
    private int frame = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Game.IsDead) {
            return;
        }

        frame++;
        Vector3 position = transform.position;
        position.x -= 0.48f / CYCLE;
        if (frame >= CYCLE) {
            frame = 0;
            position.x = 1.68f;
        }
        transform.position = position;
	}
}
