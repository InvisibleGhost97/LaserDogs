using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float rotationDamp = 5f;
    [SerializeField]
    float movementSpeed = 30f;

    [SerializeField]
    float rayCastOffsset = 2.5f;

    [SerializeField]
    float detectionDistance = 30.0f;

    void OnEnable()
    {
        EventManager.onPlayerDeath += FindMainCamera;
        EventManager.onStartGame += SelfDestruct;
    }

    void Disable()
    {
        EventManager.onPlayerDeath -= FindMainCamera;
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (!FindTarget())
        {
            return;
        }
        PathFinding();
        //Turn();
        Move();
    }

    void Turn()
    {
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationDamp * Time.deltaTime);
    }

    void Move()
    {
        transform.position += transform.forward * movementSpeed* Time.deltaTime;

    }

    void PathFinding()
    {
        RaycastHit hit;
        Vector3 _raycastOffset = Vector3.zero;

        Vector3 left = transform.position - transform.right* rayCastOffsset;
        Vector3 right = transform.position + transform.right * rayCastOffsset;
        Vector3 up = transform.position + transform.up * rayCastOffsset;
        Vector3 down = transform.position - transform.up * rayCastOffsset;

        Debug.DrawRay(left, transform.forward* detectionDistance, Color.cyan);
        Debug.DrawRay(right, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(up, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(down, transform.forward * detectionDistance, Color.cyan);

        if(Physics.Raycast(left, transform.forward, out hit, detectionDistance))
        {
            _raycastOffset += Vector3.right;
        }
        else if (Physics.Raycast(right, transform.forward, out hit, detectionDistance))
        {
            _raycastOffset -= Vector3.right;
        }
        if (Physics.Raycast(up, transform.forward, out hit, detectionDistance))
        {
            _raycastOffset -= Vector3.up;
        }
        else if (Physics.Raycast(down, transform.forward, out hit, detectionDistance))
        {
            _raycastOffset += Vector3.up;
        }

        if (_raycastOffset != Vector3.zero)
        {
            transform.Rotate(_raycastOffset * 5f * Time.deltaTime);
        }
        else
        {
            Turn();
        }
    }

    bool FindTarget()
    {
        if(target == null)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("Player");
            if (temp != null)
            {
                target = temp.transform;
            }

        }
        if (target == null)
        {
            return false;

        }
        return true;
    }

    void FindMainCamera()
    {
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;

    }

}