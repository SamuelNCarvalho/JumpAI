using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyObject;

    void Start()
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            if (!GameManager.instance.gameOver)
            {
                Instantiate(enemyObject);
            }

            yield return new WaitForSeconds(Random.Range(GameManager.instance.minEnemySpawnInterval, GameManager.instance.maxEnemySpawnInterval));
        }

    }
}
