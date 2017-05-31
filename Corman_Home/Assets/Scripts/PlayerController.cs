using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpForce;


    float kneelFaktor;
    float moveX;
    Rigidbody p_rigidbody;
    Vector3 movement, jumping;
    CapsuleCollider col;
    

    List<int> ground;
    
    

	void Start ()
    {
        p_rigidbody = GetComponent<Rigidbody>();
        ground = new List<int>();
        col = GetComponent<CapsuleCollider>();
        kneelFaktor = 2f;
	}
	
	// FixedUpdate weil Physik im Spiel ist
	void FixedUpdate ()
    {
        moveX = Input.GetAxis("Horizontal");
        Move();


        if (Input.GetButtonDown("Jump"))
            Jump(jumpForce);

        if (ground.Count >= 1)
        {
            if (Input.GetButtonDown("Kneel"))
            {                
                    KneelDown();
            }
        }
        if (Input.GetButtonUp("Kneel"))
        {         
            KneelUp();
        }
	}

    void KneelDown()
    {
        if (col.height >= 1 && col.height <= 2)
            col.height = col.height / kneelFaktor;

        Debug.Log(col.height);
    }
    void KneelUp()
    {
        if (col.height >= 1 && col.height <= 2)
            col.height = col.height * kneelFaktor;
        Debug.Log(col.height);
    }

    void Move()
    {
        movement = new Vector3(moveX * speed * Time.deltaTime, 0.0f, 0.0f);
        p_rigidbody.MovePosition(p_rigidbody.position + movement);
    }

    void Jump(float y)
    {
        if (ground.Count >= 1)
        { 
            jumping.Set(0.0f, y, 0.0f);
            jumping = jumping * jumpForce * Time.deltaTime;
            p_rigidbody.AddForce(transform.position + jumping);
            ground.Clear();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {          
                ground.Add(1);
            Debug.Log(col.height);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

                ground.Remove(1);
                

        }
    }


}
