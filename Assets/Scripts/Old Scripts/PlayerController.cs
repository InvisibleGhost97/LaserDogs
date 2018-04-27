//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    [Header("Movement variables: ")]
    [SerializeField] float movementSpeed = 5.0f; //speed of the object
    [SerializeField] float turnSpeed = 45.0f; //turn speed of the object
    [Header("Camera Position variables: ")]
    [SerializeField] float cameraDistance = 16f; //distance from the player that the camera should be
    [SerializeField] float cameraheight = 16f;   //the height off of the ground that the camera should be

    Rigidbody localRigidBody;   //cache the reference to the rigidbocy
    Transform mainCamera;       //ref. to the scen'es main camera
    Vector3 cameraOffset;       //a vec3 containing how far back and up the camera should be form the player

    // Use this for initialization
	void Start () {

        //if this tank is not the local player...
        if (!isLocalPlayer)
        {
            //...then remove this script. this is important becasue this tank (not the local player)
            //won't need to control the camera, move on its own, or really do anythin interactive.
            //with the script removed, all of the rest of this code will not be run 
            Destroy(this);
            return;
        }


        //get the ref to the object's rigidbody since we will be using it a lot
        localRigidBody = GetComponent<Rigidbody>();

        //set up the camera offset for future use
        cameraOffset = new Vector3(0f, cameraheight, -cameraDistance);

        //find the main scene camera and move it into the correct position
        mainCamera = Camera.main.transform;
        MoveCamera();


	}

    void Update()
    {
        Turn();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //get the horizontal and vertical input. note theat we can get inpput for any platform using the CrossPlatforminput class
        float turnAmount = CrossPlatformInputManager.GetAxis("Horizontal");
        float moveAmount = CrossPlatformInputManager.GetAxis("Vertical");

        //Calculate and apply the new position
        Vector3 deltaTranslation = transform.position + transform.forward * movementSpeed * moveAmount * Time.deltaTime;
        localRigidBody.MovePosition(deltaTranslation);

        //caculate and apply the new position
        Quaternion deltaRotation = Quaternion.Euler(turnSpeed * new Vector3(0, turnAmount, 0) * Time.deltaTime);
        localRigidBody.MoveRotation(localRigidBody.rotation * deltaRotation);

        //update the camer'a position
        MoveCamera();
        
    }

    void MoveCamera()
    {
        mainCamera.position = transform.position;   //position the camera on the dog
        mainCamera.rotation = transform.rotation;   //align the camera with the dog
        mainCamera.Translate(cameraOffset);         //move the camera up and away from the tank
        mainCamera.LookAt(transform);               //make the camera look at the dog
    }

    void Turn()
    {
        float yaw = turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float pitch = turnSpeed * Time.deltaTime * Input.GetAxis("Pitch");
        float roll = turnSpeed * Time.deltaTime * Input.GetAxis("Roll");


        transform.Rotate(-pitch, yaw, -roll);
        //input *turn speed *Time
    }

}
