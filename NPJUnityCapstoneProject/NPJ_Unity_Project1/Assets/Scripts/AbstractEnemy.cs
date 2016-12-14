using UnityEngine;
using System.Collections;

public abstract class AbstractEnemy : MonoBehaviour
{
    EnemyMovementInterface movementType;
    float speed;
    float circleRadius;

    public AbstractEnemy()
    {

    }
    
    public void SetMovementType(EnemyMovementInterface type)
    {
        movementType = type;
    }

    public void ApplyMovement()
    {
        movementType.Move();
    }
}
