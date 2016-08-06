using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

    void OnMouseUpAsButton()
    {
        SceneManager.LoadScene(0);//重新加载场景
    }

}
