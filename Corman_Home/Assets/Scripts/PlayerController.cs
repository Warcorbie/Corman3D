using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpForce;

    float moveX;
    Rigidbody p_rigidbody;
    Vector3 movement, jumping;

    List<int> ground;
    
    

	void Start ()
    {
        p_rigidbody = GetComponent<Rigidbody>();
        ground = new List<int>();        
	}
	
	// FixedUpdate weil Physik im Spiel ist
	void FixedUpdate ()
    {
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
        if (ground.Count >= 1)
        { 
            jumping.Set(0.0f, y, 0.0f);
            jumping = jumping * jumpForce * Time.deltaTime;
            p_rigidbody.AddForce(transform.position + jumping);
           
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {          
                ground.Add(1);
                Debug.Log("Jump");            
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

                ground.Remove(1);
                Debug.Log("notJump");

        }
    }


}
