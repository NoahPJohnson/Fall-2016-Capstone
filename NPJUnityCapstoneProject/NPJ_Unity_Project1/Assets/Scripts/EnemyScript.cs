using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] GameObject projectileEnemy;
    [SerializeField] GameObject firedEnemyProjectile;
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    [SerializeField] bool attacks;

	// Use this for initialization
	void Start ()
    {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
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

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Projectile")
        {
            currentHealth -= other.GetComponent<ProjectileScript>().GetDamage();
            if (currentHealth < 1)
            {
                Destroy(gameObject);
            }
            other.GetComponent<ProjectileScript>().ReduceProjectileHP();
        }
        //Debug.Log("HIT");
    }
}
