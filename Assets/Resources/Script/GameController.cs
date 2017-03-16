using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using FlappyBird;

public class GameController : MonoBehaviour {
    public enum State{
        PREPARE,
        START,
        DEAD,
        FADEOUT
    };

    public State state = State.PREPARE;

    public int score = 0;
    private int startTimer = 0;
    private int fadeOutTimer = 0;
    private int buttonDisplayTimer = 0;

    public static float LAND_HEIHGT = 1.12f;
    private const int PIPE_INTERVAL = 120;
    private const int MIN_PIPE_HEIGHT = 24;
    private int MAX_PIPE_HEIGHT;
    private const int BUTTON_DISPLAY_TIME = 50;


    public ArrayList upPipes = new ArrayList();
    public ArrayList downPipes = new ArrayList();
    public ArrayList scoreObjects = new ArrayList();

	// Use this for initialization
	void Start () {
        GameObject fadeIn = Resources.Load("Prefab/FadeIn") as GameObject;
        Instantiate(fadeIn);
        UpdateScore();
        Game.IsDead = false;

        MAX_PIPE_HEIGHT = 512 - (int)(LAND_HEIHGT * 100) - MIN_PIPE_HEIGHT - PIPE_INTERVAL;
        GameObject.Find("Land").transform.Translate(0.0f, LAND_HEIHGT - 1.12f, 0.0f);
        Debug.Log("Successfullly load GameController");
    }
	
	// Update is called once per frame
	void Update () {
        switch (state) {
            case State.START:
                startTimer++;
                if (startTimer % 80 == 0) {
                    GeneratePipes();
                    startTimer = 0;
                }
                CalcScore();
                if (Intersect()) {
                    Die();
                }
                break;
            case State.FADEOUT:
                fadeOutTimer++;
                if (fadeOutTimer >= FadeOut.fadeOutTime) {
                    Debug.Log("Prepare to load scene");
                    SceneManager.LoadScene("game");
                    Debug.Log("Successfully load scene");
                }
                break;
            case State.DEAD:
                buttonDisplayTimer++;
                if (buttonDisplayTimer == BUTTON_DISPLAY_TIME) {
                    Instantiate(Resources.Load("Prefab/ButtonPlay") as GameObject);
                    Instantiate(Resources.Load("Prefab/ButtonQuit") as GameObject);
                }
                break;
        }
	}

    public void StartFlying() {
        state = State.START;
        GameObject.Find("Tutorial").SendMessage("Disappear");
        GameObject.Find("TextReady").SendMessage("Disappear");
        GameObject.Find("Bird").SendMessage("StartFlying");
    }

    public void GeneratePipes() {
        GameObject pipeUpPrefab = Resources.Load("Prefab/PipeUp") as GameObject;
        GameObject pipeDownPrefab = Resources.Load("Prefab/PipeDown") as GameObject;
        GameObject pipeUp = GameObject.Instantiate(pipeUpPrefab);
        GameObject pipeDown = GameObject.Instantiate(pipeDownPrefab);

        Texture2D upTexture = pipeUp.GetComponent<SpriteRenderer>().sprite.texture;
        Texture2D downTexture = pipeDown.GetComponent<SpriteRenderer>().sprite.texture;
        int pipeUpHeight = UnityEngine.Random.Range(MIN_PIPE_HEIGHT, MAX_PIPE_HEIGHT);
        int pipeDownHeight = 512 - (int)(LAND_HEIHGT * 100) - PIPE_INTERVAL - pipeUpHeight;

        Rect upRect = new Rect(0, upTexture.height - pipeUpHeight, upTexture.width, pipeUpHeight);
        Vector2 upPivot = new Vector2(0.5f, 0.5f);
        Sprite upSprite = Sprite.Create(upTexture, upRect, upPivot);
        pipeUp.GetComponent<SpriteRenderer>().sprite = upSprite;

        Rect downRect = new Rect(0, 0, downTexture.width, pipeDownHeight);
        Vector2 downPivot = new Vector2(0.5f, 0.5f);
        Sprite downSprite = Sprite.Create(downTexture, downRect, downPivot);
        pipeDown.GetComponent<SpriteRenderer>().sprite = downSprite;

        Vector3 downPos = new Vector3(3.2f, 5.12f - (float)pipeDownHeight / 2 / 100, 20);
        Vector3 upPos = new Vector3(3.2f, LAND_HEIHGT + (float)pipeUpHeight / 2 / 100, 20);
        pipeUp.transform.position = upPos;
        pipeDown.transform.position = downPos;

        upPipes.Add(pipeUp);
        downPipes.Add(pipeDown);
    }

    public void CalcScore()  {
        GameObject bird = GameObject.Find("Bird");
        foreach (GameObject upPipe in upPipes)   {
            if (Math.Abs(bird.transform.position.x - upPipe.transform.position.x) < 1e-4)  {
                score++;
                UpdateScore();
            }
        }
    }

    public void UpdateScore() {
        ArrayList scores = new ArrayList();
        int temp = score;
        if (temp == 0) {
            scores.Add(0);
        }
        else {
            while (temp > 0) {
                scores.Add(temp % 10);
                temp /= 10;
            }
        }

        DeleteScoreObjects();

        int length = 0;
        for (int i = 0; i < scores.Count; i++) {
            GameObject prefab = Resources.Load("Prefab/Score") as GameObject;
            GameObject obj = Instantiate(prefab);
            obj.GetComponent<Score>().SetNumber((int)scores[i]);
            length += obj.GetComponent<SpriteRenderer>().sprite.texture.width;
            scoreObjects.Add(obj);
        }

        float x = 1.44f + length / 2 / 100.0f;
        foreach (GameObject obj in scoreObjects) {
            Vector3 position = new Vector3(x - obj.GetComponent<SpriteRenderer>().sprite.texture.width / 2 / 100.0f,
                4.3f, 20.0f);
            obj.transform.position = position;
            x -= obj.GetComponent<SpriteRenderer>().sprite.texture.width / 100.0f;
        }
    }

    public bool Intersect() {
        Rect rcBird = GameObject.Find("Bird").GetComponent<BirdScript>().GetCollideRect();
        foreach (GameObject pipe in upPipes) {
            Rect rcPipe = Geometry.GetObjectRect(pipe);
            if (Geometry.RectIntersect(rcBird, rcPipe)) {
                return true;
            }
        }
        foreach (GameObject pipe in downPipes) {
            Rect rcPipe = Geometry.GetObjectRect(pipe);
            if (Geometry.RectIntersect(rcBird, rcPipe)) {
                return true;
            }
        }
        return false;
    }

    public void Die() {
        GameObject.Find("Bird").SendMessage("Die");
        state = State.DEAD;
        Instantiate(Resources.Load("Prefab/White") as GameObject);
        Game.IsDead = true;

        ShowResult();
    }

    public void DeleteScoreObjects() {
        foreach (GameObject obj in scoreObjects) {
            DestroyObject(obj);
        }
        scoreObjects.Clear();
    }

    public void ShowResult() {
        foreach (GameObject obj in scoreObjects) {
            obj.GetComponent<Score>().Disappear();
        }
        scoreObjects.Clear();

        Instantiate(Resources.Load("Prefab/TextGameOver") as GameObject);
        Instantiate(Resources.Load("Prefab/ScorePanel") as GameObject);
        buttonDisplayTimer = 0;
    }
}


