using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController playerController;
    [SerializeField] float movementSpeed = 4f;
    [SerializeField] float h;
    [SerializeField] float v;

    Vector3 moveVector = new Vector3();
    //RequireComponent(typeof)

    // Use this for initialization
    void Start ()
    {
        playerController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        MovePlayer();
	}

    void MovePlayer()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        moveVector.z = v;
        moveVector.x = h;
        playerController.Move(moveVector * movementSpeed * Time.deltaTime);
        //transform.Translate(moveVector * movementSpeed * Time.deltaTime);
    }
}
