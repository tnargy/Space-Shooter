using UnityEngine;

public class Player : Ship
{
    public Joystick joystick;
    public GameObject mobileUI;
    public GameObject gm;
    public Animator animator;

    private Vector2 movement;

    void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8.5f, 8.5f),
                                        Mathf.Clamp(transform.position.y, -4.5f, 4.5f));
        if (mobileUI.activeSelf)
        {
            if (joystick.Horizontal > .25f)
            {
                movement.x = 1;
            }
            else if (joystick.Horizontal < -.25f)
            {
                movement.x = -1;
            }
            else
            {
                movement.x = 0;
            }
            if (joystick.Vertical > .25f)
            {
                movement.y = 1;
            }
            else if (joystick.Vertical < -.25f)
            {
                movement.y = -1;
            }
            else
            {
                movement.y = 0;
            }
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

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
            gm.GetComponent<GM>().SendMessage("Score");
        }
    }
}
