using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public int damage;
    public float distance;
    public LayerMask whatIsSolid;
    public float knockBackForce = 1f;
    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if(hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                IDamageable damagableObject = (IDamageable) hitInfo.collider.GetComponent<IDamageable>();
                if (damagableObject != null)
                {
                    Vector3 parentPosition = transform.root.position;
                    Vector2 direction = (hitInfo.collider.transform.position - parentPosition).normalized;
                    Vector2 knockback = direction * knockBackForce;
                    damagableObject.OnHit(damage, knockback);
                }
               
            }
            Destroy(gameObject);
            
            
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
