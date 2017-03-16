using UnityEngine;
using System.Collections;
using System;

public class BirdScript : MonoBehaviour {
    public const int ORANGE = 0;
    public const int BLUE = 0;
    public const int RED = 0;
    private const int ANIM_INTERVAL = 10;
    private const int CYCLE = 45;
    private const int RISE_TIME = 20;
    private const int FALL_TIME = 40;
    private const float MAX_ROTATE_ANGLE = 30.0f;
    private const float MAX_RISE_SPEED = 0.05f;
    private const float MAX_FALL_SPEED = 0.05f;

    private const float yStep = 0.2f / CYCLE;

    Sprite[] anim = new Sprite[3]; 

    private int color;
    private int frame = 0;       //controls wings
    private int frame2 = 0;
    private float rotAngle = 0;      

    enum State  {
        NORMAL,
        RISING,
        FALLING,
        DYING,
        DEAD
    }

    State state = State.NORMAL;

	// Use this for initialization
	void Start () {
        int rand = UnityEngine.Random.Range(0, 3);
        color = rand;
        string resFileName = "bird" + color + "_";
        for (int i = 0; i < 3; i++){
            Texture2D texture = Resources.Load<Texture2D>(resFileName + i);
            anim[i] = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (state == State.DEAD) {
            return;
        }

        frame++;
        if (frame >= 3 * ANIM_INTERVAL) {
            frame = 0;
        }

        SpriteRenderer render = GetComponent<SpriteRenderer>();
        render.sprite = anim[(frame / ANIM_INTERVAL) % 3];

        frame2++;
        if (state == State.NORMAL) {
            NormalUpdate();
        }
        else {
            FlyingUpdate();
        }

        float rad = (float)(rotAngle / 180.0f * Math.PI) / 2;
        this.transform.rotation = new Quaternion(0.0f, 0.0f,
            (float)Math.Sin(rad), (float)Math.Cos(rad));
	}

    public void NormalUpdate() {
        if (frame2 >= CYCLE) {
            frame2 = 0;
        }
        Vector3 position = transform.position;
        if (frame2 < CYCLE / 4) {
            position.y = 2.56f + frame2 * yStep;
        }
        else if (frame2 < CYCLE * 3 / 4) {
            position.y = 2.56f + (CYCLE / 2 - frame2) * yStep;
        }
        else {
            position.y = 2.56f + (frame2 - CYCLE) * yStep;
        }
        transform.position = position;
    }

    public void FlyingUpdate() {
        Vector3 position = this.transform.position;
        switch (state) {
            case State.RISING:
                if (frame2 < RISE_TIME){
                    position.y += MAX_RISE_SPEED / RISE_TIME * (RISE_TIME - frame2);
                }
                else {
                    state = State.FALLING;
                    frame2 = 0;
                }
                if (rotAngle < MAX_ROTATE_ANGLE) {
                    if (frame2 < 15)       {
                        rotAngle += 8.0f;
                    }
                }
                else {
                    rotAngle = MAX_ROTATE_ANGLE;
                }
                this.transform.position = position;
                break;
            case State.FALLING:
            case State.DYING:
                if (frame2 < RISE_TIME){
                    position.y -= MAX_FALL_SPEED / FALL_TIME * frame2;
                }
                else {
                    position.y -= MAX_FALL_SPEED;
                }
                if (rotAngle > -90.0f) {
                    if (frame2 < 15)  {
                        rotAngle -= 3.0f;
                    }
                    else {
                        rotAngle -= 5.0f;
                    }
                }
                else {
                    rotAngle = -90.0f;
                }
                this.transform.position = position;
                break;
        }
        if (this.transform.position.y <= GameController.LAND_HEIHGT){
            if (state == State.DYING ) {   // touch the ground
                state = State.DEAD;
            }
            else {
                GameObject.Find("GameController").GetComponent<GameController>().Die();
            }
        }
    }

    public void StartFlying(){
        Rise();
    }

    public void Rise() {
        this.state = State.RISING;
        frame2 = 0; 
    }

    public void Die() {
        this.state = State.DYING;
        frame2 = 0;
    }

    public bool IsDead() {
        if (this.state == State.DYING || this.state == State.DEAD) {
            return true;
        }
        return false;
    }

    public Rect GetCollideRect() {
        return new Rect(transform.position.x - 0.18f, transform.position.y - 0.11f, 0.34f, 0.24f);
    }
}
