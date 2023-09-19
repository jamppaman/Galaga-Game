using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int enemyAmount = 5;
    public int enemySpacing = 7;
    public GameObject enemyPrefab;
    public List<GameObject> enemyList = new List<GameObject>();

    public float enemySpeed = 15f;
    public bool enemySide = false;
    private float sideMultiplier = 1f;
    public int boundary = 35;

    void Start()
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            GameObject spawnedEnemy = Instantiate(enemyPrefab, this.transform);
            spawnedEnemy.transform.position = new Vector3(enemySpacing * i, 31, 0);
            enemyList.Add(spawnedEnemy);
        }
    }


    void FixedUpdate()
    {
        bool check = PositionChecker(enemyList);
        if (check == false)
        {
            sideMultiplier = sideMultiplier * -1;
        }
        foreach (GameObject moveEnemy in enemyList)
        {
            moveEnemy.transform.Translate(Vector3.left * enemySpeed * sideMultiplier * Time.deltaTime);
        }
    }

    private bool PositionChecker(List<GameObject> eList)
    {
        bool result = true;
        foreach (GameObject Enemy in eList)
        {
            if (Enemy.transform.position.x > boundary || Enemy.transform.position.x < -boundary)
            {
                result = false;
                break;
            }
        }

        return result;
    }
}

