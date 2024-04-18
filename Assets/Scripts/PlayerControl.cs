using UnityEngine;

public class Player1 : PlayerControl
{
    protected override void MovePlayer()
    {
        var direction = 0;
        if (Input.GetKey(KeyCode.A))
        {
            direction = -1;
            Flip(direction);
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction = 1;
            Flip(direction);
        }

        animator.SetBool("isRunning", direction != 0);

        var velocity = new Vector2(direction * speed * Time.fixedDeltaTime, rb.velocity.y);
        if (state == PlayerState.grounded && Input.GetButton("Jump"))
        {
            velocity.y = jumpForce;
            state = PlayerState.jumped;
            animator.SetBool("isJumping", true);
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

    protected override void Flip(int direction)
    {
        transform.localScale = new Vector3(direction, 1, 1);
    }

    protected override void CheckCollisions()
    {
        var colliders = Physics2D.OverlapCircleAll(legs.transform.position, 1f);
        foreach (var c in colliders)
        {
            if (!c.gameObject.CompareTag("Ground")) continue;
            state = PlayerState.grounded;
            animator.SetBool("isJumping", false);
        }
    }
}