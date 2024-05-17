using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordAttack : MonoBehaviour
{
    Collider2D swordCollider;
    public float damage = 1f;
    public float knockBackForce = 15f;
    SpriteRenderer spriteRenderer;
    public float offset;
    public GameObject bullet;
    Transform shotPoint;
    private float timeBtwShots = 0;
    GameObject player;
    public float startTimeBtwShots;
    Animator playerAnimator;
    public bool canAttack;
    public bool isAttacking = false;
    public GameObject effect;
    public ScoreManager scoreManager;
    public GameObject soundShot;


    private void Start() { 
        swordCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(gameObject.tag != "SwordGun")
        {
            shotPoint = transform.GetChild(0).GetComponent<Transform>();
        }
        player = transform.parent.gameObject;
        playerAnimator = player.GetComponent<Animator>();
       
    }
    void Update()

    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        if(rotZ > 90f )
        {
            spriteRenderer.flipY=true;
        }
        else if(rotZ < -90f && rotZ > -180f)
        {
            spriteRenderer.flipY = true;
        }
        else if(rotZ > -90f)
        {
            spriteRenderer.flipY = false;
        }
        if(playerAnimator.GetBool("isSafety") == false) {
            if (timeBtwShots <= 0)
            {
                canAttack = true;

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    
                    timeBtwShots = startTimeBtwShots;
                }
            }
            else
            {
               
                timeBtwShots -= Time.deltaTime;
                canAttack = false;
            }

        }
    }

    public void Attack() {
        isAttacking = true;
        if (canAttack == true)
        {
            if(gameObject.tag != "SwordGun")
            {
                GameObject _temp = Instantiate(soundShot, transform.position, Quaternion.identity);
                _temp.GetComponent<AudioSource>().Play();
                Destroy(_temp, 1);
                Instantiate(effect, shotPoint.position, Quaternion.identity);
                Instantiate(bullet, shotPoint.position, transform.rotation);
                isAttacking = false;
            }
            else
            {
                swordCollider.enabled = true;
            }         
        }  
    }

    public void StopAttack() {
        swordCollider.enabled = false;
    }
  

   

    private void OnTriggerEnter2D(Collider2D other) {
        if(gameObject.tag == "SwordGun")
        {
            IDamageable damagableObject = (IDamageable)other.GetComponent<IDamageable>();
            if (damagableObject != null)
            {
                Vector3 parentPosition = transform.parent.position;
                Vector2 direction = (other.transform.position - parentPosition).normalized;
                Vector2 knockback = direction * knockBackForce;
                damagableObject.OnHit(damage, knockback);
                StopAttack();
            }
        }
    }
    public float getStartTime()
    {
        return startTimeBtwShots;
    }
    
}
