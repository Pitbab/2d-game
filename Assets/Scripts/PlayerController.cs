using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    
    public InputBrain inputBrain { get; private set; }
    
    public Animator animator { get; private set; }
    public Rigidbody2D rb {get; private set; }
    public SpriteRenderer spRenderer {get; private set; }

    private LayerMask ground;
    public bool isGrounded { get; private set; }

    private Vector2 velocity;
    public float yVel => velocity.y;
    [Header("Movements")]
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float velPower;
    [SerializeField] private float frictionAmount;
    [Header("Jump")]
    [SerializeField] private float jumpHeight;
    [Header("Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashResetTimer;

    private const float rollTimer = 0.24f;
    
    public bool isJumping { get; private set; }
    public bool isFalling { get; private set; }
    public bool isRolling { get; private set; }
    public bool canRoll { get; private set; }
    public bool blocking { get; private set; }
    
    public bool UsedDoubleJump { get; private set; }

    #endregion

    #region Events

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        ground = LayerMask.GetMask("Ground");
        inputBrain = gameObject.AddComponent<InputBrain>();
        canRoll = true;
    }

    private void Update()
    {
        Jump();
        Rolling();
    }

    private void FixedUpdate()
    {
        Move();
        Friction();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == ground) ;
        {
            isGrounded = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == ground) ;
        {
            isGrounded = false;
        }
    }

    #endregion

    #region Functions
    
    private void Move()
    {
        float targetSpeed = inputBrain.horAxis * speed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        
        rb.AddForce(movement * Vector2.right);
    }

    private void Friction()
    {
        if (isGrounded && inputBrain.horAxis == 0 && !isRolling)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));

            amount *= Mathf.Sign(rb.velocity.x);
            
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    private void Rolling()
    {
        if (inputBrain.rolling && canRoll)
        {
            int dir = spRenderer.flipX ? -1 : 1;
            rb.AddForce(new Vector2(dashSpeed * dir, 0), ForceMode2D.Impulse);
            isRolling = true;
            canRoll = false;
            StartCoroutine(NoFricDash());
            StartCoroutine(ResetDash());
        }
    }

    private void Jump()
    {
        if (inputBrain.jumping)
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
                isJumping = true;
            }
            else if (!isGrounded && !UsedDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
                isJumping = true;
                UsedDoubleJump = true;
                isFalling = false;
            }
        }

        if (rb.velocity.y < 0)
        {
            isJumping = false;
            isFalling = true;
        }

        if (isGrounded)
        {
            isFalling = false;
            UsedDoubleJump = false;
        }
        
    }

    #endregion

    #region Ienumm

    private IEnumerator NoFricDash()
    {
        float timer = 0;
        
        while (timer < dashResetTimer)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        isRolling = false;
    }


    private IEnumerator ResetDash()
    {
        float timer = 0;

        while (timer < dashResetTimer)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        canRoll = true;

    }

    #endregion
}
