using UnityEngine;
using System.Collections;

public class BgAnimation : MonoBehaviour {
    public static float speed = 2;
	public static float backDistance = 8.52f;



	// 背景向下移动后返回原始位置
	void Update () {
		this.transform.Translate (new Vector3 (0, -1, 0) * speed * Time.deltaTime);
		if (this.transform.position.y < -backDistance)
        {
			transform.position = new Vector3 (transform.position.x, transform.position.y + backDistance * 2, transform.position.z);
        }
	}
}
