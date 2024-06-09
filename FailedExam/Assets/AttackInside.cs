using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class AttackInside : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer renderer;
    Animator animator;
    Collider2D collider;
    SwordAttack sword;
    public float damage = 1f;
    public float knockBackForce = 15f;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        sword = transform.GetComponentInParent<SwordAttack>();
        animator = GetComponent<Animator>();
        collider = transform.GetComponentInParent<Collider2D>();
    }

    public void Attack()
    {
        renderer.enabled = true;
        collider.enabled = true;
        animator.SetTrigger("Attack");
    }
    public void StopAttack()
    {
        collider.enabled = false;
        renderer.enabled = false;
       // sword.StateOfCollider();
        sword.StopAttack();
        //sword.StateOfCollider();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (gameObject.tag == "SwordGun")
        {
           
            if(other.tag != "Player")
            {
                IDamageable damagableObject = (IDamageable)other.GetComponent<IDamageable>();
                if (damagableObject != null)
                {

                    Vector3 parentPosition = transform.parent.position;
                    Vector2 direction = (other.transform.position - parentPosition).normalized;
                    Vector2 knockback = direction * knockBackForce;
                    damagableObject.OnHit(damage, knockback);
                }
            }
            
        }
    }
}
