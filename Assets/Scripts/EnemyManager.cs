using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Spawn Variables
    public int enemyAmount = 5;
    public int enemyPerRow = 5;
    public int enemySpacing = 7;
    public GameObject enemyPrefab;
    public List<GameObject> enemyList = new List<GameObject>();

    // Move Variables
    public float enemySpeed = 15f;
    public bool enemySide = false;
    private float sideMultiplier = 1f;
    public int boundary = 35;

    // Shooting Variables
    public float currentCooldown = 2f;
    public bool inCooldown = false;
    public int enemyAmmoPool = 3;
    void Start()
    {
        currentCooldown = 2f;
        inCooldown = false;
        enemyAmmoPool = 3;

        int enemySpawned = 0;
        int enemyCurrentRow = 0;
        while(enemySpawned < enemyAmount)
        {
            for (int i = 0; i < enemyPerRow; i++)
            {
                GameObject spawnedEnemy = Instantiate(enemyPrefab, this.transform);
                spawnedEnemy.transform.position = new Vector3(enemySpacing * i, 31 + enemyCurrentRow, 0);
                enemyList.Add(spawnedEnemy);
                enemySpawned++;
                if(enemySpawned >= enemyAmount)
                {
                    break;
                }
            }
            enemyCurrentRow = enemyCurrentRow - 5;
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

        currentCooldown -= Time.deltaTime;
        if(inCooldown == true)
        {
            currentCooldown += 0.2f;
            inCooldown = false;
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

