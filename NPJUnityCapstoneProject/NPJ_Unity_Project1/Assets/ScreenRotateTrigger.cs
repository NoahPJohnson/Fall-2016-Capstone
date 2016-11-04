using UnityEngine;
using System.Collections;

public class ScreenRotateTrigger : MonoBehaviour
{
    [SerializeField] bool left;
    [SerializeField] bool right;
    [SerializeField] bool up;
    [SerializeField] bool down;
    [SerializeField] Vector3 designatedRotation;

    // Use this for initialization
    void Start ()
    {
	    if (left == true)
        {
            designatedRotation = Vector3.left;
        }
        else if (right == true)
        {
            designatedRotation = Vector3.right;
        }
        if (up == true)
        {
            designatedRotation = Vector3.up;
        }
        else if (down == true)
        {
            designatedRotation = Vector3.down;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
