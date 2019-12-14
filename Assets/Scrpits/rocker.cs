using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocker : MonoBehaviour
{
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        //rb.AddForce(new Vector2(-15, 20));
        rb.AddForce(new Vector2(0, 0));
    }
}
