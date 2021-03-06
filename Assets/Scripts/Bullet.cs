﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string targetTag;
    public int damage = 1;

    private void OnEnable()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject item in bullets)
        {
            Physics2D.IgnoreCollision(item.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            Destroy(gameObject);
            collision.gameObject.SendMessage("TakeDamage", damage);
        }
    }
}
