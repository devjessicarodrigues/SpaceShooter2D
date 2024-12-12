using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToPoint : MonoBehaviour
{
    public Transform localA; 
    public Transform localB; 
    public float speed = 0.05f; 
    private Vector3 target;

    void Start()
    {
        target = localB.position; 
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            target = target == localA.position ? localB.position : localA.position;
        }
    }
}
