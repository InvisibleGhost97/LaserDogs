using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    GameObject gameUI;

    //bool isDisplay = true;
    //[SerializeField]
    //GameObject playButton;

    //[SerializeField]
    //GameObject playerPrefab;
    //[SerializeField]
    //GameObject playerStartPos;

    void Start()
    {
        ShowMainMenu();
    }

    void OnEnable()
    {
        EventManager.onStartGame += ShowGameUI;
        EventManager.onPlayerDeath += ShowMainMenu;
    }

    void OnDisable()
    {
        EventManager.onStartGame -= ShowGameUI;
        EventManager.onPlayerDeath -= ShowMainMenu;
    }

    void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        gameUI.SetActive(false);

    }

    void ShowGameUI()
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(true);

        //Instantiate(playerPrefab, playerStartPos.transform.position, playerStartPos.transform.rotation);

    }

    //void HidePanel()
    //{
    //    isDisplay = !isDisplay;
    //    playButton.SetActive(isDisplay);


    //}


}
