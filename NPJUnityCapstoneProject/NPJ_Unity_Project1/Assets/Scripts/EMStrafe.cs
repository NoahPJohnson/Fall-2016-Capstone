using UnityEngine;
using System.Collections;

public class EMStrafe : EnemyMovementInterface
{
    Transform enemy;
    EnemyScript enemyScript;

    public void Move()
    {
        enemy.Translate(Vector3.right * enemyScript.speed * Time.deltaTime);
    }

    public void EstablishEnemy(Transform enemyUsingThis)
    {
        enemy = enemyUsingThis;
        enemyScript = enemy.GetComponent<EnemyScript>();
    }
}
