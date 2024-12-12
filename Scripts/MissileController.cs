using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float missileSpeed = 25f;

    void Update()
    {
        transform.Translate(Vector3.up * missileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(GameManager.Instance.explosion, transform.position, transform.rotation);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            Score.Instance.AddScore(15);
        }
        else if (collision.gameObject.tag == "Asteroid")
        {
            Instantiate(GameManager.Instance.explosion, transform.position, transform.rotation);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            Score.Instance.AddScore(10);
        }
        else if (collision.gameObject.tag == "EnemyShooter")
        {
            Instantiate(GameManager.Instance.explosion, transform.position, transform.rotation);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            Score.Instance.AddScore(20);
        }
        else if (collision.gameObject.tag == "EnemyAlien")
        {
            collision.gameObject.GetComponent<AlienController>().TakeDamage(10f);

            Instantiate(GameManager.Instance.explosion, transform.position, transform.rotation);
            Destroy(this.gameObject);
            Score.Instance.AddScore(40);
        }
        if (collision.gameObject.CompareTag("EnemyLoot"))
        {
            Instantiate(GameManager.Instance.explosion, transform.position, transform.rotation);
            Destroy(this.gameObject); 

            Destroy(collision.gameObject);

            GameObject healthItem = Instantiate(GameManager.Instance.healthItemPrefab,
                                                 collision.transform.position,
                                                 Quaternion.identity);

            if (healthItem.TryGetComponent<Collider2D>(out Collider2D collider))
            {
                collider.isTrigger = true;
            }
            Score.Instance.AddScore(25);
        }
    }
}
