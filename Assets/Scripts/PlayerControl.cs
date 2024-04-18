using UnityEngine;

public class Player1 : PlayerControl
{
    protected override void MovePlayer()
    {
        var h = Input.GetAxis("Horizontal");
        var velocity = new Vector2(h * speed * Time.fixedDeltaTime, rb.velocity.y);
        if (state == PlayerState.grounded && Input.GetButton("Jump"))
        {
            velocity.y = jumpForce;
            state = PlayerState.jumped;
        }
        rb.velocity = transform.TransformDirection(velocity);
    }

    protected override void UpdateTexture()
    {
    }

    protected override void SetAnimationRun()
    {
    }

    protected override void SetAnimationJump()
    {
    }

    protected override void Flip()
    {
    }

    protected override void CheckCollisions()
    {
        var colliders = Physics2D.OverlapCircleAll(legs.transform.position, 1f);
        foreach (var c in colliders)
        {
            if (c.gameObject.CompareTag("Ground"))
                state = PlayerState.grounded;
        }
    }
}