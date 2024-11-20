using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightDoor : MonoBehaviour
{
    [SerializeField] private int idLight;
    public Material lightOnMaterial;
    [SerializeField] private GameObject pointLight;
    
    // Start is called before the first frame update

    void Update()
    {
    }

    public void SetLightOn(int idMimic)
    {
        if (idLight == idMimic)
        {
            gameObject.GetComponent<MeshRenderer>().material = lightOnMaterial;
            pointLight.SetActive(true);
            if(gameObject.CompareTag("TutoDoor"))
            {
                gameObject.transform.parent.parent.parent.GetComponent<Animator>().SetTrigger("DOOR");
            }
        }
    }
}
