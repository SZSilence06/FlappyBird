using UnityEngine;
using System.Collections;
using FlappyBird;

public class GameInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GameController controller = GameObject.Find("GameController").GetComponent<GameController>();
        if (Game.HasInput()) {
            Vector2 mousePosition = Game.GetInputPosition();
            mousePosition = Geometry.PixelStandardize(mousePosition);
            switch (controller.state)   {
                case GameController.State.PREPARE:
                    controller.StartFlying();
                    break;
                case GameController.State.START:
                    GameObject.Find("Bird").SendMessage("Rise");
                    break;
                case GameController.State.DEAD:
                    GameObject buttonPlay = GameObject.FindGameObjectWithTag("ButtonPlay");
                    GameObject buttonQuit = GameObject.FindGameObjectWithTag("ButtonQuit");
                    if(buttonPlay == null || buttonQuit == null){
                        break;
                    }
                    if (InObject(buttonPlay, mousePosition)) {
                        controller.state = GameController.State.FADEOUT;
                        GameObject fadeOut = Resources.Load("Prefab/FadeOut") as GameObject;
                        Instantiate(fadeOut);
                    }
                    else if (InObject(buttonQuit, mousePosition))  {
                        Application.Quit();
                    }
                    break;
                case GameController.State.FADEOUT:
                    break;
                default:
                    break;
            }
        }
	}

    public bool InObject(GameObject obj, Vector3 position) {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        int width = renderer.sprite.texture.width;
        int height = renderer.sprite.texture.height;
        float x = obj.transform.position.x * 100;
        float y = obj.transform.position.y * 100;
        if (position.x <= x + width / 2 && position.x >= x - width / 2
            && position.y <= y + height / 2 && position.y >= y - height / 2) {
            return true;
        }
        return false;
    }
}
