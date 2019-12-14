using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public Vector2 camPos;
    private float moving;
    private bool movenow = false;

    private void Update()
    {
        if (movenow)
        {
            moving += Time.deltaTime;
            transform.position = camPos;
            camPos = new Vector2(0, (moving * -2.5f));
        }
    }
    public void CameraMove()
    {
        movenow = true;
    }
}
