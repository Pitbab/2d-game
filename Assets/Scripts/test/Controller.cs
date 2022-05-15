using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
    #region State Variables

    public StateMachine stateMachine { get; private set; }
    
    public IdleState IdleState { get; private set; }
    public MoveState moveState { get; private set; }
    public JumpState jumpState { get; private set; }
    public InAirState inAirState { get; private set; }
    public LandState landState { get; private set; }

    [SerializeField] private PlayerData playerData;
    
    #endregion

    #region Components

    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public PlayerInputHandler playerInputHandler { get; private set; }

    #endregion

    #region Check Transforms

    [SerializeField] private Transform groundCheck;

    #endregion

    #region Other Variables

    public int facingDir { get; private set; }
    
    public Vector2 currentVelocity { get; private set; }
    
    private Vector2 workSpace;

    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        stateMachine = new StateMachine();

        IdleState = new IdleState(this, stateMachine, playerData, "Idle");
        moveState = new MoveState(this, stateMachine, playerData, "Run");
        jumpState = new JumpState(this, stateMachine, playerData, "InAir");
        inAirState = new InAirState(this, stateMachine, playerData, "InAir");
        landState = new LandState(this, stateMachine, playerData, "Land");

    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();

        facingDir = 1;
        
        stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        currentVelocity = rb.velocity;
        stateMachine.currentState.LogicUpdate();
        text.text = stateMachine.currentState.ToString();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    #endregion

    #region Set Functions

    public void SetVelocityX(float velocity)
    {
        workSpace.Set(velocity, currentVelocity.y);
        rb.velocity = workSpace;
        currentVelocity = workSpace;
    }

    public void SetVelocityY(float velocity)
    {
        workSpace.Set(currentVelocity.x, velocity);
        rb.velocity = workSpace;
        currentVelocity = workSpace;
    }

    #endregion

    #region Check Functions

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }
    
    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput != 0 && xInput != facingDir)
        {
            Flip();
        }
    }

    #endregion

    #region Other Functions

    private void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();
    private void AnimationFinishedTrigger() => stateMachine.currentState.AnimationFinishedTrigger();
    
    private void Flip()
    {
        facingDir *= -1;
        transform.Rotate(0.0f, 180f, 0.0f);
    }

    #endregion
    
}
