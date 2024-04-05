using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
      
    public GameObject healthText;
    public bool disableSimulation = false;
    public bool canTurnInvincable = false;
    public float invincibilityTime = 0.25f;
    Animator animator;
    Rigidbody2D rb;
    Collider2D physCollider;
    bool isAlive = true;
    private float invincibleTimeElapsed = 0f;
    public float _health = 3;
    public bool _targetable = true;
    public bool _invincible = false;
    public float Health
    {
        set
        {
            if (value < _health)
            {
                animator.SetTrigger("hit");

                RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();

                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                Canvas canvas = GameObject.FindObjectOfType<Canvas>();

                textTransform.SetParent(canvas.transform);
            }
            _health = value;

            if(_health <= 0)
            {
                animator.SetBool("isAlive", false);
                Targetable = false;

            }
        }
        get
        {
            return _health;
        }
    }

    public bool Targetable
    {
        get
        {

            return _targetable;
        }
        set
        {
            _targetable = value;
            if ((disableSimulation))
            {
                rb.simulated = false;   
            }
            
            physCollider.enabled = value;
        }
    }

    public bool Invincible {
        get
        {
            return _invincible;

        }
        set
        {
            _invincible = value;
            if(_invincible == true)
            {
                invincibleTimeElapsed = 0f;
            }
        }
 
       
    }

    public void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive",true);
        rb = GetComponent<Rigidbody2D>(); 
        physCollider = GetComponent<Collider2D>();
    }
    public void OnHit(float damage, Vector2 knockback) //with knockback
    {
        if (!Invincible)
        {
            Health-=damage;

            //apply force for enemy
            //impulse for forces
            rb.AddForce(knockback, ForceMode2D.Impulse);
            if (canTurnInvincable)
            {
                Invincible = true;
            }

        }
    }
    //without knockback

    public void OnHit(float damage)
    {
        if (!Invincible)
        {
            Health -= damage;
            if (canTurnInvincable)
            {
                Invincible = true;
            }
        }
    }
    public void OnObjectDestroyed()
    {
        Destroy(gameObject);
    }
    public void FixedUpdate()
    {
            if(Invincible)
        {
            invincibleTimeElapsed += Time.deltaTime;
            if(invincibleTimeElapsed > invincibilityTime)
            {
                Invincible = false;

            }
        }

    }
}
