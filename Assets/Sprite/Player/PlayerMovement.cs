using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float JumpHeight = 12f;
    [SerializeField] private Rigidbody2D rb;
    private float horizontal;
    private bool isFacingRight = true;

    [Header("GroundCheck")]
    public Transform GroundCheck;
    public Vector2 GroundCheckSize = new Vector2(0.7f, 0.1f);
    public LayerMask GroundLayer;

    [Header("WallCheck")]
    public Transform WallCheck;
    public Vector2 WallCheckSize = new Vector2(0.1f, 1f);
    public LayerMask WallLayer;

    [Header("WallMovement")]
    public float wallSlideSpeed = 2f;
    public bool isWallSliding;
    public bool isWallJumping;
    private float WallJumpDirection;
    [SerializeField] private float WallJumpTime = 0.5f;
    public float WallJumpTimer;
    private Vector2 wallJumpPower = new Vector2(6f, 12f);

    [Header("Animate")]
    public Animator Anim;

    [Header("Dash")]
    private bool candash = true; //dash
    private bool isdash;
    [SerializeField] private float dashpower = 24f;
    [SerializeField] private float dashtime = 0.2f;
    [SerializeField] private float dashcooldown = 1f;


    void Start()
    {
        Anim = GetComponent<Animator>();
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
        isGrounded();
        ProceswallSlide();
        ProceswallJump();

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

        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            Anim.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
            Anim.SetFloat("yVelocity", rb.velocity.y);
            Flip();

        }

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

    private bool isGrounded()
    {
        if(Physics2D.OverlapBox(GroundCheck.position, GroundCheckSize, 0, GroundLayer))
        {
            return true;
        }
        else
        {
            Anim.SetBool("isJumping", true);
            return false;
        }

    }

    private bool isWalled()
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
                Anim.SetBool("isJumping", true);

            }else
            {
                Anim.SetBool("isJumping", false);
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && WallJumpTimer > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(WallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            WallJumpTimer = 0;

            if(transform.localScale.x != WallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(CancelWallJump), WallJumpTime + 0.1f);
        }
    }

    private void ProceswallSlide()
    {
        if (!isGrounded() & isWalled() & horizontal != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -wallSlideSpeed));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void ProceswallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            WallJumpDirection = -transform.localScale.x;
            WallJumpTimer = WallJumpTime;

            CancelInvoke(nameof(CancelWallJump));
        }
        else if (WallJumpTimer > 0f)
        {
            WallJumpTimer -= Time.deltaTime;
        }
    }

    private void CancelWallJump()
    {
        isWallJumping = false;
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
