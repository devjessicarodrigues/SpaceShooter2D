using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Enemy Prefabs")]
    public GameObject enemyPrefab;
    public GameObject enemyShooter;
    public List<GameObject> asteroidPrefabs;

    [Header("Enemy Loot")]
    public List<GameObject> enemyLootPrefabs;
    public GameObject healthItemPrefab;

    [Header("Boss Enemy")]
    public GameObject enemyAlien;

    [Header("Spawn Settings")]
    public float minInstanteValue;
    public float maxInstanteValue;
    public float enemyDestroyTime = 60f;
    public float enemyShooterDestroyTime = 20f;
    public float enemyLootDestroyTime = 20f;

    [Header("Particle Effects")]
    public GameObject explosion;

    [Header("Audio")]
    public AudioClip shooterSceneSound;
    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        if (shooterSceneSound != null)
        {
            audioSource.clip = shooterSceneSound;
            audioSource.Play();
        }

        InvokeRepeating("InstantiateEnemy", 1f, 1f);
        InvokeRepeating("InstantiateAsteroid", 1f, 3f);
        InvokeRepeating("InstantiateEnemyShooter", 1f, 5f);
        InvokeRepeating("InstantiateEnemyBoss", 30f, 60f);
        InvokeRepeating("InstantiateEnemyLoot", 20f, 30f);
    }

    void InstantiateEnemy()
    {
        Vector3 posEnemy = new Vector3(Random.Range(minInstanteValue, maxInstanteValue), 25f, 0f);
        GameObject enemy = Instantiate(enemyPrefab, posEnemy, Quaternion.Euler(0f, 0f, 180f));
        Destroy(enemy, enemyDestroyTime);
    }

    void InstantiateEnemyShooter()
    {
        Vector3 posEnemy = new Vector3(Random.Range(minInstanteValue, maxInstanteValue), 25f, 0f);
        GameObject enemy = Instantiate(enemyShooter, posEnemy, Quaternion.Euler(0f, 0f, 180f));
        Destroy(enemy, enemyShooterDestroyTime);
    }

    void InstantiateAsteroid()
    {
        if (asteroidPrefabs.Count == 0)
        {
            return;
        }

        GameObject selectedAsteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Count)];
        Vector3 posEnemy = new Vector3(Random.Range(minInstanteValue, maxInstanteValue), 10f, 0f);

        GameObject asteroid = Instantiate(selectedAsteroidPrefab, posEnemy, Quaternion.Euler(0f, 0f, 180f));
        Destroy(asteroid, enemyDestroyTime);
    }

    void InstantiateEnemyBoss()
    {
        Vector3 posBoss = new Vector3(Random.Range(minInstanteValue, maxInstanteValue), 27f, 0f);
        GameObject boss = Instantiate(enemyAlien, posBoss, Quaternion.identity);
    }

    void InstantiateEnemyLoot()
    {
        if (enemyLootPrefabs.Count == 0)
        {
            return;
        }

        GameObject selectedLootPrefab = enemyLootPrefabs[Random.Range(0, enemyLootPrefabs.Count)];
        Vector3 posLoot = new Vector3(Random.Range(minInstanteValue, maxInstanteValue), 27f, 0f);

        GameObject loot = Instantiate(selectedLootPrefab, posLoot, Quaternion.identity);
        Destroy(loot, enemyLootDestroyTime);
    }
}
