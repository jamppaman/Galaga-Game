using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float playerSpeed = 10f;
    public int playerAmmo = 2;
    public int usedAmmo = 0;

    public GameObject bullet;
    public GameObject eManager;
    private EnemyManager managerVal;

    void Start()
    {
       managerVal = eManager.GetComponent<EnemyManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    void FixedUpdate()
    {
        // Movement inputs
        if(Input.GetKey(KeyCode.LeftArrow) && this.transform.position.x > -managerVal.boundary|| Input.GetKey(KeyCode.A) && this.transform.position.x > -managerVal.boundary)
        {
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x < managerVal.boundary || Input.GetKey(KeyCode.D) && this.transform.position.x < managerVal.boundary)
        {
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        }
    }

    void Shoot() 
    {
        if(usedAmmo <= playerAmmo) 
        {
            Transform firePosition = this.transform;
            Instantiate(bullet, new Vector3(firePosition.position.x, firePosition.position.y, firePosition.position.z), firePosition.rotation);
            usedAmmo++;
        }
    }

    public void PlayerDie()
    {
        SceneManager.LoadScene(sceneName: "Main Menu");
    }
}
