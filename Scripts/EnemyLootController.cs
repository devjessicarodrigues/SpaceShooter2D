using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLootController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float minDistance = 2f;
    public float maxDistance = 5f;

    [Header("Ball Settings")]
    public GameObject ballPrefab;
    public Transform firePoint;
    public float ballSpeed = 5f;
    public float timeBetweenShots = 2f;

    private Transform player;
    private bool isShooting = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > maxDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else if (distance < minDistance)
        {
            Vector3 direction = (transform.position - player.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, moveSpeed * Time.deltaTime);
        }

        if (!isShooting)
        {
            StartCoroutine(ShootBall());
        }
    }

    IEnumerator ShootBall()
    {
        isShooting = true;

        ReleaseBall();

        yield return new WaitForSeconds(timeBetweenShots);

        isShooting = false;
    }

    void ReleaseBall()
    {
        GameObject ball = Instantiate(ballPrefab, firePoint.position, Quaternion.identity);

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * ballSpeed;
        }
        Destroy(ball, 5f);
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
