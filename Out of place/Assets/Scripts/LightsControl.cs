using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsControl : MonoBehaviour
{
    [SerializeField] Material Defaut;
    [SerializeField] Material EmissiveLight;
    [SerializeField] Material EmissiveLightVeilleuse;
    [SerializeField] GameObject[] tabEtoile;
    [SerializeField] GameObject[] tabLight;
    [SerializeField] GameObject[] tabVeilleuse;
    //[SerializeField] GameObject Light;
    //[SerializeField] GameObject Veilleuse;
    [SerializeField] private float timerBetweenState = 10f;
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
            timerBetweenState = 10f;

            if (isLightOn)
            {
                isLightOn = false;
                for (int i = 0; i < tabEtoile.Length; i++)
                {
                    for (int y = 0; y < tabLight.Length; y++)
                    {
                        tabLight[y].SetActive(false);

                    }

                    tabEtoile[i].GetComponent<MeshRenderer>().material = EmissiveLight;
                    
                    if(tabEtoile[i].transform.GetChild(0) != null)
                    {
                        
                        tabEtoile[i].transform.GetChild(0).gameObject.SetActive(true);
                    }
                    for (int y = 0; y < tabVeilleuse.Length; y++)
                    {
                        tabVeilleuse[y].GetComponent<MeshRenderer>().material = Defaut;
                    }
                        
                }
            }
            else
            {
                isLightOn = true;
                for (int i = 0; i < tabEtoile.Length; i++)
                {
                    for (int y = 0; y < tabLight.Length; y++)
                    {
                        tabLight[y].SetActive(true);

                    }
                    tabEtoile[i].GetComponent<MeshRenderer>().material = Defaut;
                    for (int y = 0; y < tabVeilleuse.Length; y++)
                    {
                        tabVeilleuse[y].GetComponent<MeshRenderer>().material = EmissiveLightVeilleuse;
                    }
                    if (tabEtoile[i].transform.GetChild(0) != null)
                    {
                        
                        tabEtoile[i].transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
        }
        else 
        {
            timerBetweenState -= Time.deltaTime;
        }
    }
}
