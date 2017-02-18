using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchScript : MonoBehaviour
{
    [SerializeField] Transform catchableDisc;
    [SerializeField] Transform caughtDisc;
    DiscScript discScript;

    [SerializeField] float recoveryTime;
    [SerializeField] bool ableToCatch;
    [SerializeField] bool holdingDisc;
    [SerializeField] bool discInBox;
    float time;
    
	// Use this for initialization
	void Start ()
    {
        IdentifyDisc();
        discInBox = false;
        holdingDisc = true;
        ableToCatch = false;
        time = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetButtonDown("Fire1"))
        {
            AttemptCatch();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            AttemptThrow();
        }

        if (ableToCatch == false && holdingDisc == false)
        {
            time += Time.deltaTime;
            if (time > recoveryTime)
            {
                ableToCatch = true;
                time = 0;
            }
        }
	}

    void AttemptCatch()
    {
        if (ableToCatch == true && discInBox == true)
        {
            ableToCatch = false;
            if (discScript != null)
            {
                discScript.CatchDisc(transform);
                discInBox = false;
                holdingDisc = true;
                IdentifyDisc();
            }
        }
    }

    void AttemptThrow()
    {
        if (holdingDisc == true && transform.childCount > 1)
        {
            discScript.ThrowDisc();
            caughtDisc = null;
            discScript = null;
            holdingDisc = false;
        }
    }

    void IdentifyDisc()
    {
        caughtDisc = transform.GetChild(1);
        if (caughtDisc != null)
        {
            discScript = caughtDisc.GetComponent<DiscScript>();
        }
    }

    void OnTriggerEnter(Collider disc)
    {
        if (disc.tag == "Disc")
        {
            if (ableToCatch == true)
            {
                catchableDisc = disc.transform;
                discScript = catchableDisc.GetComponent<DiscScript>();
                discInBox = true;
            }
        }
    }

    void OnTriggerExit(Collider disc)
    {
        if (disc.tag == "Disc")
        {
            if (ableToCatch == true && holdingDisc == false)
            {
                catchableDisc = null;
                discScript = null;
                discInBox = false;
            }
        }
    }
}
