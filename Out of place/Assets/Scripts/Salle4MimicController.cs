using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salle4MimicController : MonoBehaviour
{
    [SerializeField] private int idEcran;
    public Material materialEcran;
    private MeshRenderer ren;
    //[SerializeField] private GameObject pointLight;
    // Start is called before the first frame update
    void Start()
    {
        ren = gameObject.GetComponent<MeshRenderer>();
    }
    public void SetLightOff(int idMimic)
    {
        if (idEcran == idMimic)
        {
            Material[] mat = ren.materials;
            mat[1].color = Color.black;
            //pointLight.SetActive(false);
        }
    }
}
