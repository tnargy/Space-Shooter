using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject ship;
    public GameObject beam;
    public Transform[] guns;
    public float force;
    public float randomOffset;
    public string target;

    private float randomTime;

    private void Start()
    {
        randomTime = Time.time + (float)Random.Range(0f, randomOffset);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            if (ship.CompareTag("Player"))
            {
                if (Input.GetButtonDown("Fire1") && GM.canFire)
                {
                    Fire();
                }
            }
            else if (Time.time > randomTime && force != 0)
            {
                randomTime = Time.time + (float)Random.Range(0f, randomOffset);
                Fire();
            }
        }
    }

    public void Fire()
    {
        foreach (Transform gun in guns)
        {
            GameObject bullet = Instantiate(beam, gun.position, gun.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (target == "")
            {
                rb.AddForce(gun.up * force, ForceMode2D.Impulse);
            }
            else
            {
                Vector2 targetPos = GameObject.FindGameObjectWithTag(target).transform.position;
                targetPos = (Vector2)gun.position - targetPos;
                rb.AddForce(targetPos * force, ForceMode2D.Impulse);
            }
        }
    }
}
