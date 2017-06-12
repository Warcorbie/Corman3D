using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpForce;
    public float slideForce;


    float moveX;
    Rigidbody p_rigidbody;
    Vector3 movement, jumping, slideing, velocity, lastPosition;
    CapsuleCollider col;
    SphereCollider kneelcol;
    

    List<int> ground;
    List<int> onKneel;
    bool isKneel = false;
    
    

	void Start ()
    {
        p_rigidbody = GetComponent<Rigidbody>();
        ground = new List<int>();
        onKneel = new List<int>();
        col = GetComponent<CapsuleCollider>();
        kneelcol = GetComponent<SphereCollider>();
        
	}
	void Update()
    {
        velocity.x = ((p_rigidbody.position - lastPosition).magnitude / Time.deltaTime);
        lastPosition = p_rigidbody.position;
    }
	// FixedUpdate weil Physik im Spiel ist
	void FixedUpdate ()
    {
        moveX = Input.GetAxis("Horizontal");
        if (onKneel.Count == 0)
        {
            Move(moveX, velocity.x);
            Debug.Log(velocity.x);
        }


        if (ground.Count >= 1)
        {
            if (Input.GetButtonDown("Jump") && onKneel.Count == 0)
                Jump(jumpForce, velocity.x);

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
        if (Input.GetAxisRaw("Kneel") == 0 && isKneel == true)
        {
            Kneel();
            isKneel = false;
            onKneel.Clear();
        }
  
	}

    void Kneel()
    {

        col.enabled = !col.enabled;
        kneelcol.enabled = !kneelcol.enabled;
    }

    void Slide(float x)
    {
        slideing.Set(x, 0.0f, 0.0f);
        slideing = slideing * slideForce * Time.deltaTime;
        p_rigidbody.AddForce(slideing);
    }

    void Move(float x, float xX)
    {
        movement.Set(x, 0.0f, 0.0f);
        movement = movement.normalized * speed * Time.deltaTime;
        p_rigidbody.MovePosition(p_rigidbody.position + movement);
    }

    void Jump(float y, float x)
    {
            jumping.Set(0.0f, y, 0.0f);
            jumping = jumping * jumpForce * Time.deltaTime;
            p_rigidbody.AddForce(jumping);
            ground.Clear();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {          
                ground.Add(1);
            
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
