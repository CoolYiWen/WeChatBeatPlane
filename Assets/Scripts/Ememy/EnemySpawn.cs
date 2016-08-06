using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
    
    public GameObject smallEnemy;//小飞机
    public float smallEnemyRate = 0.5f;//小飞机生成速率
    public GameObject middleEnemy;//中飞机
    public float middleEnemyRate = 3;
    public GameObject bigEnemy;//大飞机
    public float bigEnemyRate = 10;
    public GameObject powerUp;//奖励物品
    public float powerUpRate = 8;
    public GameObject boom;//炸弹
    public float boomRate = 30;

    void Start () {
        InvokeRepeating("creatSmallEnemy", 1, smallEnemyRate);
        //重复调用生成小飞机
        InvokeRepeating("creatMiddleEnemy", 5, middleEnemyRate);
        //重复调用生成中飞机
        InvokeRepeating("creatBigEnemy", 10, bigEnemyRate);
        //重复调用生成大飞机
        InvokeRepeating("creatPowerUp", 10, powerUpRate);
        //重复调用生成奖励物品
        InvokeRepeating("creaBoom", 18, boomRate);
        //重复调用生成全屏炸弹

    }

    void creatSmallEnemy()
    {
         creatEnemy(-2.2f, 2.2f, smallEnemy); 
    }

    void creatMiddleEnemy()
    {
        creatEnemy(-2.1f, 2.1f, middleEnemy); 
    }
    void creatBigEnemy()
    {
        creatEnemy(-1.6f, 1.6f, bigEnemy);
    }
    void creatPowerUp()
    {
        creatEnemy(-2.1f, 2.1f, powerUp);
    }
    void creaBoom()
    {
        creatEnemy(-2.1f, 2.1f, boom);
    }

    void creatEnemy(float xMin,float xMax,GameObject plan)
    {
        float x = Random.Range(xMin, xMax); //随机生成位置
        GameObject.Instantiate(plan, new Vector3(x, transform.position.y, 0), Quaternion.identity);
    }
   
    
}
