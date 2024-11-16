using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsControl : MonoBehaviour
{
    [SerializeField] GameObject[] tabLights;
    [SerializeField] private float timerBetweenState = 30f;
    [SerializeField] private bool isLightOn = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timerBetweenState < 0)
        {
            timerBetweenState = 30f;

            if(isLightOn)
            {
                isLightOn = false;
                for (int i = 0; i < tabLights.Length; i++)
                {
                    tabLights[i].SetActive(false);
                }
            }
            else
            {
                isLightOn = true;
                for (int i = 0; i < tabLights.Length; i++)
                {
                    tabLights[i].SetActive(true);
                }
            }
        }
        else 
        {
            timerBetweenState -= Time.deltaTime;
        }
    }
}
