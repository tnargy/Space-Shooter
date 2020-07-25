using UnityEngine;

public class Loot : MonoBehaviour
{
    private void Start()
    {
        transform.GetComponent<Rigidbody2D>().AddForce(transform.up * -2, ForceMode2D.Impulse);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
