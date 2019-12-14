using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class geyserguys : MonoBehaviour
{
    private Rigidbody2D rb;
    public float Shootup;
    private float waiting;
    private float timer = 0.0f;
    public int flyup;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        waiting = (int)timer;
        Debug.Log(waiting);
        if (waiting <= flyup)
        {
            rb.AddForce(new Vector2(0, Shootup));
        }
        if (waiting == flyup + 5)
        {
            timer = 0;
        }
    }
}
