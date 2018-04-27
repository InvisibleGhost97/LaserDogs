﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVelocity : MonoBehaviour {

    public float speed;
    Rigidbody rigidbody;


    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * speed;	
       // GetComponent<RigidBody>().velocity = transform.forward * speed;
    }
	
}