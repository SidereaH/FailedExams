using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  Slime: MonoBehaviour, IDamageable
{
    Animator animator;
    Rigidbody2D rb;
    public float moveSpeed = 500f;
    public float knockForce = 100f;
    //public DetectionZone detectionZone;

    public float _health = 10;
    public bool _targetable = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //damagebleCharacter = GetComponent(<DamageableCharacter>());
    }
    public float Health {
        set {
            _health = value;
            
            animator.SetTrigger("hit");

            if (_health <= 0) {
                animator.SetBool("isAlive", false);
                Targetable = false;
            }
            else
            {
                animator.SetBool("isAlive", true);
            }
        }
        get {
            return _health;
        }
    }
    public bool Targetable
    {
        get {
            return _targetable;
        }

        set{
            _targetable = value;
            rb.simulated = value;
        }
        
    }

    public void OnHit(float damage)
    {
        Health -= damage;
    }
    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        //apply force to the bat
        rb.AddForce(knockback);
        Debug.Log("Force" + knockback);
    }
    public void Defeated(){
     
        
        
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
