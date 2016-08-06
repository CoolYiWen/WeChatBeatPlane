using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public int life;//生命值
    public int score;//积分
    public float speed;//速度 
    public EnemyType planeType = EnemyType.smallEnemy;  //敌机类型
    public float minYDistance = -5.4f;
    [HideInInspector]
    public bool isAddedScore = false;
    private bool isHit = false;//是否被击中

    private Animator anim;

    //获取组件
    void Awake () 
    {
        anim = GetComponent<Animator>();
	}

    void Update()
    {
        if (transform.position.y <= minYDistance)   //是否超出屏幕最下边
        {
            Destroy(this.gameObject);
        }

        checkLife();  //每帧检查生命

        if (Input.GetKeyDown(KeyCode.Space) && BombManager.Instance.count > 0)
        {      
            this.life = -1;        
            //如果按下了空格键，并且炸弹数目大于0，则把敌人的生命设为-1，全部执行爆炸动画    
        }
    }
	
	void FixedUpdate () 
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
	}

    public void Behit()   //被击中
    {
        this.life--;
        this.isHit = true; 
    }

    IEnumerator Delay(int state)   //延时动画方法，可用Anmator里的播放完触发函数取代的
    {
        switch (state)
        {
            case 1:
                anim.SetBool("Dead", true);
                yield return new WaitForSeconds(0.25f);//延时以播放爆炸动画
                Destroy(this.gameObject);
                break;

            case 2:
                anim.SetBool("Hited", isHit);
                yield return new WaitForSeconds(0.2f);//延时以播放爆炸动画
                this.isHit = false;
                break;

            case 3:
                anim.SetBool("Dead", true);   
                yield return new WaitForSeconds(0.42f);//延时更长的时间以播放爆炸动画 
                Destroy(this.gameObject);
                break;

            default:
                break;

        }


    }
    
    /// <summary>
    /// Checks the life.
    /// </summary>
    private void checkLife()
    {
        if (planeType == EnemyType.smallEnemy) //如果是小飞机
        {
            if (life <= 0)
            {
                if (!isAddedScore)
                {
                    GameMananger.Instance.addScore(score); //加分
                    isAddedScore = true;
                }
                StartCoroutine(Delay(1));  //调用延时1的方法
            }
        }
        if(planeType == EnemyType.middleEnemy)  //中型飞机
        {
            if (life <= 0)
            {
                if (!isAddedScore)
                {
                    GameMananger.Instance.addScore(score); //加分
                    isAddedScore = true;
                }
                StartCoroutine(Delay(1));
            }
            else  //如果生命不为0，则播放受伤动画
            {
                StartCoroutine(Delay(2));  
            }
        }
        if (planeType == EnemyType.bigEnemy)  //大飞机
        {
            if (life <= 0)
            {
                if (!isAddedScore)
                {
                    GameMananger.Instance.addScore(score); //加分
                    isAddedScore = true;
                }
                StartCoroutine(Delay(3));
            }
            else  //播放受伤动画
            {                
                StartCoroutine(Delay(2));           
            }
        }
       
    }

}


public enum EnemyType  //敌机的类型
{
    smallEnemy,
    middleEnemy,
    bigEnemy,
    support,
    boom
}