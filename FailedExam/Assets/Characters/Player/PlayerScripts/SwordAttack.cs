using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] TargetType targetType;
    Animator gunanimator;
    public GunType guntype;
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
    [SerializeField] bool isEnemy;
    public bool inPlayer;
    [SerializeField] enum TargetType { Player, EnemyBoss };
    GameObject target;
    Slime enemySlime;
    public enum GunType {Default, Enemy};
    private void Start() {
        gunanimator = GetComponent<Animator>();
        if(transform.parent.tag == "Player")
        {
            inPlayer = true;
            gunanimator.SetBool("isPicked", true);
            pickUp();
        }
        else if(transform.parent.tag =="GunHolder")
        {
            inPlayer = false;
            gunanimator.SetBool("isPicked", false);
            pickDown();
        }
        player = transform.parent.gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (player.tag == "Player")
        {
            isEnemy = false;
            swordCollider = GetComponent<Collider2D>();
            
            if (gameObject.tag != "SwordGun")
            {
                shotPoint = transform.GetChild(0).GetComponent<Transform>();
            }

            playerAnimator = player.GetComponent<Animator>();
        }
        else if(player.tag == "Enemy")
        {
            enemySlime = player.GetComponent<Slime>();
      
            isEnemy = true;
            
            target = GameObject.FindGameObjectWithTag(targetType.ToString());
            if (gameObject.tag != "SwordGun")
            {
                shotPoint = transform.GetChild(0).GetComponent<Transform>();
            }
        }
       
    }
    void Update()

    {
        
        if(player.tag == "Player" && GunType.Default == guntype)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            if (rotZ > 90f)
            {
                spriteRenderer.flipY = true;
            }
            else if (rotZ < -90f && rotZ > -180f)
            {
                spriteRenderer.flipY = true;
            }
            else if (rotZ > -90f)
            {
                spriteRenderer.flipY = false;
            }
            if (playerAnimator.GetBool("isSafety") == false)
            {
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
        else if(player.tag == "Enemy")
        {
            
            Vector3 difference = target.transform.position - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            if (rotZ > 90f)
            {
                spriteRenderer.flipY = true;
            }
            else if (rotZ < -90f && rotZ > -180f)
            {
                spriteRenderer.flipY = true;
            }
            else if (rotZ > -90f)
            {
                spriteRenderer.flipY = false;
            }

            if (enemySlime.IsRunning ==true)
            {
                if (timeBtwShots <= 0)
                {
                   
                    canAttack = true;
                  
                    Attack();
                    timeBtwShots = startTimeBtwShots;
                    
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                    canAttack = false;
                }
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
    public void pickDown()
    {
        Debug.Log("pickedDown");
        inPlayer = false;
        gameObject.GetComponent<Animator>().SetBool("isPicked", false);
        transform.Rotate(0, 0, 0);
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    public void pickUp()
    {
        Debug.Log("pickedUp");
        inPlayer = true;
        gameObject.GetComponent<Animator>().SetBool("isPicked", true);
        player = transform.GetComponentInParent<Transform>().gameObject;
        Debug.Log(player.tag);
        /*
            if (player.transform.GetChild(0).GetComponent<DetectEnemies>().isDanger == true)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        */

        gameObject.GetComponent<Collider2D>().enabled = true;


        //swordCollider.enabled=true;
    }
    public void StopAttack() {
        //swordCollider.enabled = false;
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
