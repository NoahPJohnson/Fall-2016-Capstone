using UnityEngine;
//using System;
using System.Collections;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] Transform[] spawnArray;
    //[SerializeField] bool front;
    [SerializeField] float xRange;
    [SerializeField] float yRange;
    [SerializeField] GameObject[] enemyArray;
    EnemyMovementInterface[] enemyMovementArray;
    
    //[SerializeField] float xLocation;
    //[SerializeField] float yLocation;

    [SerializeField] float spawnTime;

    float timer = 0;

	// Use this for initialization
	void Start ()
    {
        EnemyMovementInterface EM0 = new EMStraight();
        EnemyMovementInterface EM1 = new EMStrafe();
        EnemyMovementInterface EM2 = new EMCircle();
        EnemyMovementInterface EM3 = new EMAvoid();
        EnemyMovementInterface EM4 = new EMZigZag();
        enemyMovementArray = new EnemyMovementInterface[5] {EM0, EM1, EM2, EM3, EM4};
        Debug.Log("Move Types = " + enemyMovementArray[0]);
        //xLocation = Random.Range(-xRange, xRange);
        //yLocation = Random.Range(-yRange, yRange);
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            timer = 0;
            Debug.Log ("spawn time = " + spawnTime + "    timer = " + timer);
            EnemyData spawnData = RandomizeEnemyData();
			//Debug.Log ("Enemy to spawn: " + spawnData.enemyToSpawn.name);
            SpawnEnemy(spawnData);
            //Instantiate<GameObject>(enemyToSpawn);
            //enemyToSpawn.transform.localPosition = new Vector3(xLocation, yLocation, transform.localPosition.z);
            //xLocation = Random.Range(-xRange, xRange);
            //yLocation = Random.Range(-yRange, yRange);
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

        data.spawnIndex = Random.Range(0, 3);
        Debug.Log("Spawn Index: " + data.spawnIndex);
        if (Random.Range(0, 2) == 0)
        {
            data.front = false;
        }
        else
        {
            data.front = true;
        }
        Debug.Log("front = " + data.front);
        data.xPosition = Random.Range(-xRange, xRange);
        data.yPosition = Random.Range(-yRange, yRange);
        data.enemyToSpawn = enemyArray[Random.Range(0, enemyArray.Length)];
        return data;
    }
    string RandomlyGenerateMovementTypes()
    {
        int rand = Random.Range(0, 16);
        string randBinary = System.Convert.ToString(rand, 2);
        return randBinary;
    }

    void SpawnEnemy(EnemyData data)
    {
        GameObject spawnedEnemy = Instantiate<GameObject>(data.enemyToSpawn);
        Debug.Log("INSTANTIATED");
        Transform spawnPoint = spawnArray[data.spawnIndex];
        if (data.front == false)
        {
            spawnPoint = spawnPoint.GetChild(0);
        }
        spawnedEnemy.transform.position = spawnPoint.position;
        spawnedEnemy.transform.forward = spawnPoint.forward;
        spawnedEnemy.transform.position += new Vector3(data.xPosition, data.yPosition, 0);
        spawnedEnemy.GetComponent<EnemyScript>().SetMovementRange(spawnPoint.position);
        spawnedEnemy.GetComponent<EnemyScript>().EstablishMoveList();
        string EMBinary = RandomlyGenerateMovementTypes();
        int iterator = 0;
        foreach (char c in EMBinary)
        {
            Debug.Log("EMBinary: " + EMBinary);
            if (c != '0')
            {
                //Debug.Log("Iterator = " + iterator);
                //Debug.Log("MovementType = " + enemyMovementArray[iterator]);
                spawnedEnemy.GetComponent<EnemyScript>().AddMovementType(enemyMovementArray[iterator]);
                enemyMovementArray[iterator].EstablishEnemy(spawnedEnemy.transform);
            }
            iterator++;
        }
        spawnedEnemy.SetActive(true);
    }

    
}
