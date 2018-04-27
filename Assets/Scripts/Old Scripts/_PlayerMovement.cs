using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerMovement : MonoBehaviour {

    [Header("Movement variables: ")]
    [SerializeField] float movementSpeed = 50f;
    [SerializeField] float turnSpeed = 60f;

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

    public float tilt;

   // public GameObject shot;
   // public Transform shotSpawn;
    public float fireRate = 1.5f;

    private float nextFire = 0.0f;

    
    public float rotationSpeed = 100.0f;


    float pitch, yaw;
    bool canFire;


    [SerializeField]
    float maxDistance = 500f;



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
        //cameraOffset = new Vector3(0f, cameraheight, -cameraDistance);

        //find the main scene camera and move it into the correct position
        mainCamera = Camera.main.transform;

       // Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

}

// Update is called once per frame
void Update () {
        //Turn();
        Thrust();


        FireLaser();

        //turning left
        //if (Input.mousePosition.x <= (Screen.width / 2))
        //{
        //    float horizontalPosition = turnSpeed* Time.deltaTime* Input.GetAxis("Horizontal");
        //}

       
       // mousePos.z = 10;

        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetMouseButton(1))
        {
            //Debug.Log("Going to turn");
            if (Input.mousePosition.x < (Screen.width / 3))
            {
                //Debug.Log("Turningleft");
                 pitch = turnSpeed * Time.deltaTime;
                myT.Rotate(0, -pitch, 0);
            }
            if (Input.mousePosition.x > ((Screen.width / 3))*2)
            {
                //Debug.Log("Turningright");
                 pitch = turnSpeed * Time.deltaTime;
                myT.Rotate(0, pitch, 0);
            }
            //if (Input.mousePosition.x < (Screen.width / 3) && Input.mousePosition.x > (Screen.width / 3))
            //{
            //    pitch = 0;
            //    myT.Rotate(0, pitch, 0);
            //}
            //up
            if (Input.mousePosition.y <= (Screen.height / 3))
            {
                //Debug.Log("going down");
                yaw = turnSpeed * Time.deltaTime;
                myT.Rotate(yaw, 0, 0);
            }

            //turning down
            if (Input.mousePosition.y >= ((Screen.height / 3))*2)
            {
                yaw = turnSpeed * Time.deltaTime;
                myT.Rotate(-yaw, 0, 0);
            }
        }
       

        

    }

    void LateUpdate()
    {
       //tw SmoothFollow();
    }
    void FixedUpdate()
    {
        // MoveCamera();
       // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

       // transform.rotation = rot;
       // transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        //localRigidBody.angularVelocity = 0;


    }

    void Turn()
    {
        //float yaw = turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        

        float pitch = turnSpeed * Time.deltaTime * Input.GetAxis("Pitch");
        float roll = turnSpeed * Time.deltaTime * Input.GetAxis("Roll");


        //myT.Rotate(-pitch, yaw, -roll);

       // localRigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, localRigidBody.velocity.x * -tilt);

        //input *turn speed *Time
    }

    void Thrust()
    {
        if (Input.GetAxis("Vertical")> 0)
        {
            myT.position += myT.forward * movementSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        }
        //else
        //{
        //    this.transform
        //}
    }

    void SmoothFollow()
    {
        Vector3 toPos = mainCamera.position + (mainCamera.rotation * defaultDistance);
        Vector3 curPos = Vector3.SmoothDamp(myT.position, toPos, ref velocity, distanceDamp);
        myT.position = curPos;

        myT.LookAt(mainCamera, mainCamera.up);
    }

    void MoveCamera()
    {
        mainCamera.position = transform.position;   //position the camera on the dog
        mainCamera.rotation = transform.rotation;   //align the camera with the dog
        mainCamera.Translate(cameraOffset);         //move the camera up and away from the tank
        mainCamera.LookAt(transform);               //make the camera look at the dog
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Asteroid")
        {
           // myT.position = 0;
        }
    }

    void CanFire()
    {
        canFire = true;
    }


    public void FireLaser()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, Color.yellow);
            CastRay();
            nextFire = Time.time + fireRate;
           // Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //canFire = false;
            
        }

    }

    Vector3 CastRay()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward) * maxDistance;

        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            Debug.Log("We hit: " + hit.transform.name);

            Explosion temp = hit.transform.GetComponent<Explosion>();
            if (temp != null)
            {
                temp.HittiedMe(hit.point);
            }

            //hit.transform.GetComponent<Explosion>().HittiedMe(hit.point);
            return hit.point;

        }
        Debug.Log("We missed");
        return transform.position + (transform.forward * maxDistance);
    }

}
