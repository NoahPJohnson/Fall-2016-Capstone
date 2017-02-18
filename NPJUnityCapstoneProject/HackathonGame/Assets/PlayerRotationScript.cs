using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationScript : MonoBehaviour
{
    [SerializeField] Camera MainCamera;
    [SerializeField] float rotationX;
    [SerializeField] float rotationY;
    [SerializeField] Vector3 mouseVector;
    [SerializeField] Quaternion rotationQuaternion;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //mouseVector.x = Input.GetAxis("Mouse X");
        //mouseVector.y = Input.GetAxis("Mouse Y");
        Vector3 MouseGlobalPosition = MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        transform.LookAt(MouseGlobalPosition);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
    }
}
