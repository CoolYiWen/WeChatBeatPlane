using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed = 10;  // 子弹的速度
	

	void Update () 
    {
        //子弹的运动
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);
        //射出屏幕后销毁
        if (this.transform.position.y > 4.4) 
        {
            PoolManager.Instance.AddBulletPool(this.gameObject);//存入对象池中
        }
	}


    void OnTriggerEnter2D(Collider2D otherCollider)//碰撞处理
    {
        if (otherCollider.gameObject.tag == "Enemy") //如果打中的是敌机
        {
            otherCollider.gameObject.GetComponent<Enemy>().Behit();//触发被击中函数
            PoolManager.Instance.AddBulletPool(this.gameObject);//存入对象池中
        }
        
    }
   
}
