using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : SingletonUnity<GameOver> {
    public Text nowScoreText;
    public Text highScoreText;

    void Awake()
    {
        Instance.Update();
        this.gameObject.SetActive(false);
        nowScoreText.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);
    }
	// Use this for initialization
	void Start () {
	
	}
		
	void Update () {

	
	}
    public void showScore(int nowScore)
    {     
        int historyScore = PlayerPrefs.GetInt("historyHighScore", 0);
        if (nowScore > historyScore)
        {
            PlayerPrefs.SetInt("historyHighScore", nowScore);
           // highScoreText.text = historyScore + "";
        }
        highScoreText.text = historyScore + "";
        this.nowScoreText.text = nowScore + "";

        this.gameObject.SetActive(true);
        //print("now" + nowScore + " " + "High" + historyScore); 

        nowScoreText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
        GameMananger.Instance.scoreText.gameObject.SetActive(false);
        BombManager.Instance.boomIcon.gameObject.SetActive(false);
        BombManager.Instance.boomNumber.gameObject.SetActive(false);
        GameStateButton.Instance.gameObject.SetActive(false);
    }
}
