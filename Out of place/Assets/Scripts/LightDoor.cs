using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightDoor : MonoBehaviour
{
    [SerializeField] private GameObject mimic;
    [SerializeField] private Material lightOnMaterial;
    [SerializeField] private GameObject pointLight;
    // Start is called before the first frame update
    void Start()
    {
        if (mimic == null)
            SetLightOn();
    }

    public void SetLightOn()
    {
        gameObject.GetComponent<MeshRenderer>().material = lightOnMaterial;
        pointLight.SetActive(true);
    }
}
