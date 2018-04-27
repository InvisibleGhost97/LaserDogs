using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float movementSpeed = 50f;

    [SerializeField]
    float turnSpeed = 60f;

    [Header("Camera Position variables: ")]
    [SerializeField]
    float cameraDistance = 16f; //distance from the player that the camera should be
    [SerializeField]
    float cameraheight = 16f;   //the height off of the ground that the camera should be

    Rigidbody localRigidBody;   //cache the reference to the rigidbocy
    Transform mainCamera;       //ref. to the scenes main camera
    Vector3 cameraOffset;       //a vec3 containing how far back and up the camera should be form the player


    Transform myT;

    [SerializeField]
    Vector3 defaultDistance = new Vector3(0f, 2f, -25f);

    [SerializeField]
    float distanceDamp = 1.5f;

    public Vector3 velocity = Vector3.one;


    void Awake()
    {
        myT = transform;
    }

    // Use this for initialization
    void Start()
    {
        //get the ref to the object's rigidbody since we will be using it a lot
        localRigidBody = GetComponent<Rigidbody>();

        //set up the camera offset for future use
        cameraOffset = new Vector3(0f, cameraheight, -cameraDistance);

        //find the main scene camera and move it into the correct position
        mainCamera = Camera.main.transform;
        MoveCamera();
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
        Thrust();
        MoveCamera();
        //SmoothFollow();

    }

    void Turn()
    {
        float yaw = turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float pitch = turnSpeed * Time.deltaTime * Input.GetAxis("Pitch");
        float roll = turnSpeed * Time.deltaTime * Input.GetAxis("Roll");


        myT.Rotate(-pitch, yaw, -roll);
        //input *turn speed *Time
    }

    void Thrust()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            myT.position += myT.forward * movementSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        }
    }

    void SmoothFollow()
    {
        Vector3 toPos = transform.position + (transform.rotation * defaultDistance);
        Vector3 curPos = Vector3.SmoothDamp(myT.position, toPos, ref velocity, distanceDamp);
        myT.position = curPos;

        myT.LookAt(transform, transform.up);
    }

    void MoveCamera()
    {
        mainCamera.position = transform.position;   //position the camera on the dog
        mainCamera.rotation = transform.rotation;   //align the camera with the dog
        mainCamera.Translate(cameraOffset);         //move the camera up and away from the tank
        mainCamera.LookAt(transform);               //make the camera look at the dog
    }

}
