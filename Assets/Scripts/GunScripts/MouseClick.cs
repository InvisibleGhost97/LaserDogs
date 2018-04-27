using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using System;

public class MouseClick : NetworkBehaviour {

    [SerializeField]
    GameObject shotParticles;


    [SerializeField]
    GameObject muzzleEffect;

   
   
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame

    

    void OnMouseDown()
    {
        Instantiate(muzzleEffect, gameObject.transform.position, gameObject.transform.rotation);

        Instantiate(shotParticles, gameObject.transform.position, gameObject.transform.rotation);
    }

}
