using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shield : MonoBehaviour {
    [SerializeField]
    int maxHealth = 10;
    [SerializeField]
    int curHealth;
    [SerializeField]
    int regenerateAmount = 1;
    [SerializeField]
    float regenerationRate = 2f;

    void Start()
    {
        curHealth = maxHealth;
        InvokeRepeating("Regenerate", regenerationRate, regenerationRate);

    }

    void Regenerate()
    {
        if (curHealth< maxHealth)
        {
            curHealth += regenerateAmount;
        }

        if(curHealth >maxHealth)
        {
            curHealth = maxHealth;

        }

        EventManager.TakeDamage(curHealth / (float)maxHealth);
        
    }


    public void TakeDamage(int dmg = 1)
    {
        curHealth -= dmg;

        if (curHealth < 0)
        {
            curHealth = 0;
        }

        EventManager.TakeDamage(curHealth/(float)maxHealth);
        if (curHealth < 1)
        {
            GetComponent<Explosion>().BlowUp();
            //go back to main menu
            SceneManager.LoadScene("GameOver");

            //Debug.Log("i'm dead!");
            //remove a life
        }
    }
}
