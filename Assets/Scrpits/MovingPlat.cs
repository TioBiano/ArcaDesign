using UnityEngine;
using System.Collections;

public class MovingPlat : MonoBehaviour
{

    float moveSpeed = 1f;
    private Animator anim;
    public float time;

    // Update is called once per frame
    private void Start()
    {
         anim = GetComponent<Animator>();
        StartCoroutine(GoingUp());
    }

    void FixedUpdate()
    {
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);

            //anim.Play("GeyserWater");

            //anim.Play("GeyserIdle");
    }

    IEnumerator GoingUp()
    {
        moveSpeed = 1f;
        yield return new WaitForSeconds(time * 2);
        moveSpeed = 0;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(GoingDown());
    }

    IEnumerator GoingDown()
    {
        moveSpeed = -2f;
        yield return new WaitForSeconds(time);
        moveSpeed = 0f;
        yield return new WaitForSeconds(1f);
        StartCoroutine(GoingUp());
    }
}