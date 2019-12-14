using UnityEngine;


public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float runSpeed = 20f;
    [SerializeField] private float Jump = 80f;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform GroundedPos;
    [SerializeField] private Transform CeilingCheck;
    [SerializeField] private Collider2D CrouchDisable;

    public GameObject Checkpoint;

    private bool AirControl = true;
    private float CrouchSpeed = .5f;
    private float MovementSmoothing = .2f;
    const float GroundedRadius = .2f;
    private bool Grounded;
    const float CeilingRadius = .2f;
    private Rigidbody2D rb;
    private bool FacingRight = true;
    private Vector3 velocity = Vector3.zero;
    private bool floating = false;
    private bool uncrounch = true;
    private bool holdcr;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    private bool running;

    public AudioClip WalkSound;
    public AudioClip JumpSound;

    public AudioSource WalkSource;
    public AudioSource JumpSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 1f;
        WalkSource.clip = WalkSound;
        JumpSource.clip = JumpSound;
    }


    private void FixedUpdate()
    {
        if (rb.velocity.y > 0 && !Input.GetButton("Jump") && !floating)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * 1.5f * Time.fixedDeltaTime;
        }

        Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundedPos.position, GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                Grounded = true;
            GetComponent<Animator>().SetBool("JumpDown", false);
        }
        Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

        if (floating)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0.3f;
            GetComponent<Rigidbody2D>().drag = 3;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 0.8f;
            GetComponent<Rigidbody2D>().drag = 1;
        }

    }

    private void Update()
    {
        if (Grounded)
        {
            floating = false;
        }
        if (!Grounded)
        {
            WalkSource.Stop();
            running = false;
            if (Input.GetButton("Jump"))
            {
                floating = true;
            }
            if (Input.GetButtonUp("Jump"))
            {
                floating = false;
            }
            if (!floating)
            {
                GetComponent<Animator>().SetBool("JumpDown", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("JumpDown", false);
            }
        }

        if (floating)
        {
            GetComponent<Animator>().SetBool("Float", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Float", false);
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch") && Grounded)
        {
            crouch = true;
            holdcr = true;
            GetComponent<Animator>().SetBool("CrouchIdle", true);
        }
        if (Input.GetButtonUp("Crouch"))
        {
            holdcr = false;
        }
        if (uncrounch && !holdcr)
        {
            crouch = false;
            GetComponent<Animator>().SetBool("CrouchIdle", false);
            GetComponent<Animator>().SetBool("Crouch", false);
            GetComponent<Animator>().SetBool("idle", true);
        }

        if (crouch)
        {
            if (rb.velocity.x > 0.1f || rb.velocity.x < -0.1f)
            {
                GetComponent<Animator>().SetBool("Crouch", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("Crouch", false);
            }
        }
        if (rb.velocity.x > 0.1f || rb.velocity.x < -0.1f)
        {
            GetComponent<Animator>().SetBool("Running", true);
            if(!running)
            {
                running = true;
                //WalkSource.Play();
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("Running", false);
            WalkSource.Stop();
            running = false;
        }

        if (rb.velocity.x == 0 || rb.velocity.y == 0 || !crouch || Grounded)
        {
            GetComponent<Animator>().SetBool("idle", true);
        }
    }


    public void Move(float move, bool crouch, bool jump)
    {
        if (crouch)
        {
            if (Physics2D.OverlapCircle(CeilingCheck.position, CeilingRadius, m_WhatIsGround))
            {
                uncrounch = false;
            }
            else
            {
                uncrounch = true;
            }
        }

        if (Grounded || AirControl)
        {
            if (crouch)
            {
                move *= CrouchSpeed;
                CrouchDisable.enabled = false;
            }
            else
            {
                CrouchDisable.enabled = true;
            }

            Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);

            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, MovementSmoothing);

            if (move > 0 && !FacingRight)
            {
                Flip();
            }
            else if (move < 0 && FacingRight)
            {
                Flip();
            }
        }
        if (Grounded && jump && !crouch)
        {
            //JumpSource.Play();
            floating = false;
            Grounded = false;
            crouch = false;
            rb.AddForce(new Vector2(0f, Jump));
        }
    }

    private void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Death"))
        {
            transform.position = Checkpoint.transform.position;
        }

        if (collision2D.gameObject.CompareTag("Checkpoint"))
        {
            Checkpoint = collision2D.gameObject;
        }
    }
}
