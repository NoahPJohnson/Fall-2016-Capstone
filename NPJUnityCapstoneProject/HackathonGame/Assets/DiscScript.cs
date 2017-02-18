using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscScript : MonoBehaviour
{
    [SerializeField] bool Player1;
    [SerializeField] bool caught;
    [SerializeField] float speed;
    [SerializeField] float speedMax;
    [SerializeField] float speedMin;
    [SerializeField] float increment;
    [SerializeField] float initialSpeed;
    [SerializeField] float initialSpeedMin;
    [SerializeField] int pointValue;
    Rigidbody discRigidbody;
    Collider discCollider;

	// Use this for initialization
	void Start ()
    {
        discRigidbody = GetComponent<Rigidbody>();
        discCollider = GetComponent<CapsuleCollider>();
        caught = true;
        pointValue = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
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
        discCollider.enabled = true;
        discRigidbody.constraints = RigidbodyConstraints.None;
        discRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        discRigidbody.AddForce(transform.forward * speed/*, ForceMode.Impulse*/);
    }

    public void CatchDisc(Transform catchBox)
    {
        //Debug.Log("Disc is caught!!");
        caught = true;
        discCollider.enabled = false;
        transform.parent = catchBox;
        transform.localPosition = new Vector3(0, 0, 1);
        transform.forward = transform.parent.forward;
        discRigidbody.velocity = Vector3.zero;
        discRigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
        initialSpeed = speed;
    }

    void FlyingState()
    {
        if (discRigidbody.velocity.magnitude < speedMin)
        {
            discRigidbody.velocity = transform.forward * speedMin;
        }
        transform.forward = discRigidbody.velocity;
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
