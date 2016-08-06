using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMananger : SingletonUnity<GameMananger> {
    
    public Text scoreText;  //分数UI组件
    public int score;  //分数
    public GameState gameState = GameState.Running; //游戏状态

    //检测ESC
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (gameState == GameState.Running))
        {
            ChangeGameState();
            GameStateButton.Instance.setToPause();            
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.Pause)
        {            
            ChangeGameState();
            GameStateButton.Instance.setToContinue();
        }
    }

    /// <summary>
    /// Adds the score.Then show the score.
    /// </summary>
    /// <param name="sc">Score.</param>
    public void addScore(int sc)
    {
       this.score += sc;
       scoreText.text = "Score:" + score;
    }

    /// <summary>
    /// Changes the state of the game.
    /// </summary>
    public void ChangeGameState()
    {
        if (gameState == GameState.Running)
        {
            PauseGame();
        }
        else if(gameState == GameState.Pause)
        {
            ContinueGame();
        }
        
    }
    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;//暂停游戏时间
        gameState = GameState.Pause;
    }

    /// <summary>
    /// Continues the game.
    /// </summary>
    public void ContinueGame()
    {
        Time.timeScale = 1;
        gameState = GameState.Running;
    }

    //绘制UI
    void OnGUI()
    {
        if (gameState == GameState.Pause)
        {
            if (GUI.Button(new Rect(Screen.width * 0.5f - 50, Screen.height * 0.4f, 100, 30), "继续游戏"))
            {
                ContinueGame();
                GameStateButton.Instance.setToContinue();
            }
            if (GUI.Button(new Rect(Screen.width * 0.5f - 50, Screen.height * 0.6f, 100, 30), "退出游戏"))
            {
                Application.Quit();
            }
        }
    }

}


public enum GameState
{
    Running,
    Pause,
}