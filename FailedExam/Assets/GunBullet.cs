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
                    damagableObject.OnHit(damage);
                    
                }
               
            }
            Destroy(gameObject);
            
            
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
