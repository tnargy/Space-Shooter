using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject gm;
    public Rigidbody2D rb;
    public HealthBar heathBar;
    private SpriteRenderer sr;
    private Material hitEffect;
    private Material matDefault;
    private Object explosionRef;
    public float moveSpeed;
    public int maxHealth;
    public int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;

        if (heathBar != null)
        {
            heathBar.SetMaxHealth(maxHealth);
        }

        sr = gameObject.GetComponent<SpriteRenderer>();
        hitEffect = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;

        explosionRef = Resources.Load("Prefabs/Explosion");
    }

    public void IncreaseHealth(int amount)
    {
        if (currentHealth < maxHealth)
            currentHealth += amount;
        if (heathBar != null)
        {
            heathBar.SetHealth(currentHealth);
        }
    }
    public void TakeDamage()
    {
        sr.material = hitEffect;
        if (--currentHealth <= 0)
        {
            gameObject.SendMessage("ShipDestroyed", 5);
            Kill();
        }
        else
        {
            Invoke("RestMaterial", .1f);
            gameObject.SendMessage("ShipHit", 3);
        }

        if (heathBar != null)
        {
            heathBar.SetHealth(currentHealth);
        }
    }

    private void RestMaterial()
    {
        sr.material = matDefault;
    }

    private void Kill()
    {
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = transform.position;
        Destroy(this.gameObject);
    }
}
