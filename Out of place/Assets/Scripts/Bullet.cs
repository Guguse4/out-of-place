using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mimic"))
        {
            playerManager.numberOfHP -= 1;
            Debug.Log("Vie restante : " + playerManager.numberOfHP);
        }
    }
}
