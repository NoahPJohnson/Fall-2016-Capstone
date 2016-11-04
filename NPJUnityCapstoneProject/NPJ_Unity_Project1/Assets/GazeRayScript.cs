using UnityEngine;
using System.Collections;

public class GazeRayScript : MonoBehaviour
{
    [SerializeField] GameObject inputObject;
    RaycastScript rayScript;

    void Start()
    {
        rayScript = inputObject.GetComponent<RaycastScript>();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Enemy")
        {
            rayScript.SetTarget(other.gameObject.transform);
        }
    }
    
    void OnTriggerExit (Collider other)
    {
        if (other.tag == "Enemy")
        {
            rayScript.LookAway();
        }
    }
}
