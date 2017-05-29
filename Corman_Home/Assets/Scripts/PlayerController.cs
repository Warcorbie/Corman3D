using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpForce;
    public GameObject objCheck;

    float moveX;
    Rigidbody p_rigidbody;
    Vector3 movement, jumping;

    bool grounded;
    bool isJumping;
    
    

	void Start () {
        p_rigidbody = GetComponent<Rigidbody>();
        
	}
	
	// FixedUpdate weil Physik im Spiel ist
	void FixedUpdate () {

        //OnCollisionEnter();
   
        moveX = Input.GetAxis("Horizontal");
        Move(moveX);


        if (Input.GetButtonDown("Jump"))
            Jump(jumpForce);
	}


    void Move(float x)
    {
        movement.Set(x, 0.0f, 0.0f);
        movement = movement * speed * Time.deltaTime;
        p_rigidbody.MovePosition(transform.position + movement);
    }

    void Jump(float y)
    {
        if (grounded == true )
        { 
            jumping.Set(0.0f, y, 0.0f);
            jumping = jumping * jumpForce * Time.deltaTime;
            p_rigidbody.AddForce(transform.position + jumping);
            //isJumping = false;
        }
        //else
        //{
        //    isJumping = true;
        //}

    }

    void OnCollisionEnter(Collision collision)
    {
      if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
            Debug.Log("Grounded True");
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
            Debug.Log("Grounded false");
        }
    }
}
