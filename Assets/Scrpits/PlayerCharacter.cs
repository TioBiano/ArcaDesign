using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCharacter : MonoBehaviour
{
    public float ForcaPulo;
    public float velocidadeMaxima;

    public int lives;
    public int rings;

    public bool IsGrouded;

    public bool canFly;

    public GameObject lastCheckpoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        float movimento = Input.GetAxis("Horizontal");

        rigidbody.velocity = new Vector2(movimento * velocidadeMaxima, rigidbody.velocity.y);

        if (movimento < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (movimento > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (movimento > 0 || movimento < 0)
        {
            GetComponent<Animator>().SetBool("walking", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("walking", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrouded)
            {
                rigidbody.AddForce(new Vector2(0, ForcaPulo));
                GetComponent<AudioSource>().Play();
                canFly = false;
            }
            else
            {
                canFly = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            GetComponent<Animator>().SetTrigger("hammer");
            Collider2D[] colliders = new Collider2D[3];
            transform.Find("hammerArea").gameObject.GetComponent<Collider2D>()
                .OverlapCollider(new ContactFilter2D(), colliders);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != null && colliders[i].gameObject.CompareTag("Monstros"))
                {
                    Destroy(colliders[i].gameObject);
                }

            }
        }

        if (canFly && Input.GetKey(KeyCode.Space))
        {
            GetComponent<Animator>().SetBool("flying", true);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -0.3f);
            //fly
        }
        else
        {
            GetComponent<Animator>().SetBool("flying", false);
        }


        if (IsGrouded)
        {
            GetComponent<Animator>().SetBool("jumping", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("jumping", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Checkpoint"))
        {
            lastCheckpoint = collision2D.gameObject;
        }
    }


    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("moedas"))
        {
            Destroy(collision2D.gameObject);
            rings++;
        }

        if (collision2D.gameObject.CompareTag("Monstros"))
        {
            lives--;
            if (lives == 0)
            {
                transform.position = lastCheckpoint.transform.position;
            }
        }


        if (collision2D.gameObject.CompareTag("plataformas"))
        {
            IsGrouded = true;
        }

        if (collision2D.gameObject.CompareTag("Tranpolim"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 10f);
        }

    }

    void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("plataformas"))
        {
            IsGrouded = false;
        }
    }



}
