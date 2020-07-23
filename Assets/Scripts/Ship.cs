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
    private int currentHealth;

    void OnEnable()
    {
        currentHealth = maxHealth;

        if (heathBar != null)
        {
            heathBar.SetMaxHealth(maxHealth);
        }

        sr = gameObject.GetComponent<SpriteRenderer>();
        hitEffect = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;

        explosionRef = Resources.Load("Explosion");
    }
    
    public void TakeDamage()
    {
        string ship;
        if (gameObject.CompareTag("Player"))
        {
            ship = "Player";
        }
        else
        {
            ship = "Enemy";
        }
        gm.GetComponent<GM>().Score(ship + "Damage");

        sr.material = hitEffect;
        if (--currentHealth <= 0)
        {
            gm.GetComponent<GM>().Score(ship + "Kill");
            Kill();
        }
        else
        {
            Invoke("RestMaterial", .1f);
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
