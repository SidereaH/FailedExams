
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float damage = 1f;
    [SerializeField] float knockbackForce = 100f;
    IDamageable damageable;
    Collider2D collider;
    Animator animator;
    [SerializeField] GameObject[] soundBooms;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            collider = collision;
            if (collision.gameObject.tag != "Enemy")
            {
                if (collision.gameObject.tag == "Player")
                {
                    damageable = collision.GetComponent<IDamageable>();
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            collider = null;
            damageable = null;

        }
        
    }
    public void Boom()
    {     
        animator.SetTrigger("Boom");
        GameObject _tempsound = soundBooms[Random.Range(0, soundBooms.Length)];
        GameObject _temp = Instantiate(_tempsound, transform.position, Quaternion.identity);
        if (damageable != null)
        {
           
            Vector2 direction = (collider.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;    
            damageable.OnHit(damage, knockback);
            
        }
        Destroy(_temp, 1);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
