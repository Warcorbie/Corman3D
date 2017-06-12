using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpForce;
    public float slideForce;
    


    float moveX;
    Rigidbody p_rigidbody;
    Vector3 movement, jumping, slideing;
    CapsuleCollider col;
    SphereCollider kneelcol;

    List<int> ground;
    List<int> onKneel;
    bool isKneel = false;
    bool isFlipped;
    
    

	void Start ()
    {
        p_rigidbody = GetComponent<Rigidbody>();
        ground = new List<int>();
        onKneel = new List<int>();
        col = GetComponent<CapsuleCollider>();
        kneelcol = GetComponent<SphereCollider>();
	}
	
	// FixedUpdate weil Physik im Spiel ist
	void FixedUpdate ()
    {
        moveX = Input.GetAxis("Horizontal");
        if (onKneel.Count == 0)
        {                             //Nur Bewegbar wenn er nicht im knien ist
            Move(moveX);         
        }

        if (moveX == 1 || moveX == -1)          //Flip
        {
            Flip(moveX);
        }

     

        if (ground.Count >= 1)                              //Jeglicher Input der auf den Boden stattfindet
        {
            if (Input.GetButtonDown("Jump") && onKneel.Count == 0)
                Jump(jumpForce);

            if (Input.GetAxisRaw("Kneel") == 1 && isKneel == false)
            {                
              Kneel();
              isKneel = true;
              onKneel.Add(1);
            }

            if (Input.GetButtonDown("Jump") && onKneel.Count >= 1)
            {
                Slide(slideForce);
            }
        }

        if (Input.GetAxisRaw("Kneel") == 0 && isKneel == true)  //Knie Input
        {
            Kneel();
            isKneel = false;
            onKneel.Clear();
        }
  
	}

    void Kneel()                // Kneel function
    {

        col.enabled = !col.enabled;
        kneelcol.enabled = !kneelcol.enabled;
     
        Debug.Log(col.enabled);
        Debug.Log(kneelcol.enabled);
    }

    void Slide(float x)         // Kneel Action Slide function
    {
        slideing.Set(x, 0.0f, 0.0f);
        slideing = slideing * slideForce * Time.deltaTime;
        p_rigidbody.AddForce(slideing);
    }

    void Move(float x)          // Movement function
    {
        movement.Set(x, 0.0f, 0.0f);
        movement = movement.normalized * speed * Time.deltaTime;
        p_rigidbody.MovePosition(p_rigidbody.position + movement);
    }

    void Jump(float y)          // Jump function
    {
            jumping.Set(0.0f, y, 0.0f);
            jumping = jumping * jumpForce * Time.deltaTime;
            p_rigidbody.AddForce(jumping);
            ground.Clear();
    }

    void Flip(float x)              // Character Flip
    {
        Vector3 angle = new Vector3(0.0f, x * 90, 0.0f);
        Quaternion delta = Quaternion.Euler(angle);
        p_rigidbody.MoveRotation(delta);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")           // If any gameobject enters the collider of gameoject with tag "Ground"
        {          
                ground.Add(1);
            
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")       // If any gameobject leaves the collider of gameoject with tag "Ground"
        {

                ground.Remove(1);
                

        }
    }


}
