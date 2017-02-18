using UnityEngine;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] Transform enemySpawner;
    [SerializeField] Transform player;
    [SerializeField] GameObject projectileEnemy;
    [SerializeField] GameObject firedEnemyProjectile;
    public float speed;
    public float circleRadius;
    public float avoidRadius;
    float limitFactor;
    [SerializeField] Vector3 limitVector;
    bool clampZ = false;
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    [SerializeField] bool attacks;

    [SerializeField] string movementDisplay;

    List<EnemyMovementInterface> moveList;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        enemySpawner = GameObject.FindGameObjectWithTag("NeutralLook").transform;
        //moveList = new List<EnemyMovementInterface>();
        currentHealth = maxHealth;
    }
	
	// Update is called once per frame
	void Update ()
    {
        limitFactor = 0;// 2 + Vector3.Distance(transform.position, player.position);
        //Debug.Log("MoveList" + moveList);
        //Debug.Log("Move List count = " + moveList.Count);
        for (int i = 0; i < moveList.Count; i ++)
        {
            //Debug.Log("Do this: " + moveList[i]);
            moveList[i].Move();
        }
        if (clampZ == true)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, limitVector.x - 12f, limitVector.x + 12f),
            Mathf.Clamp(transform.position.y, limitVector.y - 12, limitVector.y + 12),
            Mathf.Clamp(transform.position.z, enemySpawner.position.z - 17, enemySpawner.position.z + 17));
        }
        else
        {
            //transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, limitVector.x - limitFactor, limitVector.x + limitFactor),
            //Mathf.Clamp(transform.localPosition.y, limitVector.y - limitFactor, limitVector.y + limitFactor),
            //transform.localPosition.z);
        }
    }

    public void FireProjectile ()
    {
        if (attacks == true)
        {
            Debug.Log("ENEMY Attack");
            firedEnemyProjectile = Instantiate<GameObject>(projectileEnemy);
            firedEnemyProjectile.GetComponent<ProjectileScript>().SetProjectileType();
            firedEnemyProjectile.transform.position = transform.position;
            firedEnemyProjectile.transform.rotation = Quaternion.Euler(Vector3.back);
        }
    }

    public void SetMovementRange (Vector3 spawnPosition)
    {
        limitVector = spawnPosition;
    }

    public void EstablishMoveList()
    {
        //Debug.Log("Established moveList for: " + gameObject);
        moveList = new List<EnemyMovementInterface>();
    }

    public void SetMovementType (EnemyMovementInterface type)
    {
        moveList.Clear();
        EnemyMovementInterface typeToAdd = type;
        moveList.Add(typeToAdd);
    }

    public void AddMovementType (EnemyMovementInterface type)
    {
        //Debug.Log("Type: " + type);
        movementDisplay += type.ToString();
        movementDisplay += "_";
        moveList.Add(type);
    }

    public void RemoveMovementType (EnemyMovementInterface type)
    {
        moveList.Remove(type);
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Projectile")
        {
            currentHealth -= other.GetComponent<ProjectileScript>().GetDamage();
            if (currentHealth < 1)
            {
                moveList.Clear();
                Destroy(gameObject);
            }
            other.GetComponent<ProjectileScript>().ReduceProjectileHP();
        }
        //Debug.Log("HIT");
    }
}
