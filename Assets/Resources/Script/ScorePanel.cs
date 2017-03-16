using UnityEngine;
using System.Collections;

public class ScorePanel : MonoBehaviour {
    private int frame = 0;
    private const int MOVE_TIME = 40;
    private const float MOVE_SPEED = 0.1f;
    private const int SCORE_ADD_INTERVAL = 5;


    private int score;
    private int bestScore;
    private int scoreTimer = 0;
    private int scoreNow = 0;
    private bool killScoreTimer = false;
    private ArrayList scoreObjects = new ArrayList();
    private ArrayList bestScoreObjects = new ArrayList();

	// Use this for initialization
	void Start () {
        transform.Translate(new Vector3(0, -MOVE_TIME * MOVE_SPEED, 0));
        score = GameObject.Find("GameController").GetComponent<GameController>().score;
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
	}
	
	// Update is called once per frame
	void Update () {
        frame++;
        if (frame <= MOVE_TIME) {
            transform.Translate(0, MOVE_SPEED, 0);
        }
        else {
            if (killScoreTimer == false) {
                scoreTimer++;
                if (scoreTimer % SCORE_ADD_INTERVAL == 0) {
                    scoreTimer = 0;
                    if (scoreNow < score) {
                        scoreNow++;
                    }
                    else {
                        if (score > bestScore) {
                            bestScore = score;
                            PlayerPrefs.SetInt("BestScore", score);
                            killScoreTimer = true;
                            ShowTextNew();     
                        }
                        ShowMedal();
                    }
                }
                ShowScore();
            }  
        }
	}

    public void ShowScore() {
        ArrayList scores = new ArrayList();
        ArrayList bestScores = new ArrayList();

        //draw score
        int temp = scoreNow;
        if (temp == 0) {
            scores.Add(0);
        }
        else {
            while (temp > 0) {
                scores.Add(temp % 10);
                temp /= 10;
            }
        }

        foreach (GameObject obj in scoreObjects) {
            DestroyObject(obj);
        }
        scoreObjects.Clear();

        for (int i = 0; i < scores.Count; i++) {
            GameObject prefab = Resources.Load("Prefab/PanelScore") as GameObject;
            GameObject obj = Instantiate(prefab);
            obj.GetComponent<PanelScore>().SetNumber((int)scores[i]);
            Vector3 position = new Vector3(2.27f - 0.16f * i, 2.73f, -2);
            obj.transform.position = position;
            scoreObjects.Add(obj);
        }

        //draw best score
        temp = bestScore;
        if (temp == 0) {
            bestScores.Add(0);
        }
        else {
            while (temp > 0) {
                bestScores.Add(temp % 10);
                temp /= 10;
            }
        }

        foreach (GameObject obj in bestScoreObjects) {
            DestroyObject(obj);
        }
        bestScoreObjects.Clear();

        for (int i = 0; i < bestScores.Count; i++) {
            GameObject prefab = Resources.Load("Prefab/PanelScore") as GameObject;
            GameObject obj = Instantiate(prefab);
            obj.GetComponent<PanelScore>().SetNumber((int)bestScores[i]);
            Vector3 position = new Vector3(2.27f - 0.16f * i, 2.31f, -2);
            obj.transform.position = position;
            bestScoreObjects.Add(obj);
        }
    }

    public void ShowTextNew() {
        int bits = 0,temp = bestScore;
        if(temp == 0){
            bits = 1;
        }
        else{
             while(temp > 0){
                 temp /= 10;
                 bits++;
             }
        }

        GameObject textNew = Instantiate(Resources.Load("Prefab/TextNew") as GameObject);
        Vector3 position = new Vector3(2.16f - bits * 0.16f, 2.31f, -2);
        textNew.transform.position = position;
    }

    public void ShowMedal() {
        if (score < 10) {
            return;
        }
        GameObject medal = Instantiate(Resources.Load("Prefab/Medal") as GameObject);
        if (score < 20) {
            medal.GetComponent<Medal>().SetMedal(Medal.BLONDE);
        }
        else if (score < 30) {
            medal.GetComponent<Medal>().SetMedal(Medal.SILVER);
        }
        else if (score < 40) {
            medal.GetComponent<Medal>().SetMedal(Medal.GOLD);
        }
        else {
            medal.GetComponent<Medal>().SetMedal(Medal.DIAMOND);
        }
    }
}
