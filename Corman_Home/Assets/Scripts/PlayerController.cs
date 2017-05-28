using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpForce;

    float moveX;
    Rigidbody p_rigidbody;
    Vector3 movement;

    bool grounded;
    Transform groundCheck;
    bool isJumping;

	void Start () {
        p_rigidbody = GetComponent<Rigidbody>();
        
	}
	
	// FixedUpdate weil Physik im Spiel ist
	void FixedUpdate () {
        //grounded = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.GetMask("Ground"));
        //if (grounded == true)
        //    Debug.Log("isGrounded");

        moveX = Input.GetAxis("Horizontal") * speed;
        Move(moveX);
	}


    void Move(float x)
    {
        movement.Set(x, 0.0f, 0.0f);
        movement = movement * speed * Time.deltaTime;
        p_rigidbody.MovePosition(transform.position + movement);
    }

    void Jump()
    {

    }
}
