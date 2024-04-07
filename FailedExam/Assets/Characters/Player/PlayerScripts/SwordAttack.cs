using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    public float damage = 1f;
    Vector2 rightAttackOffset;
    public Vector3 gunRight = new Vector3(1, -0.9f, 0);
    public float knockBackForce = 500f;

    private void Start() {
        rightAttackOffset = transform.position;
    }

    public void AttackRight() {
        
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
        gameObject.transform.position = gunRight;
    }

    public void AttackLeft() {
        
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);

    }

    public void StopAttack() {
        swordCollider.enabled = false;
    }
    public void EndSwordAttack()
    {
        StopAttack();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        IDamageable damagableObject = (IDamageable) other.GetComponent<IDamageable>();
        if ( damagableObject != null)
        {
            print("hit");
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
            Vector2 direction = (Vector2)(other.gameObject.transform.position - parentPosition ).normalized;
            Vector2 knockback = direction * knockBackForce;

            /*if (other.tag == "Enemy") {
                // Deal damage to the enemy
                Enemy enemy = other.GetComponent<Enemy>();

                if(enemy != null) {
                    enemy.Health -= damage;


                }
            }*/
            //other.SendMessage("OnHit", damage, knockback);
            damagableObject.OnHit(damage, knockback);
        }
        else
        {
            Debug.LogWarning("Collider does not implement IDamageable");
        }
        
        

        


        /*Vector2 CalculateKnockBack()
        {
            GameObject character = gameObject.GetComponentInParent<GameObject>();
        }
        */
    }
    
}
