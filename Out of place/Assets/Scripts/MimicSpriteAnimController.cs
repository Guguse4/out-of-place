using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicSpriteAnimController : MonoBehaviour
{
    [SerializeField] private GameObject doorLight;
    [SerializeField] private GameObject associatedObject;
    [SerializeField] private float moveSpeed = 4f;
    public Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (associatedObject == null)
        {
            rend.enabled = true;
            //Moves the GameObject from it's current position to destination over time
            transform.position = Vector3.MoveTowards(transform.position, doorLight.transform.position, Time.deltaTime * moveSpeed);
            if(transform.position == doorLight.transform.position)
            {
                Destroy(gameObject);
            }
        }
    }
}
