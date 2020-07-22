using UnityEngine;

public class Player : Ship
{
    public GameObject gm;
    public Animator animator;

    private Vector2 movement;

    void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8.5f, 8.5f),
                                        Mathf.Clamp(transform.position.y, -4f, 4.5f));

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Speed", Mathf.Abs(movement.x));
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GM.GameOver();
        }
        else if (collision.gameObject.CompareTag("Loot"))
        {
            Destroy(collision.gameObject);
            gm.GetComponent<GM>().SendMessage("Score", 100);
        }
    }
}
