using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikke : MonoBehaviour
{
    public Rigidbody2D rb;
    public int spikespeed;
    // Start is called before the first frame update
    void Start()
    {
        //rb.AddForce(new Vector2(0f, spikespeed));
        rb.velocity = transform.up * spikespeed;
    }
}
