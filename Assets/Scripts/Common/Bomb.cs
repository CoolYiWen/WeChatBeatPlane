using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    void OnMouseUpAsButton()
    {
        BombManager.Instance.UseBomb();
    }
}
