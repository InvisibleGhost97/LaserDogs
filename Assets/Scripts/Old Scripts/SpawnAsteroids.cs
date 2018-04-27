using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour {

    public GameObject asteroidsPrefab;

    public Vector3 center;
    public Vector3 size;

	// Use this for initialization
	void Start () {
       // SpawnAsters();

    }
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    SpawnAsters();
        //}
	}

    public void SpawnAsters()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        Instantiate(asteroidsPrefab, pos, Quaternion.identity);
    }

    void onDrawGizmoSelected()
    {
        Gizmos.color = Color.red;
        // Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        Gizmos.DrawCube(center, size);
    }

}
