using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFade : MonoBehaviour
{
    private Animation anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        GetComponent<Image>().enabled = false;
        GetComponent<Animator>().enabled = false;
    }
    public void FadeMenu()
    {
        GetComponent<Image>().enabled = true;
        GetComponent<Animator>().enabled = true;
    }
}
