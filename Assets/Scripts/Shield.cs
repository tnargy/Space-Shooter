using UnityEngine;

public class Shield : MonoBehaviour
{
    public int health;
    public float timeLeftInShield;

    // Start is called before the first frame update
    void OnEnable()
    {
        health = 5;
        timeLeftInShield = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 || timeLeftInShield <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (GM.canFire)
        {
            timeLeftInShield -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            health -= collision.GetComponent<Bullet>().damage;
        }
    }
}
