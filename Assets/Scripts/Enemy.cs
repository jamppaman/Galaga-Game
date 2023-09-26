using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameObject enemyBullet;

    public EnemyManager enemyManager;
    public GameObject playerTarget;
    void Start()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
        playerTarget = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();

    }

    void CheckDistance()
    {
        Debug.Log("tarkistetaan etaisyys");
        Vector2 playerPosX = new Vector2(playerTarget.transform.position.x, 0);
        Vector2 enemyPosX = new Vector2(this.transform.position.x, 0);
        if(Vector2.Distance(playerPosX, enemyPosX) < 0.003f)
        {
            Debug.Log("etaisyys oikea");
            EnemyFiring();
        }
    }
    void EnemyFiring()
    {
        bool allowedToFire = ReloadCheck();
        if(allowedToFire == true)
        {
            Debug.Log("lupa ampua");
            Transform firePosition = this.transform;
            Instantiate(enemyBullet, new Vector3(firePosition.position.x, firePosition.position.y, firePosition.position.z), firePosition.rotation);
            enemyManager.enemyAmmoPool--;
            enemyManager.inCooldown = true;
            if (enemyManager.enemyAmmoPool <= 0)
            {
                enemyManager.enemyAmmoPool = 3;
                enemyManager.currentCooldown = Random.Range(1.5f, 2.7f);
            }
        }
    }

    bool ReloadCheck()
    {
        bool result = true;
        if(enemyManager.currentCooldown > 0 || enemyManager.enemyAmmoPool <= 0 || enemyManager.inCooldown == true)
        {
            result = false;
        }

        return result;
    }

    public void Die()
    {
        enemyManager.enemyList.Remove(this.gameObject);
        if(enemyManager.enemyList.Count <= 0)
        {
            SceneManager.LoadScene(sceneName: "Main Menu");
        }
        Destroy(gameObject);
    }
}
