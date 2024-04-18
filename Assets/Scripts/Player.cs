using System;
using UnityEngine;

public enum PlayerState
{
    grounded,
    jumped,
}

public class PlayerControl : MonoBehaviour
{
    public float speed = 500f;
    public float jumpForce = 10f;
    public PlayerState state = PlayerState.grounded;
    protected Rigidbody2D rb;
    protected BoxCollider2D legs;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        legs = GetComponentInChildren<BoxCollider2D>();
    }

    protected void FixedUpdate()
    {
        CheckCollisions();
        MovePlayer();
        UpdateTexture();
        Flip();
    }

    protected virtual void MovePlayer()
    {
        throw new NotImplementedException();
    }

    protected virtual void UpdateTexture()
    {
        throw new NotImplementedException();
    }

    protected virtual void SetAnimationRun()
    {
        throw new NotImplementedException();
    }

    protected virtual void SetAnimationJump()
    {
        throw new NotImplementedException();
    }

    protected virtual void Flip()
    {
        throw new NotImplementedException();
    }

    protected virtual void CheckCollisions()
    {
        throw new NotImplementedException();
    }
}