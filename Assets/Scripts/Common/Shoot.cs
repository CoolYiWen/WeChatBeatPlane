using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
    public GameObject bullet;  //子弹
    public float rate = 0.8f;   //射击频率
		
    /// <summary>
    /// Fire,Instantiate the bullet.
    /// </summary>
    void  Fire()
    {
        GameObject ob = PoolManager.Instance.GetBulletPool(bullet);
        if (ob == null)
        {
            GameObject.Instantiate(bullet, transform.position, Quaternion.identity);//初始化子弹
        }
        else
        {
            ob.transform.position = transform.position;
        }
    }
    /// <summary>
    /// Opens the fire.
    /// </summary>
    public void OpenFire()
    {
        InvokeRepeating("Fire", 0.2f, rate);//循环调用Fire方法
    }
    /// <summary>
    /// Stops the fire.
    /// </summary>
    public void StopFire()
    {
        CancelInvoke("Fire"); //取消循环调用Fire方法
    }
}
