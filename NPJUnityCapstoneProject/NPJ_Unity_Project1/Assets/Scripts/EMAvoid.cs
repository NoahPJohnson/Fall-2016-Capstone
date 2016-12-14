using UnityEngine;
using System.Collections;

public class EMAvoid : EnemyMovementInterface
{
    Transform enemy;
    EnemyScript enemyScript;
    [SerializeField] Vector3 inputPosition;

    public void Move()
    {
        Ray rayToAvoid = GameObject.FindGameObjectWithTag("Input").GetComponent<RaycastScript>().GetInputRay();

        inputPosition = rayToAvoid.origin + (rayToAvoid.direction * (Vector3.Magnitude(enemy.localPosition - rayToAvoid.origin)));
        //Debug.Log("Magnitude: " + Vector3.Magnitude(inputPosition - transform.localPosition));
        //Debug.DrawRay(transform.localPosition, inputPosition - transform.localPosition);
        if (Vector3.Magnitude(inputPosition - enemy.localPosition) < enemyScript.avoidRadius)
        {
            enemy.Translate(Vector3.Scale(new Vector3(1, -1, 0), (inputPosition - enemy.localPosition)) * enemyScript.speed * Time.deltaTime);
        }
    }

    public void EstablishEnemy(Transform enemyUsingThis)
    {
        enemy = enemyUsingThis;
        enemyScript = enemy.GetComponent<EnemyScript>();
    }
}
