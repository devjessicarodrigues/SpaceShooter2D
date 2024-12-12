using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    [Header("Missile")]
    public GameObject missile;
    public Transform missileSpawnPosition;
    public float destroyTime = 5f;
    public Transform muzzleSpawnPosition;

    [Header("Audio")]
    public AudioClip missileSound;
    private AudioSource audioSource;

    [Header("Movement Limits")]
    public float minX = -8f; 
    public float maxX = 8f;  
    public float minY = -4f;
    public float maxY = 4f;  

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        PlayerMovement();
        PlayerShoot();
    }

    void PlayerMovement()
    {

        float posX = Input.GetAxis("Horizontal");
        float posY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(posX, posY, 0) * speed * Time.deltaTime;
        transform.Translate(movement);

        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMissle();
            PlayMissileSound();
        }
    }

    void SpawnMissle()
    {
        GameObject gm = Instantiate(missile, missileSpawnPosition.position, missileSpawnPosition.rotation);
        gm.transform.SetParent(null);
        Destroy(gm, destroyTime);
    }

    void PlayMissileSound()
    {
        if (missileSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(missileSound);
        }
        else
        {
            Debug.LogWarning("missileSound ou audioSource não configurado no Inspector.");
        }
    }
}
