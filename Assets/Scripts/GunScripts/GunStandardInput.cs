using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStandardInput : MonoBehaviour {

    private GunMaster gunMaster;
    private float nextAttack;
    public float attackRate = 0.5f;
    private Transform myTranforms;
    public bool isAutomatic;
    public bool hasBurstFire;
    private bool isBurstFireActive;
    public string attackButtonName;
    public string reloadButtonName;
    public string burstFireButtonName;


	// Use this for initialization
	void Start () {
        SetInitialialReferences();
	}
	
	// Update is called once per frame
	void Update () {
        CheckIfWeaponShouldAttack();
        CheckForBurstFireToggle();
        CheckForReloadRequest();

	}

    void SetInitialialReferences()
    {
        gunMaster = GetComponent<GunMaster>();
        myTranforms = transform;
        gunMaster.isGunLoaded = true; // so the player can attempt to shooot right away

    }
     void CheckIfWeaponShouldAttack()
    {
        if (Time.time > nextAttack && Time.timeScale > 0)
        // && myTranforms.root.CompareTag(GameManager_References._playerTag))
        {
            if (isAutomatic && !isBurstFireActive)
            {
                if (Input.GetButton(attackButtonName))
                {
                    Debug.Log("Full Auto");
                    AttemptAttack();

                }
            }
            else if (isAutomatic && isBurstFireActive)
            {
                if (Input.GetButton(attackButtonName))
                {
                    Debug.Log("Burst");
                    StartCoroutine(RunBurstFire());

                }
            }

            else if (!isAutomatic)
            {
                if (Input.GetButton(attackButtonName))
                {
                   AttemptAttack();

                }
            }
        }

    }


    void AttemptAttack()
    {
        nextAttack = Time.time + attackRate;

        if (gunMaster.isGunLoaded)
        {
            Debug.Log("Shooting");
            gunMaster.CallEventPlayerInput();

        }
        else
        {
            gunMaster.CallEventGunNotUsable();
        }

    }

    void CheckForReloadRequest()
    {
        if (Input.GetButtonDown(reloadButtonName)&& Time.timeScale>0)
           // && myTranforms.root.CompareTag(GameManager_References._playerTag))
        {
            gunMaster.CallEventRequestreload();
        }
    }

    void CheckForBurstFireToggle()
    {
        if (Input.GetButtonDown(burstFireButtonName) && Time.timeScale > 0 )
            //&& myTranforms.root.CompareTag(GameManager_References._playerTag))
        {
            Debug.Log("BurstFire Toggled");
            isBurstFireActive = !isBurstFireActive;
            gunMaster.CallEventToggleBurstFire();
        }
    }

    IEnumerator RunBurstFire()
    {
        AttemptAttack();
        yield return new WaitForSeconds(attackRate);
        AttemptAttack();
        yield return new WaitForSeconds(attackRate);
        AttemptAttack();
    }

}
