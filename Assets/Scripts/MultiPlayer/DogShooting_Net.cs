using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class DogShooting_Net : NetworkBehaviour {

    [SerializeField]
    float power = 800f;
    [SerializeField]
    GameObject laserPrefab;
    [SerializeField]
    Transform laserEyes;


    //won't affect the player at runtime
    void Reset()
    {
        laserEyes = transform.Find("LaserEyes");
    }

	// Update is called once per frame
	void Update () {
        //if not the local player then leave. this script cannot be removed b/c this script has Command,
        //CmdSpawnLaser, that needs to exist on this object even if it's not the local player

        if (!isLocalPlayer)
        {
            return;
        }

        //if mouse clicked, screen touched, or spacebar hit...
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            //run serever command to spawn laser
            CmdSpawnLasers();
        }
		
	}

    //called from the locaPlayer and run on the server
    [Command]
    void CmdSpawnLasers()
    {
        //init a laer at the laser eyes position with the correct rotation
        GameObject instance = Instantiate(laserPrefab, laserEyes.position, laserEyes.rotation) as GameObject;
        //find rigidbody component of laser and add forward forseto it
        instance.GetComponent<Rigidbody>().AddForce(laserEyes.forward * power);

        //init object for all players to see
        NetworkServer.Spawn (instance);
    }
}
