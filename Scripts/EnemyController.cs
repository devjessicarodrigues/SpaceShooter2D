using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5f;


    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
