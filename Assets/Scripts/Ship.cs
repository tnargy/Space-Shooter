﻿using UnityEngine;

public class Ship : MonoBehaviour
{
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
        sr.material = hitEffect;
        if (--currentHealth <= 0)
        {
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