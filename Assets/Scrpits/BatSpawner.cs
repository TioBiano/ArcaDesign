using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawner : MonoBehaviour
{
    public Transform batpos;
    public GameObject bat;
    [SerializeField] private Collider2D batcol;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(bat, batpos.position, batpos.rotation);
        batcol.enabled = false;
        Destroy(gameObject, 2f);
    }
}
