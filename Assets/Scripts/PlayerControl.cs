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
    private Rigidbody2D rb;
    public PlayerState state = PlayerState.grounded;
    private BoxCollider2D legs;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        legs = GetComponentInChildren<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        CheckCollisions();
        MovePlayer();
        UpdateTexture();
    }

    private void MovePlayer()
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

    private void UpdateTexture()
    {
    }

    private void SetAnimationRun()
    {
    }

    private void SetAnimationJump()
    {
    }

    private void Flip()
    {
    }

    private void CheckCollisions()
    {
        var collier = Physics2D.OverlapCircle(legs.transform.position, 1f);
        if (collier.gameObject.CompareTag("Ground"))
            state = PlayerState.grounded;
    }
}