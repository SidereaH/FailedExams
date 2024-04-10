using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
public class  Slime: MonoBehaviour
{
    public float damage = 1f;
    public float knockbackForce = 100f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();

        if (damageable != null)
        {

            Vector2 direction = (collider.transform.position - transform.position).normalized;

            Vector2 knockback = direction * knockbackForce;
            damageable.OnHit(damage, knockback);
        }
        Debug.Log("onCollisionEnter");
    }

    
}
