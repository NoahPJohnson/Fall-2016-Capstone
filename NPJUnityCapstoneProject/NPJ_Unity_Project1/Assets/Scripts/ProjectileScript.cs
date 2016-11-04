using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] Transform projectileTarget;
    [SerializeField] Vector3 toTargetVector;
    [SerializeField] Vector3 turnToTargetRotation;
    [SerializeField] bool enemyProjectile = false;
    [SerializeField] float speed = 8f;
    [SerializeField] float accelerationStart = 1f;
    [SerializeField] float accelerationMax = 4f;
    [SerializeField] int projectileBonus = 0;
    //[SerializeField] float distance;
    [SerializeField] float lerpValue = 0;

    [SerializeField] int damage = 1;
    [SerializeField] int hitPoints = 1;
    float timer = 0;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (projectileTarget != null)
        {
            timer += Time.deltaTime;
            toTargetVector = projectileTarget.position - transform.position;
            lerpValue = timer;
            turnToTargetRotation = Vector3.RotateTowards(transform.forward, toTargetVector, lerpValue, 0);
            transform.rotation = Quaternion.LookRotation(turnToTargetRotation);
            if (accelerationStart < accelerationMax)
            {
                accelerationStart = timer * (accelerationMax/2);
            }
            transform.Translate(Vector3.forward * speed * accelerationStart * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
	}

    public void SetProjectileTarget (Transform targetObject)
    {
        if (targetObject != null)
        {
            projectileTarget = targetObject;
        }
    }

    public void SetProjectileBoost (int boost)
    {
        projectileBonus = boost;
        transform.localScale *= projectileBonus;
        damage += projectileBonus;
        accelerationMax *= projectileBonus;
        hitPoints += projectileBonus;
    }
    
    public void SetProjectileType ()
    {
        enemyProjectile = true;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            projectileTarget = GameObject.FindGameObjectWithTag("Player").transform;
        }
        gameObject.tag = "Enemy";
    }

    public int GetDamage ()
    {
        return damage;
    }

    public void ReduceProjectileHP ()
    {
        hitPoints--;
        if (hitPoints < 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (enemyProjectile == true)
        {
            if (other.tag == "Projectile")
            {
                hitPoints -= other.GetComponent<ProjectileScript>().GetDamage();
                if (hitPoints < 1)
                {
                    Destroy(gameObject);
                }
                other.GetComponent<ProjectileScript>().ReduceProjectileHP();
            }
        }
    }
}
