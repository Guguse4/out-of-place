using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    private PlayerManager playerManager;
    [SerializeField] private LightDoor[] lightDoor;

    [SerializeField] private Camera fpsCam;
    [SerializeField] private float aimDistanceFromCamera = 1000f;
    [SerializeField] private PauseManager pauseManager;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip hitAlienSound;
    [SerializeField] private AudioClip missSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseManager != null && pauseManager.IsPaused)
        {
            return; // Do nothing if the game is paused
        }
        
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


        if (hit.transform.CompareTag("Mimic"))
        {
            PlaySound(hitAlienSound);
            for (int i = 0; i < lightDoor.Length; i++)
            {
                lightDoor[i].SetLightOn(hit.transform.GetComponent<MimicManager>().GetIdMimic());
            }

            Destroy(hit.transform.gameObject);
            playerManager.numberOfMimicFound--;
        }
        else
        {
            PlaySound(missSound);
            GameObject impact = Instantiate(bulletPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Debug.Log(impact.transform.rotation);
            impact.transform.position = new Vector3(impact.transform.position.x, impact.transform.position.y + 0.001f, impact.transform.position.z);
            playerManager.numberOfHP -= 1;
            Debug.Log("Vie restante : " + playerManager.numberOfHP);
        }
    }
    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
