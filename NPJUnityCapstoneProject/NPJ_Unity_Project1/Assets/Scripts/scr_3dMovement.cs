using UnityEngine;
using System.Collections;

public class scr_3dMovement : MonoBehaviour {

    float speed = 6f;

    Rigidbody playerRB;
    Vector3 moveVector;

	// Use this for initialization
	void Awake ()
    {
        playerRB = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        //float y = Input.GetAxisRaw("Special");
        Movement(x, 0, z);
    }

    void Movement(float x, float y, float z)
    {
        moveVector.Set(x, 0, z);

        moveVector = moveVector.normalized * speed * Time.deltaTime;

        playerRB.MovePosition(transform.position + moveVector);
    }
}
