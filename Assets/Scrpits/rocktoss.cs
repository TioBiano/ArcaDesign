using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rocktoss : MonoBehaviour
{
    public Transform pos;
    public GameObject rock;
    public Text season;
    private bool sex = false;
    private bool fuck = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            RockToss();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            sex = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            fuck = true;
        }

        if (fuck == true)
        {
            Instantiate(rock, pos.position, pos.rotation);
        }
        season.text = "is mating season : " + sex.ToString();
    }

    void RockToss()
    {
        Instantiate(rock, pos.position, pos.rotation);
    }
}
