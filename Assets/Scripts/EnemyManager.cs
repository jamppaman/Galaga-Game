using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Spawn Variables
    public int enemyAmount = 5;
    public int enemyPerRow = 5;
    public int enemySpacing = 7;
    public bool extendingRows = false;
    public bool fillTillFull = false;
    public GameObject enemyPrefab;
    public List<GameObject> enemyList = new List<GameObject>();

    // Move Variables
    public float enemySpeed = 10f;
    public bool enemySide = false;
    private float sideMultiplier = 1f;
    public int boundary = 35;

    // Assault variables
    public bool assaultInProgress = false;
    public float assaultTimer;
    public float assaultSpeed = 15f;
    private int picker = 0;
    public bool timeToReturn = false;
    private Enemy iAmAttacking = null;
    //AssaultPaths = pather;

    // Shooting Variables
    public float currentCooldown = 2f;
    public bool inCooldown = false;
    public int enemyAmmoPool = 3;
    void Start()
    {
        currentCooldown = 2f;
        inCooldown = false;
        enemyAmmoPool = 3;
        assaultTimer = 3f;
        SpawnEnemies();
    }

    void FixedUpdate()
    {
        currentCooldown -= Time.deltaTime;
        if(inCooldown == true)
        {
            currentCooldown += 0.2f;
            inCooldown = false;
        }
        if(assaultInProgress == false && assaultTimer <= 0f)
        {
            Debug.Log("Time to attack");
            iAmAttacking = AssaultStarter();
            //pather.SetAttackParametres();
        }
        EnemyMover();
        assaultTimer -= Time.deltaTime;
    }

    private void EnemyMover()
    {
        bool check = PositionChecker(enemyList);
        if (check == false)
        {
            sideMultiplier = sideMultiplier * -1;
        }
        foreach (GameObject moveEnemy in enemyList)
        {
            Enemy curEnemyVal = moveEnemy.GetComponent<Enemy>();
            if(curEnemyVal.assaulting == false)
            {
                moveEnemy.transform.Translate(Vector3.left * enemySpeed * sideMultiplier * Time.deltaTime);
            }
            else
            {
                Debug.Log("Going down");
                AssaultMover(moveEnemy);
            }
        }
    }

    private Enemy AssaultStarter()
    {
        assaultInProgress = true;
        picker = Random.Range(0, enemyList.Count);
        Enemy attacker = enemyList[picker].GetComponent<Enemy>();
        attacker.currentPosition = (enemyList[picker].transform.position);
        attacker.returnPosition = new Vector3(attacker.currentPosition.x, attacker.currentPosition.y, attacker.currentPosition.z);
        attacker.assaulting = true;
        timeToReturn = false;
        Debug.Log("Starting assault");
        return attacker;
    }

    private void AssaultMover(GameObject attackerE)
    {
        Enemy attackerVal = attackerE.GetComponent<Enemy>();
        attackerVal.currentPosition = attackerE.transform.position;

        if(timeToReturn == false)
        {
            if (attackerVal.currentPosition.y < -9f)
            {
                Debug.Log("I am making my move");
                attackerVal.currentPosition.y = 40f;
                Debug.Log(enemyList.Count);
                if (enemyList.Count > 1)
                {
                    timeToReturn = true;
                }
                else
                {
                    // pather.SetAttackParametres();
                }
            }
            // Kommentoi tämä myöhemmin
            attackerVal.currentPosition.y = attackerVal.currentPosition.y - Time.deltaTime * assaultSpeed;
            attackerVal.currentPosition.x = Mathf.Sin(attackerVal.currentPosition.y)* 10f;
            // Kommentti loppuu tähän
            // attackerVal.currentPosition = pather.AssaultPattern(attackerE);
            //attackerE.transform.position = attackerVal.currentPosition; // <-- TÄSSÄ OLI SE LIIKE ONGELMA KOMMENTIT POIS
        }
        else
        {
            float step = assaultSpeed * Time.deltaTime;
            attackerE.transform.position = Vector3.MoveTowards(attackerVal.currentPosition,
                attackerVal.returnPosition, step);
            float dis = Vector2.Distance(attackerVal.currentPosition , attackerVal.returnPosition);
            if (dis < 0.2f) 
            {
                attackerVal.assaulting = false;
                assaultInProgress = false;
                assaultTimer = Random.Range(0.5f, 1.8f);
                attackerE.transform.position = attackerVal.returnPosition;
                Debug.Log("I have returned");
            }
        }
        attackerVal.returnPosition = attackerVal.returnPosition + 
            (Vector3.left * enemySpeed * sideMultiplier * Time.deltaTime);
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

    void SpawnEnemies()
    {
        int limiter = enemyPerRow;
        int enemySpawned = 0;
        int enemyCurrentRow = 0;
        int rowAdder = 0;

        while (enemySpawned < enemyAmount)
        {
            for (int i = 0; i < limiter; i++)
            {
                GameObject spawnedEnemy = Instantiate(enemyPrefab, this.transform);
                spawnedEnemy.transform.position = new Vector3((enemySpacing * i - enemySpacing * rowAdder) - limiter / 2, 31 + enemyCurrentRow, 0);
                enemyList.Add(spawnedEnemy);
                enemySpawned++;
                if (enemySpawned >= enemyAmount && fillTillFull == false)
                {
                    break;
                }
            }
            enemyCurrentRow = enemyCurrentRow - 5;
            if(extendingRows == true)
            {
                limiter += 2;
                rowAdder++;
            }
        }
    }
}

