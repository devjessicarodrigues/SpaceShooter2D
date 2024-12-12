using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float maxThrust;
    public float minTorque;
    public Rigidbody2D rb;
    public float screenTop;
    public float screenBottom;
    public float screenLeft;
    public float screenRight;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 thrust = new Vector2(Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        float torque = Random.Range(-maxThrust, maxThrust);

        rb.AddForce(thrust);
        rb.AddTorque(torque);
    }

    void Update()
    {
        Vector2 newPos = transform.position;
        if (transform.position.y > screenTop) { newPos.y = screenTop; }
        if (transform.position.y < screenBottom) { newPos.y = screenBottom; } 
        if (transform.position.x > screenRight) { newPos.x = screenLeft; }
        if (transform.position.x < screenLeft) { newPos.x = screenRight; }

        transform.position = newPos; 
    }
}
