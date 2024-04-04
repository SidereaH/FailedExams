using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    public float moveSpeed = 500f;
    public float knockForce = 100f;
    //public DetectionZone detectionZone;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //damagebleCharacter = GetComponent(<DamageableCharacter>());
    }
    public float Health {
        set {
            health = value;
            print(health);
            if(health <= 0) {
                Defeated();
            }
        }
        get {
            return health;
        }
    }

    public float health = 1;

    

    public void Defeated(){
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy() {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
       // if (damagebleCharacter.Targetable && detectionZone.detectionObjs.Count > 0)
        //{
        //    Vector2 direction = (detectionZone.detectionObjs[0].transform.position - transform.position).normalized;
        //    rb.AddForce(direction * moveSpeed * Time.deltaTime);
       // }
        
        
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Vector2 direction = (collider.transform.position - transform.position).normalized;

            Vector2 knockback = direction * knockForce;
            //damageable.OnHit(damage, knockback);
        }
    }
}
