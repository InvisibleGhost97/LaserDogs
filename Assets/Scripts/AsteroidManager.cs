using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {
    
    [SerializeField]
    Asteroid asteroid;
    [SerializeField]
    GameObject pickupPrefab;
    [SerializeField]int numberofAsteroidsOnAnAxis = 10;
    [SerializeField]
    int gridSpacing = 100;

    public List<Asteroid> asteroidList = new List<Asteroid>();

    // Use this for initialization
    void Start () {
        //placeAsteroids();
	}

    void OnEnable()
    {
        EventManager.onStartGame += placeAsteroids;
        EventManager.onRespawnPickup += PlacePickUp;
    }

    void OnDisable()
    {
        EventManager.onStartGame -= placeAsteroids;
        EventManager.onRespawnPickup -= PlacePickUp;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void placeAsteroids()
    {
        for (int x = 0; x < numberofAsteroidsOnAnAxis; x++)
        {
            for (int y = 0; y < numberofAsteroidsOnAnAxis; y++)
            {
                for (int z = 0; z < numberofAsteroidsOnAnAxis; z++)
                {
                    InstantiateAsteroid(x, y, z);
                }
            }
        }
        PlacePickUp();
    }

    //void DestroyAsteroids()
    //{
    //    foreach (Asteroid ast in asteroidList)
    //    {
    //        ast.SelfDestruct();
    //    }
    //    asteroidList.Clear();
    //}

    void InstantiateAsteroid(int x, int y, int z)
    {
        Asteroid temp = Instantiate(asteroid, 
                    new Vector3( transform.position.x+(x * gridSpacing) + AsteroidOffset(),
                                 transform.position.y+(y * gridSpacing) + AsteroidOffset(),
                                 transform.position.z+(z * gridSpacing) + AsteroidOffset()), 
                    Quaternion.identity, 
                    transform) as Asteroid;

        temp.name = "Asteroid: " + x + "-" + y + "-"+ z;
        asteroidList.Add(temp);
    }

    void PlacePickUp()
    {
        int rnd = Random.Range(0, asteroidList.Count);


        Instantiate(pickupPrefab, asteroidList[rnd].transform.position, Quaternion.identity);

        Destroy(asteroidList[rnd].gameObject);
        asteroidList.RemoveAt(rnd);

    }

    float AsteroidOffset()
    {
        return Random.Range(-gridSpacing / 2f, gridSpacing / 2f);
    }
}
