using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fatfallingplat : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
            rb.isKinematic = false;
    }
}
