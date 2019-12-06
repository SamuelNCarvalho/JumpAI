using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxMiddleground : MonoBehaviour
{
    float startPos, length;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponentInChildren<SpriteRenderer>().bounds.size.x * 3;
    }

    void Update()
    {
       if (!GameManager.instance.gameOver)
       {
            transform.position += Vector3.left * (GameManager.instance.speed / 5) * Time.deltaTime;
       }

       if (transform.position.x < -length)
       {
            transform.position = new Vector2(length, transform.position.y);
       }

    }
}
