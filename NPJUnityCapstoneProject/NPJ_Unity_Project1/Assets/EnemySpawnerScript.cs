using UnityEngine;
using System.Collections;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] float xRange;
    [SerializeField] float yRange;

    [SerializeField] float xLocation;
    [SerializeField] float yLocation;

    [SerializeField] float spawnTime;

    float timer = 0;

	// Use this for initialization
	void Start ()
    {
        xLocation = Random.Range(-xRange, xRange);
        yLocation = Random.Range(-yRange, yRange);
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            Instantiate<GameObject>(enemyToSpawn);
            enemyToSpawn.transform.localPosition = new Vector3(xLocation, yLocation, transform.localPosition.z);
            xLocation = Random.Range(-xRange, xRange);
            yLocation = Random.Range(-yRange, yRange);
        }
	}
}
