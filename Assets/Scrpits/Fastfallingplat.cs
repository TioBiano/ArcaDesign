using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fastfallingplat : MonoBehaviour
{
    private Rigidbody2D rb;
    private ParticleSystem ps;
    private Animation anim;
    public Transform RespawnPlat;
    public GameObject fallingplat;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        GetComponent<ParticleSystem>().Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
            StartCoroutine(Rockfall());
    }

    IEnumerator Rockfall()
    {
        GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(.5f);
        rb.isKinematic = false;
        yield return new WaitForSeconds(4);
        Instantiate(fallingplat, RespawnPlat.position, RespawnPlat.rotation);
        Destroy(gameObject, 1f);
    }
}