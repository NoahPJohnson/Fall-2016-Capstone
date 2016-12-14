using UnityEngine;
using System.Collections;

public interface EnemyMovementInterface
{
    void Move();
    void EstablishEnemy(Transform enemyUsingThis);
}
