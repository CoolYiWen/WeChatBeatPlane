using UnityEngine;
using System.Collections;

public class GameStateButton : SingletonUnity<GameStateButton> {

    public Sprite[] sp;//按钮状态精灵
    public SpriteRenderer spRender;//精灵渲染组件

    void OnMouseUpAsButton()
    {
        if (GameMananger.Instance.gameState == GameState.Running)
        {
            setToPause();
            GameMananger.Instance.ChangeGameState();
        }
        else
        {
            setToContinue();
            GameMananger.Instance.ChangeGameState();
        }
    }

    void Awake()
    {
        spRender = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Sets to pause sprite.
    /// </summary>
    public void setToPause()
    {
        spRender.sprite = sp[1];
    }

    /// <summary>
    /// Sets to continue sprite.
    /// </summary>
    public void setToContinue()
    {
        spRender.sprite = sp[0];
    }

}
