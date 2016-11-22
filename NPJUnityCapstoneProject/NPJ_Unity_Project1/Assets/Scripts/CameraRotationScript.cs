using UnityEngine;
using System.Collections;

public class CameraRotationScript : MonoBehaviour
{
    [SerializeField] Transform anchor;
    [SerializeField] Quaternion rotationToTarget;
    [SerializeField] bool lookAtPoint = false;
    [SerializeField] float sensitivity = 3;

    [SerializeField] float maxManualDegrees;
     

    //[SerializeField]
    //Transform targetEnemy;

	// Use this for initialization
	void Start ()
    {
        //SetTargetRotation(targetEnemy);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (lookAtPoint == true)
        {
            anchor.rotation = Quaternion.Slerp(anchor.rotation, rotationToTarget, .4f);
        }
	}

    public void SetTargetRotation (Vector3 target)
    {
        //Debug.Log("RotationTarget: " + target.name);
        //Vector3 directionToTarget = target.localPosition - anchor.localPosition;
        Vector3 directionToTarget = Vector3.MoveTowards(anchor.position, target, 1f);
        rotationToTarget = Quaternion.FromToRotation(anchor.position, directionToTarget);
    }

    /*float ClampAngleCorrectly(float angleToClamp, float minAngle, float maxAngle)
    {
        Debug.Log(angleToClamp);
        if (angleToClamp >= 340)
        {
            angleToClamp -= 360;
        }
        Debug.Log("Corrected Angle: " + angleToClamp);
        return Mathf.Clamp(angleToClamp, minAngle, maxAngle);
    }*/

    public void ManualRotate (Vector3 rotateVector)
    {

        //anchor.Rotate((rotateVector * maxManualDegrees * Time.deltaTime));
        //anchor.localEulerAngles = new Vector3(ClampAngleCorrectly(anchor.localRotation.eulerAngles.x, -20f, 20f), anchor.localRotation.eulerAngles.y, anchor.localRotation.eulerAngles.z);
        //Debug.Log("Actual angle: " + anchor.localEulerAngles.x);
    }
}
