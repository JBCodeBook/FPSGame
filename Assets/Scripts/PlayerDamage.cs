using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    GameObject gameObject;
    public Canvas playerDamage;

    private void Awake()
    {
        gameObject = GameObject.Find("PlayerHitCanvas");
        if (gameObject != null)
        {
            playerDamage = gameObject.GetComponent<Canvas>();
        }
        else
        {
            Debug.Log("Could not find Canvas");
        }
    }
}
