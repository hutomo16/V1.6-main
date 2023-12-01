/*using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float JumpHeight = 16f;
    [SerializeField] private Rigidbody2D rb;
    private float horizontal;
    private bool isFacingRight = true;

    [Header("GroundCheck")]
    public Transform GroundCheck;
    public Vector2 GroundCheckSize = new Vector2(-.5f, 0.05f);
    public LayerMask GroundLayer;

    [Header("WallCheck")]
    public Transform WallCheck;
    public Vector2 WallCheckSize = new Vector2(-.5f, 0.05f);
    public LayerMask WallLayer;

    [Header("WallMovement")]
    public float wallSlideSpeed = 2f;
    public bool isWallSliding;

    [Header("Animation")]
    public Animator Anim;

    [Header("Dashing")]
    private bool candash = true; //dash
    private bool isdash;
    [SerializeField] private float dashpower = 24f;
    [SerializeField] private float dashtime = 0.2f;
    [SerializeField] private float dashcooldown = 1f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isdash) //dash
        {
            return;
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        Movement();//fungsi Berjalan
        Jump();//fungsi loncat
        Flip();//fungsi Berputar
        AnimateMove(); // fungsi animasi berjalan
        isGrounded();
        ProceswallSlide();

        if (Input.GetKeyDown(KeyCode.LeftShift) && candash) //dash
        {
            float dashDirection = Input.GetAxisRaw("Horizontal");
            StartCoroutine(Dash(dashDirection));
        }

    }

    private void Movement()
    {
        if (isdash)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void AnimateMove()
    {
        if (horizontal >= 0.1f || horizontal <= -0.1f)
        {
            Anim.SetBool("isRunning", true);
        }
        else
        {
            Anim.SetBool("isRunning", false);
        }
    }

    private bool isGrounded()
    {
        if (Physics2D.OverlapBox(GroundCheck.position, GroundCheckSize, 0, GroundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private bool Walled()
    {
        return (Physics2D.OverlapBox(WallCheck.position, WallCheckSize, 0, WallLayer));
    }

    private void Jump()
    {
        if (isGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpHeight);
            }
        }
    }

    private void ProceswallSlide()
    {
        if (!isGrounded() & Walled() & horizontal != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -wallSlideSpeed));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private IEnumerator Dash(float direction)
    {
        candash = false; //dash
        isdash = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(direction * dashpower, 0f);
        yield return new WaitForSeconds(dashtime);
        rb.gravityScale = originalGravity;
        isdash = false;
        yield return new WaitForSeconds(dashcooldown);
        candash = true;  //Yoga
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(GroundCheck.position, GroundCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(WallCheck.position, WallCheckSize);
    }

}
*/