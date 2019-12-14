using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyai : MonoBehaviour
{
    private float timemove = 0.0f;
    private float timeshoot = 0.0f;
    private Rigidbody2D rb;
    public int speed;
    public int timetoflip;
    public int shotsdelay;
    private bool isinrange;
    private int moving;
    private bool spikeshootitmer;
    private bool shootingspikes;
    private int shotsfirecalc;
    public Text InRange;
    public GameObject Spike;
    public Transform Spikepos;
    public Transform Spikepos1;
    public Transform Spikepos2;
    public Transform Spikepos3;
    public Transform Spikepos4;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Flip();
        moving = 1;
        spikeshootitmer = false;
        shotsfirecalc = 3;
    }

    // Update is called once per frame
    void Update()
    {
        InRange.text = "can see you : " + isinrange.ToString();
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.AddForce(new Vector2((1 * speed * moving), 0f));

        if (moving == 1)
        {
            timemove += Time.deltaTime;
        }

        if (spikeshootitmer == true)
        {
            timeshoot += Time.deltaTime;
        }

        if (timemove > timetoflip)
        {
            Timeflip();
        }

        if (shootingspikes == true)
        {
            SpikesTimer();
        }
    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void Timeflip()
    {
        timemove = 0;
        Flip();
        speed = speed * -1;
    }

    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            isinrange = true;
            moving = 0;
            shootingspikes = true;
            shotsfirecalc = 3;
        }
    }
    private void OnTriggerExit2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            isinrange = false;
        }
    }

    private void SpikesTimer()
    {
        spikeshootitmer = true;

        if (timeshoot > shotsdelay)
        {
            ShootSpikes();
            timeshoot = 0;
            shotsfirecalc--;
        }

        if (shotsfirecalc == 0)
        {
            shootingspikes = false;
            timeshoot = 0;
            moving = 1;
            Timeflip();
        }
    }
    private void ShootSpikes()
    {
        Instantiate(Spike, Spikepos.position, Spikepos.rotation);
        Instantiate(Spike, Spikepos1.position, Spikepos1.rotation);
        Instantiate(Spike, Spikepos2.position, Spikepos2.rotation);
        Instantiate(Spike, Spikepos3.position, Spikepos3.rotation);
        Instantiate(Spike, Spikepos4.position, Spikepos4.rotation);
    }
}

