using System;
using UnityEngine;

public enum PlayerState
{
    grounded,
    jumped,
}

public class PlayerControl : MonoBehaviour
{
    public float speed = 350f;
    public float jumpForce = 10f;
    public PlayerState state = PlayerState.grounded;
    protected Rigidbody2D rb;
    protected Transform legs;
    protected Animator animator;
    //protected Camera camera;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        foreach (var component in GetComponentsInChildren<Transform>())
        {
            if (component.name is "Legs")
            {
                legs = component;
                break;
            }
        }
        Debug.Log(legs?.name);
        animator = GetComponent<Animator>();
        //camera = GetComponent<Camera>();
    }

    protected void FixedUpdate()
    {
        CheckCollisions();
        MovePlayer();
        UpdateTexture();
    }

    protected virtual void MovePlayer()
    {
        throw new NotImplementedException();
    }

    protected virtual void UpdateTexture()
    {
        throw new NotImplementedException();
    }

    protected virtual void Flip(int direction)
    {
        throw new NotImplementedException();
    }

    protected virtual void CheckCollisions()
    {
        throw new NotImplementedException();
    }
}