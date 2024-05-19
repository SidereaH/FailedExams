using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
public class  Slime: MonoBehaviour
{
    [SerializeField] EnemyType enemyType;
    public float damage = 1f;
    public float knockbackForce = 100f;
    public DetectionZone detectionZone;
    public float moveSpeed = 500f;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    DamageableCharacter character;
    Animator animator;
    bool isRunning = false;
    [SerializeField] enum EnemyType { MeleeEnemy, RangeEnemy };
    [SerializeField] float distanceToShoot;
    public bool IsRunning
    {
        set
        {
            isRunning = value;
            animator.SetBool("isRunning", isRunning);
        }
        get
        {
            return isRunning;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        character = rb.GetComponent<DamageableCharacter>();
        animator = rb.GetComponent<Animator>();
    }
    private void FixedUpdate()
        
    {


        if (character.Targetable && detectionZone.detectedObjs.Count > 0)
        {
            
            
            //расчет направления между объектом и нами
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            //следовать за обнаруженным объектом
            if(enemyType.ToString() == "RangeEnemy")
            {
                if (Vector2.Distance(detectionZone.detectedObjs[0].transform.position, transform.position) > distanceToShoot)
                {
                    IsRunning = true;
                    rb.AddForce(moveSpeed * direction * Time.deltaTime);
                }
            }
            else
            {
                IsRunning = true;
                rb.AddForce(moveSpeed * direction * Time.deltaTime);
            }
            
            
            
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
                
            }
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            IsRunning = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();
        IDamageable me = gameObject.GetComponent<IDamageable>();


        if (damageable != null)
        {
            while(damageable.Invincible == false)
            {
                 Vector2 direction = (collider.transform.position - transform.position).normalized;
                 Vector2 knockback = direction * knockbackForce;
                 
                 damageable.OnHit(damage, knockback);
                 me.OnHit(0, -knockback);
            }
           
        }
    }
}
