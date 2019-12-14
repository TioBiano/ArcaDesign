using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fadescene : MonoBehaviour
{
    public Animator animator;
    private float timer = 0.0f;
    private bool timerbool = false;
    public int scene = 1;
    public GameObject disablecanvasfade;
    private float countdown = 1.5f;

    void Awake()
    {
        disablecanvasfade.SetActive(true);
    }

    void begone()
    {
        disablecanvasfade.SetActive(false);
    }

    void Update()
    {
        countdown -= +Time.deltaTime;
        if (countdown <= 0)
            begone();

        if (timerbool)
        {
            disablecanvasfade.SetActive(true);
            timer += Time.deltaTime;
        }
        if (timer >= 2)
        {
            SceneManager.LoadScene(scene);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            Fade();
            timerbool = true;
        }
    }

    public void Fade()
    {
        animator.SetTrigger("Fade");
    }
}
