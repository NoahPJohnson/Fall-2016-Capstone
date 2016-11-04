using UnityEngine;
using System.Collections;

public class EnemyMovementScript : MonoBehaviour
{
    EnemyScript enemyScript;
    [SerializeField] bool useTimer;
    [SerializeField] int timeToFire;
    bool okToFire = true;
    [SerializeField] int timeToSwitch;
    bool okToSwitch = true;
    [SerializeField] bool goStraight;
    [SerializeField] bool facePlayer;
    [SerializeField] bool strafe;
    [SerializeField] bool circle;
    [SerializeField] float circleRadius = 5f;
    [SerializeField] bool avoid;
    [SerializeField] float avoidRadius = 8f;
    [SerializeField] bool zigZag;
    float randX;
    float randY;
    Vector3 zigZagDestination;
    [SerializeField] float speed = 3f;
    [SerializeField] float circleSpeed = 50f;
    Vector3 revolvePoint;
    [SerializeField] Vector3 inputPosition;

    float timer = 0;

	// Use this for initialization
	void Start ()
    {
        enemyScript = GetComponent<EnemyScript>();
        revolvePoint = new Vector3(transform.localPosition.x, transform.localPosition.y + circleRadius, transform.localPosition.z);
        if (zigZag == true)
        {
            randX = Random.Range(-10, 10);
            randY = Random.Range(-10, 10);
            zigZagDestination = new Vector3(transform.localPosition.x + randX, transform.localPosition.y + randY, transform.localPosition.z);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (useTimer == true)
        {
            timer += Time.deltaTime;
            if (Mathf.FloorToInt(timer) % timeToFire == 0 && okToFire == true) 
            {
                if (enemyScript != null)
                {
                    enemyScript.FireProjectile();
                    okToFire = false;
                }
            }
            if (Mathf.FloorToInt(timer) % timeToFire != 0 && okToFire == false)
            {
                okToFire = true;
            }
        }
	    if (goStraight == true)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (facePlayer == true)
        {
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        }
        if (strafe == true)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (circle == true)
        {  
            //Debug.DrawRay(transform.position, revolvePoint - transform.position);
            transform.RotateAround(revolvePoint, Vector3.forward, circleSpeed * Time.deltaTime);
        }
        if (avoid == true)
        {
            Ray rayToAvoid = GameObject.FindGameObjectWithTag("Input").GetComponent<RaycastScript>().GetInputRay();
           
            inputPosition = rayToAvoid.origin + (rayToAvoid.direction * (Vector3.Magnitude(transform.localPosition - rayToAvoid.origin)));
            Debug.Log("Magnitude: " + Vector3.Magnitude(inputPosition - transform.localPosition));
            Debug.DrawRay(transform.localPosition, inputPosition - transform.localPosition);
            if (Vector3.Magnitude(inputPosition - transform.localPosition) < avoidRadius)
            {
                transform.Translate(Vector3.Scale(new Vector3(1, -1, 0), (inputPosition - transform.localPosition)) * speed * Time.deltaTime);
            }
        }
        if (zigZag == true)
        {
            if ((Mathf.Round(transform.localPosition.x - zigZagDestination.x) < 1) && (Mathf.Round(transform.localPosition.y - zigZagDestination.y) < 1))
            {
                randX = Random.Range(-10, 10);
                randY = Random.Range(-10, 10);
                zigZagDestination = new Vector3(transform.localPosition.x + randX, transform.localPosition.y + randY, transform.localPosition.z);
            }
            else
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, zigZagDestination, speed * Time.deltaTime);
            }
        }
	}

    void SwitchMovementType()
    {

    }
}
