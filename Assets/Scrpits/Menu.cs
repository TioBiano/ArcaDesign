using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private float timer = 0.0f;
    private bool timerbool = false;

    private void Awake()
    {
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (timerbool)
        {
            timer += Time.deltaTime;
        }

        if (timer >= 3)
        {
            SceneManager.LoadScene(1);
        }
    }
    public void Play()
    {
        timerbool = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
