using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveTime = 1.0f;
    public Sprite[] sprites; //存放正常精灵图片
    public Sprite[] deadSprite; //存放精灵图片

    public float timer;        //计时器
    public float doubleGunTime = 10f;  //双枪存在的时间
    private float resetWeaponTime;   //复位时间

    public Shoot gunCenter, gunLeft, gunRight; //三个子弹发射口的位置

	private SpriteRenderer sp;
    private Animator anim;

	private bool isMouseDown = false;
	private Vector3 lastMousePosition;
    private int weapon = 1;   //当前武器的类型

    private Vector3 screenArea;
    private float screenXMin , screenXMax;  //定义屏幕X轴范围
    private float screenYMin , screenYMax;  //定义屏幕Y轴范围

	void Awake()
	{
        sp = GetComponent<SpriteRenderer>();  //获取精灵组件
        anim = GetComponent<Animator>();
	}

    void Start () 
	{
        //把屏幕坐标转换成世界坐标
        screenArea = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0));
        screenXMin = -screenArea.x;
        screenXMax = screenArea.x;
        screenYMin = -screenArea.y;
        screenYMax = screenArea.y;

        resetWeaponTime = doubleGunTime;  //设置双枪复位时间
        doubleGunTime = 0;          //双枪存在的时间0为初值
        gunCenter.OpenFire();    //发射子弹
    
    }
	

	void Update () {
        timer += Time.deltaTime;//精灵变换计时器累加
        if (timer < moveTime)
        {
            sp.sprite = sprites[0];
        }
        else if (timer < 2 * moveTime)
        {
            sp.sprite = sprites[1];
        }
        else
        {
            timer = 0;
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            isMouseDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }
        if (isMouseDown && (GameMananger.Instance.gameState == GameState.Running))
        {        
            Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;
            transform.position = transform.position + offset;
            checkPosition();//检查飞机的位置            
        }
        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//鼠标位置更新，用于下一帧

        doubleGunTime -= Time.deltaTime;   //双枪存在的时间每帧递减
        if (doubleGunTime > 0)//如果双枪还存在
        {
            if (weapon == 1)  //如果当前武器种类只有1个
            {
                changeToDoubleWeapon();//改变当前武器
            }            
        }
        else//如果双枪不存在
        {
            if (weapon == 2)  //当前武器状态为2种
            {
                changeToBeseWeapon();  //还原当前武器
            }
        }

    }
    /// <summary>
    /// Changes to double weapon.
    /// </summary>
    private void changeToDoubleWeapon()
    {
        weapon = 2;  //武器数量设为2个
        //gun_Center.openFire();
        gunLeft.OpenFire();  //打开左边发射子弹
        gunRight.OpenFire(); //打开右边发射子弹
    }
    /// <summary>
    /// Changes to bese weapon.
    /// </summary>
    private void changeToBeseWeapon()
    {
        weapon = 1;   //武器数量设为1
        //gun_Center.openFire();
        gunLeft.StopFire();  //左边停止发射
        gunRight.StopFire(); //右边停止发射
    }
    /// <summary>
    /// Check the position.
    /// </summary>
    private void checkPosition() 
    {
        Vector3 pos = transform.position;
        float x = pos.x;
        float y = pos.y;

        x = x < screenXMin ? screenXMin : x;
        x = x > screenXMax ? screenXMax : x;
        y = y < screenYMin ? screenYMin : y;
        y = y > screenYMax ? screenYMax : y;

        transform.position = new Vector3(x, y, 0);  //重新改变飞机的位置
    }

    void OnTriggerEnter2D(Collider2D otherCollider)  //主角碰撞检测代码
    {
        if (otherCollider.tag == "Award")//标签为补给品
        {
            Enemy enemy = otherCollider.GetComponent<Enemy>();  //判断补给品的种类
            if (enemy.planeType == EnemyType.support)  //子弹补给
            {
                doubleGunTime = resetWeaponTime; //设置双枪时间
                Destroy(otherCollider.gameObject);
            }
            if (enemy.planeType == EnemyType.boom)//炸弹
            {
                BombManager.Instance.AddBomb();
                Destroy(otherCollider.gameObject);
            }
        }
        if (otherCollider.tag == "Enemy")
        {
            anim.SetBool("Dead", true);
            StartCoroutine(Delay());
        }

       
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.4f);
        GameOver.Instance.showScore(GameMananger.Instance.score);
        Destroy(this.gameObject);
    }
}
