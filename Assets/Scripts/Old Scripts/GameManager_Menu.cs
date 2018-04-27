using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine;

public class GameManager_Menu : MonoBehaviour {

    public static GameManager_Menu Instance { set; get; }

    public GameObject mainMenu;
    public GameObject serverMenu;
    public GameObject connectMenu;

    public GameObject serverPrefab;
    public GameObject clientPrefab;

	// Use this for initialization
	void Start () {
        Instance = this;
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        DontDestroyOnLoad(gameObject);
	}
	
    public void ConnectButton()
    {
        mainMenu.SetActive(false);
        connectMenu.SetActive(true);
    }

    public void HostButton()
    {
        mainMenu.SetActive(false);
        serverMenu.SetActive(true);
    }

    public void ConnectToServerButton()
    {
        string hostAddress =  GameObject.Find("HostInput").GetComponent<InputField>().text;

        if(hostAddress == "")
        {
            hostAddress = "127.0.0.1";

        }

        try
        {
          //  clientPrefab 
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }

    }

    public void BackButton()
    {
        serverMenu.SetActive(true);
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);

    }


    // Update is called once per frame
    void Update () {
		
	}
}
