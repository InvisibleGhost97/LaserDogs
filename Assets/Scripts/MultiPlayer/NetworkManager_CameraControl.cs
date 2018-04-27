using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Networking.NetworkSystem;

//this script causes the camera to rotate around the scene before the game starts
//it is meant to demonstrate how to expand the NetworkManager

public class NetworkManager_CameraControl : NetworkManager {

    [Header("Scene Camera Properties")]
    [SerializeField]
    Transform sceneCamera;
    [SerializeField]
    float cameraRotationRadius = 24f;
    [SerializeField]
    float cameraRotationSpeed = 3f;
    [SerializeField]
    bool canRotate = true;

    float rotation; //current rotation of the camera

    [Header("Character Selection")]
    public Button player1Button;
    public Button player2Button;
    public Button player3Button;

    int avatarIndex = 0;

    [Header("Canvas Element")]
    public Canvas characterSelectionCanvas;

    void Start()
    {
        player1Button.onClick.AddListener(delegate { AvatarPicker(player1Button.name); });
        player2Button.onClick.AddListener(delegate { AvatarPicker(player2Button.name); });
        player3Button.onClick.AddListener(delegate { AvatarPicker(player3Button.name); });
    }

    void AvatarPicker(string buttonName)
    {
        switch (buttonName)
        {
            case "Player1":
                avatarIndex = 1;
                break;
            case "Player2":
                avatarIndex = 2;
                break;
            case "Player3":
                avatarIndex = 3;
                break;

        }
        playerPrefab = spawnPrefabs[avatarIndex];
    }

    /// Copied from Unity's original NetworkManager script except where noted
    public override void OnClientConnect(NetworkConnection conn)
    {
        /// ***
        /// This is added:
        /// First, turn off the canvas...
        characterSelectionCanvas.enabled = false;
        /// Can't directly send an int variable to 'addPlayer()' so you have to use a message service...
        IntegerMessage msg = new IntegerMessage(avatarIndex);
        /// ***

        if (!clientLoadedScene)
        {
            // Ready/AddPlayer is usually triggered by a scene load completing. if no scene was loaded, then Ready/AddPlayer it here instead.
            ClientScene.Ready(conn);
            if (autoCreatePlayer)
            {
                /// This is changed - the original calls a differnet version of addPlayer this calls a version that allows a message to be sent
                ClientScene.AddPlayer(conn, 0, msg);
            }
        }

    }

    /// Copied from Unity's original NetworkManager 'OnServerAddPlayerInternal' script except where noted
    /// Since OnServerAddPlayer calls OnServerAddPlayerInternal and needs to pass the message - just add it all into one.
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        /// *** additions
        /// I skipped all the debug messages...
        /// This is added to recieve the message from addPlayer()...
        int id = 0;

        if (extraMessageReader != null)
        {
            IntegerMessage i = extraMessageReader.ReadMessage<IntegerMessage>();
            id = i.value;
        }

        /// using the sent message - pick the correct prefab
        GameObject playerPrefab = spawnPrefabs[id];
        /// *** end of additions

        GameObject player;
        Transform startPos = GetStartPosition();
        if (startPos != null)
        {
            player = (GameObject)Instantiate(playerPrefab, startPos.position, startPos.rotation);
        }
        else
        {
            player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public override void OnStartClient(NetworkClient client)
    {
        canRotate = false;
    }

    public override void OnStartHost()
    {
        canRotate = false;
    }

    public override void OnStopClient()
    {
        canRotate = true;
    }

    public override void OnStopHost()
    {
        canRotate = true;
    }
	
	// Update is called once per frame
	void Update () {
        //if we can't rotate, leave
        if (!canRotate)
            return;

        //calculate the new rotation and make sure it isn't larger than 360 degrees
        rotation += cameraRotationSpeed * Time.deltaTime;
        if (rotation >= 360f)
            rotation -= 360f;

        //rotate the camera around the center of the scene
        sceneCamera.position = Vector3.zero;
        sceneCamera.rotation = Quaternion.Euler(0f, rotation, 0f);
        sceneCamera.Translate(0f, cameraRotationRadius, -cameraRotationRadius);
        sceneCamera.LookAt(Vector3.zero);
	}
}
