using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : SingletonUnity<PoolManager> {

    public List<GameObject> bullets = new List<GameObject>();       //子弹缓存
    public List<GameObject> smallEnemys = new List<GameObject>();   //小飞机缓存

    /// <summary>
    /// Adds the bullet pool.
    /// </summary>
    public void AddBulletPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bullets.Add(bullet);

        if (bullets.Count > 10)
        {
            GameObject ob = bullets[0];
            bullets.RemoveAt(0);
            Destroy(ob);
        }
    }
    /// <summary>
    /// Gets the bullet pool.
    /// </summary>
    /// <param name="prefab">Bullet Prefab.</param>
    public GameObject GetBulletPool(GameObject prefab)
    {
        if (bullets.Count != 0)
        {
            foreach (GameObject bullet in bullets)
            {
                if (bullet.name == prefab.name)
                {
                    bullets.Remove(bullet);
                    bullet.SetActive(true);
                    return bullet;
                }
            }
        }
        return null;
    }
    /// <summary>
    /// Adds the enemy pool.
    /// </summary>
    /// <param name="enemy">Enemy.</param>
    public void AddEnemyPool(GameObject enemy)
    {
        if (smallEnemys.Count < 10)
        {
            enemy.SetActive(false);
            smallEnemys.Add(enemy);
        }
        else
        {
            Destroy(enemy);
        }
    }
    /// <summary>
    /// Gets the enemy pool.
    /// </summary>
    /// <returns>The enemy pool.</returns>
    public GameObject GetEnemyPool()
    {
        if (smallEnemys.Count == 0)
        {
            return null;
        }
        else
        {
            GameObject enemy = smallEnemys[0];
            smallEnemys.RemoveAt(0);
            enemy.SetActive(true);
            return enemy;
        }
    }

}
