using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 1f;
    bool canAttack;
    private float timeBtwShots = 0;
    [SerializeField] float startTimeBtwShots;
    void Start()
    {

    }
    private void Update()
    {
        if (timeBtwShots <= 0)
        {

            canAttack = true;     
            timeBtwShots = startTimeBtwShots;

        }
        else
        {
            timeBtwShots -= Time.deltaTime;
            canAttack = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag != "Enemy")
        {
            Collider2D collider = collision.collider;
            IDamageable damageable;
            if (collision.gameObject.tag != "Player")
            {
                damageable = collider.transform.parent.GetComponentInParent<IDamageable>();
            }
            else
            {
                damageable = collider.GetComponent<IDamageable>();
            }
            IDamageable me = gameObject.GetComponent<IDamageable>();


            if (damageable != null)
            {
                Vector2 direction = (collider.transform.position - transform.position).normalized;
                // Vector2 knockback = direction * knockbackForce;
                damageable.OnHit(damage);
            }
        }
    }
}
