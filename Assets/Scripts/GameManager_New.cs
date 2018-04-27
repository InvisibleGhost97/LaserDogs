using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_New : MonoBehaviour
{

    //public static GameManager_Menu Instance { set; get; }

    //public GameObject mainMenu;
    //public string Level1;
    //public GameObject serverMenu;
    //public GameObject connectMenu;

    //public GameObject serverPrefab;
    //public GameObject clientPrefab;

    //// Use this for initialization
    //void Start()
    //{
    //    //Instance = this;
    //    //serverMenu.SetActive(false);
    //    //connectMenu.SetActive(false);
    //    DontDestroyOnLoad(gameObject);
    //}

    public void ConnectButton()
    {
        //Debug.Log("yeah your hitting me");
        SceneManager.LoadScene("ModeSelection");

        // mainMenu.SetActive(false);
        //connectMenu.SetActive(true);
    }

    public void HelpButton()
    {
        //Debug.Log("yeah your hitting me");
        SceneManager.LoadScene("Instructions");


    }
    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");


    }

    public void CharacterSelection()
    {
        SceneManager.LoadScene("CharacterSelection");


    }

    public void MultiPlayerMode()
    {
        SceneManager.LoadScene("Level1");


    }

    public void ReturnToMainMenu()
    {  SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        SceneManager.LoadScene("MainMenu");
      
    }

    public void quitButton()
    {
        Debug.Log("The Application has been quit");
        Application.Quit();
    }

    


   
}
