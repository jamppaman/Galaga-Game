using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float lifeTime = 5f;
    public bool firedByPlayer = true;

    void Start()
    {
        if (firedByPlayer == false)
        {
            bulletSpeed = bulletSpeed * -1f;
        }
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0) 
        {
            DestroyBullet();
        }
    }

    void DestroyBullet() 
    {
        if (firedByPlayer == true)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            Player playerValues = playerObject.GetComponent<Player>();
            playerValues.usedAmmo--;
        }
        Destroy(this.gameObject);
    }

}
