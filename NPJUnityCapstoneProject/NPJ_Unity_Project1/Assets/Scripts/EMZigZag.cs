using UnityEngine;
using System.Collections;

public class EMZigZag : EnemyMovementInterface
{
    Transform enemy;
    EnemyScript enemyScript;
    float randX = 0;
    float randY = 0;
    Vector3 zigZagDestination;

    public void Move ()
    {
        if ((Mathf.Round(enemy.localPosition.x - zigZagDestination.x) < 1) && (Mathf.Round(enemy.localPosition.y - zigZagDestination.y) < 1))
        {
            randX = Random.Range(-10, 10);
            randY = Random.Range(-10, 10);
            zigZagDestination = new Vector3(enemy.localPosition.x + randX, enemy.localPosition.y + randY, enemy.localPosition.z);
        }
        else
        {
            enemy.localPosition = Vector3.MoveTowards(enemy.localPosition, zigZagDestination, enemyScript.speed * Time.deltaTime);
        }
    }

    public void EstablishEnemy(Transform enemyUsingThis)
    {
        enemy = enemyUsingThis;
        enemyScript = enemy.GetComponent<EnemyScript>();
        zigZagDestination = enemy.position;
    }
}
