using UnityEngine;
using System.Collections;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] Transform[] spawnArray;
    //[SerializeField] bool front;
    [SerializeField] float xRange;
    [SerializeField] float yRange;
    [SerializeField] GameObject[] enemyArray;

    //[SerializeField] float xLocation;
    //[SerializeField] float yLocation;

    [SerializeField] float spawnTime;

    float timer = 0;

	// Use this for initialization
	void Start ()
    {
        //xLocation = Random.Range(-xRange, xRange);
        //yLocation = Random.Range(-yRange, yRange);
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
			Debug.Log ("spawn time");
            EnemyData spawnData = RandomizeEnemyData();
			Debug.Log ("Enemy to spawn: " + spawnData.enemyToSpawn.name);
            SpawnEnemy(spawnData);
            //Instantiate<GameObject>(enemyToSpawn);
            //enemyToSpawn.transform.localPosition = new Vector3(xLocation, yLocation, transform.localPosition.z);
            //xLocation = Random.Range(-xRange, xRange);
            //yLocation = Random.Range(-yRange, yRange);
            timer = 0;
        }
	}

    struct EnemyData
    {
        public int spawnIndex;
        public bool front;
        public float xPosition;
        public float yPosition;
        public GameObject enemyToSpawn;
    }

    EnemyData RandomizeEnemyData()
    {
        EnemyData data;

        data.spawnIndex = Random.Range(0, 2);
        if (Random.Range(0, 1) == 0)
        {
            data.front = false;
        }
        else
        {
            data.front = true;
        }
        data.xPosition = Random.Range(-xRange, xRange);
        data.yPosition = Random.Range(-yRange, yRange);
        data.enemyToSpawn = enemyArray[Random.Range(0, enemyArray.Length)];
        return data;
    }

    void SpawnEnemy(EnemyData data)
    {
        Instantiate<GameObject>(data.enemyToSpawn);
        Transform spawnPoint = spawnArray[data.spawnIndex];
        if (data.front == false)
        {
            spawnPoint = spawnPoint.GetChild(0);
        }
		data.enemyToSpawn.transform.position = spawnPoint.position;
		data.enemyToSpawn.transform.forward = spawnPoint.forward;
        data.enemyToSpawn.transform.localPosition = new Vector3(data.xPosition, data.yPosition, 0);
    }
}
