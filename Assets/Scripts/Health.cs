using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Health : NetworkBehaviour {

    public int maxHealth = 3;       //player'smax health

    Text InfoText;                  // text info that will give information to the player
    int health;                     //player's current health

    // Use this for initialization
	void Start () {
        health = maxHealth;         //set player's health to max health being 3

	}
	

    public void TakeDamage(int amount)
    {
        //daamage will only be calculated on the serever. prevents a hacked client from cheating
        //also if tank is already dead you don't have to run the code anyomore
        if (!isServer ||health <= 0)
        {
            return;
        }
        health -= amount;

        //if the player is out of health
        if (health <= 0)
        {
            health = 0;

            //call a method on all instances of the object on all cllients
            RpcDied();

            //since the match is over the server will bring the players back to the lobby afetr 3 seconds
            Invoke("BackToLobby", 3f);

            //exit the method 
            return;

        }
    }

    [ClientRpc]
    void RpcDied()
    {
        //find the "game over" text object on the scene
        InfoText = GameObject.FindObjectOfType<Text>();

        //if the tank died and is the local player, that means they lost 
        //otherwise they didn't die and thus won 

        if (isLocalPlayer)
        {

            InfoText.text = "Game Over";

        }
        else
        {
            InfoText.text = "You Won!";
        }
    }

    void BackToLobby()
    {
        //go back to the lobby 
        FindObjectOfType<NetworkLobbyManager>().ServerReturnToLobby();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
