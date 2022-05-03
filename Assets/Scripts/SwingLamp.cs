using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingLamp : MonoBehaviour
{
    Rigidbody rb;
    float randomTimer = 0.0f;
    [SerializeField] float forceMultiplier = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        randomTimer -= Time.deltaTime;
        if (randomTimer <= 0)
        {
            rb.AddForce(rb.velocity.normalized * forceMultiplier, ForceMode.Acceleration);
            randomTimer = Random.value * 4.0f;
        }
    }
}
