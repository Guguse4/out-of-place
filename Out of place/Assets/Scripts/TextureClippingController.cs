using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureClippingController : MonoBehaviour
{
    [SerializeField] private GameObject plane1;
    [SerializeField] private GameObject plane2;
    [SerializeField] private bool isDelete = false;

    // Update is called once per frame
    void Update()
    {
        if(!isDelete && (plane1 == null || plane2 == null))
        {
            isDelete = true;
            if (plane1 == null)
                Destroy(plane2);

            else if(plane2 == null)
                Destroy(plane1);
        }
    }
}
