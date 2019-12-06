using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    void Update()
    {
        if (!GameManager.instance.gameOver)
        {
            transform.position += Vector3.left * GameManager.instance.speed * Time.deltaTime;
        }

        if (transform.position.x < -11) {
            Destroy(gameObject);
        }
    }
}
