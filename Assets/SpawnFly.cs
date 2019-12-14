using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFly : MonoBehaviour
{
    public GameObject FireFly;
    public Transform FireSpawn;

    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            Instantiate(FireFly, FireSpawn.position, FireSpawn.rotation);
            Destroy(gameObject);
        }
    }
}
