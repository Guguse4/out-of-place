using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int numberOfMimicFound = 0;
    public int numberOfHP = 3;
    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(numberOfHP <= 0)
        {
            isGameOver = true;
        }
    }

    public bool GetIsGameOver()
    {
        return isGameOver;
    }
}
