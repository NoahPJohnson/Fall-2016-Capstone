using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscScript : MonoBehaviour
{
    [SerializeField] bool Player1;
    [SerializeField] bool caught;
    [SerializeField] float speed;
    [SerializeField] float speedMax;
    [SerializeField] float increment;
    [SerializeField] float initialSpeed;
    [SerializeField] float initialSpeedMin;
    [SerializeField] int pointValue;
    Rigidbody discRigidbody;

	// Use this for initialization
	void Start ()
    {
        discRigidbody = GetComponent<Rigidbody>();
        caught = true;
        pointValue = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (caught == true)
        {
            CaughtState();
        }
        else
        {
            FlyingState();
        }
	}

    public void ThrowDisc()
    {
        transform.parent = null;
        speed = initialSpeed;
        caught = false;
        discRigidbody.AddForce(transform.forward * speed/*, ForceMode.Impulse*/);
    }

    public void CatchDisc(Transform catchBox)
    {
        caught = true;
        transform.parent = catchBox;
        transform.localPosition = new Vector3(0, 0, 1);
        transform.forward = transform.parent.forward;
        initialSpeed = speed;
    }

    void FlyingState()
    {
        //discRigidbody.AddForce(Vector3.forward * speed, ForceMode.Impulse);
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void CaughtState()
    {
        if (initialSpeed > initialSpeedMin)
        {
            initialSpeed -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        {
            if (speed < speedMax)
            {
                //speed += increment;
                //Increase Speed (Double?)
                //Increase Point Value (Double?)
                
            }
        } 
    }
}
