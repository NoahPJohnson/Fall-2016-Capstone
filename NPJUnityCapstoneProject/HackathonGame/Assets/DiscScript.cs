using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscScript : MonoBehaviour
{
    [SerializeField] bool player1;
    [SerializeField] bool caught;
    [SerializeField] float speed;
    [SerializeField] float speedMax;
    [SerializeField] float speedMin;
    [SerializeField] float incrementForce;
    [SerializeField] float initialForce;
    [SerializeField] float initialForceMin;
    [SerializeField] int pointValue;
    [SerializeField] int pointMax;
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
        player1 = transform.parent.GetComponent<CatchScript>().GetPlayerType();
        if (player1 == true)
        {
            GetComponent<Renderer>().material.SetColor(Shader.PropertyToID("_Color"), Color.red);
        }
        else
        {
            GetComponent<Renderer>().material.SetColor(Shader.PropertyToID("_Color"), Color.blue);
        }
        transform.parent = null;
        if (initialForce < initialForceMin)
        {
            initialForce = initialForceMin;
        }
        speed = initialForce;
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
        if (player1 == transform.parent.GetComponent<CatchScript>().GetPlayerType())
        {
            transform.parent.GetComponent<CatchScript>().IncrementScore(pointValue);
            pointValue = 0; 
        }
        
        
        transform.localPosition = new Vector3(0, 0, 1);
        transform.forward = transform.parent.forward;
        initialForce = discRigidbody.velocity.magnitude * 100/3;
        discRigidbody.velocity = Vector3.zero;
        discRigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
    }

    void FlyingState()
    {
        if (discRigidbody.velocity.magnitude < speedMin)
        {
            discRigidbody.velocity = transform.forward * speedMin;
        }
        if (discRigidbody.velocity.magnitude > speedMax)
        {
            discRigidbody.velocity = transform.forward * speedMax;
        }
        transform.forward = discRigidbody.velocity;
        speed = discRigidbody.velocity.magnitude;
        //discRigidbody.AddForce(Vector3.forward * speed, ForceMode.Impulse);
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void CaughtState()
    {
        if (initialForce > initialForceMin)
        {
            initialForce -= Time.deltaTime * 200;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        {
            if (pointValue < pointMax)
            {
                pointValue *= 2;
                if (pointValue < 1)
                {
                    pointValue += 1;
                }
            }
            if (speed < speedMax)
            {
                discRigidbody.AddForce(transform.forward * incrementForce/*, ForceMode.Impulse*/);
            }
        } 
    }
}
