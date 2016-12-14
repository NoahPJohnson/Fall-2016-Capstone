using UnityEngine;
using System.Collections;

public class EMCircle : EnemyMovementInterface
{
    Transform enemy;
    EnemyScript enemyScript;
    Vector3 revolvePoint;

    public void Move ()
    {
        revolvePoint = new Vector3(enemy.localPosition.x, enemy.localPosition.y + enemyScript.circleRadius, enemy.localPosition.z);
        enemy.RotateAround(revolvePoint, Vector3.forward, enemyScript.speed * Time.deltaTime);
    }

    public void EstablishEnemy(Transform enemyUsingThis)
    {
        enemy = enemyUsingThis;
        enemyScript = enemy.GetComponent<EnemyScript>();
    }
}
