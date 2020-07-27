using UnityEngine;

public class Shield : MonoBehaviour
{
    public int health;
    public float timeLeftInShield;
    private SpriteRenderer sr;
    private Material hitEffect;
    private Material matDefault;

    // Start is called before the first frame update
    void OnEnable()
    {
        health = 5;
        timeLeftInShield = 10f;

        sr = gameObject.GetComponent<SpriteRenderer>();
        hitEffect = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
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
            sr.material = hitEffect;
            Invoke("RestMaterial", .1f);
            Destroy(collision.gameObject);
            health -= collision.GetComponent<Bullet>().damage;
        }
    }

    private void RestMaterial()
    {
        sr.material = matDefault;
    }
}
