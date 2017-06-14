using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour {

    public float speed;

    Rigidbody b_rigidbody;

	void Start ()
    {
        b_rigidbody = GetComponent<Rigidbody>();
        
    }

	void Update ()
    {
        b_rigidbody.velocity = transform.forward * speed;
    }
}
