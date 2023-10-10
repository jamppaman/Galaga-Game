using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultPaths : MonoBehaviour
{
    EnemyManager eManager;
    int chosenPattern;
    float beforePattern = 2f;

    void Start()
    {
        eManager = GetComponent<EnemyManager>();
    }

    public void SetAttackParametres()
    {
        beforePattern = 0.5f;
        chosenPattern = Random.Range(0,3);
    }

    public Vector3 AssaultPattern(GameObject mover)
    {
        Enemy attackerValues = mover.GetComponent<Enemy>();
        Vector3 movePos = new Vector3
            (attackerValues.currentPosition.x,attackerValues.currentPosition.y,attackerValues.currentPosition.z);
        switch (chosenPattern)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
        }
        
        return movePos;
    }
}
