using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BombManager : SingletonUnity<BombManager> {
    public GameObject boomIcon;//炸弹UI
    public Text boomNumber;//数量UI

    public int count = 0;//数量

    void Start () 
    {
        Instance.run();
        //初始化炸弹数量，不显示UI
        boomIcon.SetActive(false);
        boomNumber.gameObject.SetActive(false);
	}
    void run(){
    }
    /// <summary>
    /// 增加一个炸弹。
    /// </summary>
    public void AddBomb()
    {
        count++;
        showBombNumber(count);
    }

    /// <summary>
    /// 使用一个炸弹。
    /// </summary>
    public void UseBomb()
    {
        if (count > 0)   //要求炸弹数大于0
        {
            count--;

            if (count <= 0)  //如果小于等于0
            {
                boomIcon.SetActive(false);  //则不显示
                boomNumber.gameObject.SetActive(false);
            }
            else
            {
                showBombNumber(count);
            }
        }
        
    }

    /// <summary>
    /// Shows the bomb number.
    /// </summary>
    /// <param name="count">Count.</param>
    private void showBombNumber(int count)
    {
        boomIcon.SetActive(true);
        boomNumber.gameObject.SetActive(true);
        boomNumber.text = "X" + count;  //显示炸弹数
    }


    void Update () 
    {
        //检测是否使用炸弹
        if (Input.GetKeyDown(KeyCode.Space) && count > 0)
        {
            this.UseBomb();
        }
    }
   
}
