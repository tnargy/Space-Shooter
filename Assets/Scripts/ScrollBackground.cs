using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    private float verticalLength;
    private readonly float moveSpeed = -20f;
    public Rigidbody2D rb;
    public float box = 15.36f;


    // Start is called before the first frame update
    void Start()
    {
        verticalLength = box;
        rb.velocity = new Vector2(0, moveSpeed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -verticalLength)
        {
            Vector2 spaceOffset = new Vector2(0, verticalLength * 2f);
            transform.position = (Vector2)transform.position + spaceOffset;
        }
    }
}
