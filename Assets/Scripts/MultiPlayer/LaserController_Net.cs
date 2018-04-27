using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LaserController_Net : NetworkBehaviour {

    [SerializeField]
    float laserLifeTime = 2f;   //life of laser
    [SerializeField]
    bool canKill = false;       // can laser hurt player
    [SerializeField]
    bool isDeathMatch = false;  // is this a deathmatch game?

    bool isAlive = true;        // is the laser able to explode
    float age;                  // life laser has been alive
    ParticleSystem explosionPS; //laser explosion effect
    MeshRenderer laserRenderer; //laser model renderer

    // Use this for initialization
    void Start () {
        // get ref to components
       // explosionPS = GetComponentInChidlren<ParticleSystem>();
        laserRenderer = GetComponent<MeshRenderer>();

	}
	
	// Update is called once per frame
    //laser updated by the server
	void Update () {
        // if the laser was alve for too long
        age += Time.deltaTime;
        if (age > laserLifeTime)
        {
            //destroy on network
            NetworkServer.Destroy(gameObject);

        }
	}

    void OnCollision(Collision other)
    {
        //if the laser isn't alive then leave
        if (!isAlive)
        {
            return;

        }

        //the laser is going to explode and is no longer alive
        isAlive = false;

        //hide the laser body
        laserRenderer.enabled = false;
        //show explosion effect
        explosionPS.Play(true);

        //if this is not the server then leave. the above code doesn't need to be here since it only handle the graphics.
        //the below code deals with harming the enemies and need to be run on the server
        if (!isServer)
            return;

        //if laser didn't hit enemy then leave
        if (!canKill || other.gameObject.tag != "Player")
            return;

        if (isDeathMatch)
        {
            ////for DeathMatch
            ////get a ref to the hit object's health script and tell it to take a point of damage
            //DogHealth_DM health = other.gameObject.getComponent<DogHealth_DM>();
            //if (health != null)
            //    health.TakeDamage(1);
        }
        else
        {
            //get a ref to the hit object's health script and tell it to take a point of damage
            Health health = other.gameObject.GetComponent<Health>();
            Destroy(gameObject);
            if (health != null)
                health.TakeDamage(1);

        }
    }

}
