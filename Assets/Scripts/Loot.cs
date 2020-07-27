using UnityEngine;

public class Loot : MonoBehaviour
{
    public int value = 100;

    private void Start()
    {
        transform.GetComponent<Rigidbody2D>().AddForce(transform.up * -2, ForceMode2D.Impulse);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
