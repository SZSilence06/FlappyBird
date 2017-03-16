using UnityEngine;
using System.Collections;
using FlappyBird;

public class Pipe : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Game.IsDead) {
            return;
        }

        Vector3 position = this.transform.position;
        position.x -= 0.02f;
        this.transform.position = position;

        if (this.transform.position.x < -0.3f) {
            GameObject.Find("GameController").GetComponent<GameController>().upPipes.Remove(this.gameObject);
            GameObject.Find("GameController").GetComponent<GameController>().downPipes.Remove(this.gameObject);
            GameObject.Destroy(this.gameObject);
        }
	}
}
