using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform bulletPositionSpawn;
    public GameObject bulletPrefab;
    private PlayerManager playerManager;


    public float bulletSpeed = 10f;
    private float bulletTimeOfLife = 4f;

    [SerializeField] private Camera fpsCam;
    [SerializeField] private float aimDistanceFromCamera = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerManager.GetIsGameOver() && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Aim();
        }
    }

    void Aim()
    {
        float screenX = Screen.width / 2;
        float screenY = Screen.height / 2;

        RaycastHit hit;
        Ray ray = fpsCam.ScreenPointToRay(new Vector3(screenX, screenY));

        // Check whether your are pointing to something so as to adjust the direction
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(aimDistanceFromCamera); // You may need to change this value according to your needs

        // Create the bullet and give it a velocity according to the target point computed before
        var bullet = Instantiate(bulletPrefab, hit.point, Quaternion.LookRotation(hit.normal));
    }
}
