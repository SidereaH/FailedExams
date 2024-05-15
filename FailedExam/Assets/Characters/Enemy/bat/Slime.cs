using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
public class  Slime: MonoBehaviour
{
    public float damage = 1f;
    public float knockbackForce = 100f;
    public DetectionZone detectionZone;
    public float moveSpeed = 500f;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    DamageableCharacter character;
    
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        character = rb.GetComponent<DamageableCharacter>();
    }
    private void FixedUpdate()
        
    {


        if (character.Targetable && detectionZone.detectedObjs.Count > 0)
        {

            
            //расчет направления между объектом и нами
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            //следовать за обнаруженным объектом
            rb.AddForce(moveSpeed * direction * Time.deltaTime);
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
                
            }
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();
        IDamageable me = gameObject.GetComponent<IDamageable>();


        if (damageable != null)
        {
            if(damageable.Invincible == false)
            {
                 Vector2 direction = (collider.transform.position - transform.position).normalized;
                 Vector2 knockback = direction * knockbackForce;
                 damageable.OnHit(damage, knockback);
                 me.OnHit(0, -knockback);
            }
           
        }
    }
}
