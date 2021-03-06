﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    float startPos, length;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
       if (!GameManager.instance.gameOver)
       {
            transform.position += Vector3.left * (GameManager.instance.speed / 10) * Time.deltaTime;
       }

       if (transform.position.x < -length)
       {
            transform.position = new Vector2(length, transform.position.y);
       }

    }
}
