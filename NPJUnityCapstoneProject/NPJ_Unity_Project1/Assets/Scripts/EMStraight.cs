using UnityEngine;
using System.Collections;

public class EMStraight : EnemyMovementInterface 
{
    Transform enemy;
    EnemyScript enemyScript;

    public void Move ()
    {
        //Debug.Log("Enemy: " + enemy + " Goes Straight " + enemyScript.speed);
        enemy.Translate(Vector3.forward * enemyScript.speed * Time.deltaTime);
    }

    public void EstablishEnemy(Transform enemyUsingThis)
    {
        enemy = enemyUsingThis;
        enemyScript = enemy.GetComponent<EnemyScript>();
    }
}
