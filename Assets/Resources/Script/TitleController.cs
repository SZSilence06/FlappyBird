using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using FlappyBird;

public class TitleController : MonoBehaviour {
    enum State{
        NORMAL,
        FADEOUT
    }

    State state = State.NORMAL;
    int fadeOutTimer = 0;

    void Awake()  {
        Game game = Game.GetInstance();
    }
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (state == State.FADEOUT)  {
            fadeOutTimer++;
            if (fadeOutTimer >= FadeOut.fadeOutTime) {
                SceneManager.LoadScene("game");
            }
            return;
        }

        if (Game.HasInput()) {
            Vector2 mousePosition = Game.GetInputPosition();
            mousePosition = Geometry.PixelStandardize(mousePosition);
            GameObject buttonPlay = GameObject.Find("ButtonPlay");
            GameObject buttonQuit = GameObject.Find("ButtonQuit");
            if (InObject(buttonPlay, mousePosition)) {
                state = State.FADEOUT;
                GameObject fadeOut = Resources.Load("Prefab/FadeOut") as GameObject;
                Instantiate(fadeOut);
            }
            else if (InObject(buttonQuit, mousePosition))  {
                Application.Quit();
            }
        }
	}

    public bool InObject(GameObject obj, Vector3 position)  {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        int width = renderer.sprite.texture.width;
        int height = renderer.sprite.texture.height;
        float x = obj.transform.position.x * 100;
        float y = obj.transform.position.y * 100;
        if (position.x <= x + width/2 && position.x >= x - width/2
            && position.y <= y + height/2 && position.y >= y -height/2){
                return true;
        }            
        return false;
    }
}
