using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using TMPro;
public class DamageableCharacter : MonoBehaviour, IDamageable
{
    public GameObject healthText;
    public bool disableSimulation = false;
    Animator animator;
    Rigidbody2D rb;
    public bool unkillable = false;
    public float knockForce = 100f;
    Collider2D physicsCollider;
    public DetectionZone detectionZone;

    public float _health = 3;
    public bool _targetable = true;
    //public int timeForDestroy = 10;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        physicsCollider = GetComponent<Collider2D>();
        //damagebleCharacter = GetComponent(<DamageableCharacter>());
        
    }
    public float Health
    {
        set
        {
            if(value < _health)
            {
                animator.SetTrigger("hit");
                RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);
            }
            _health = value;

            


            if (_health <= 0)
            {
                animator.SetBool("isAlive", false);
                Targetable = false;
            }
            else
            {
                animator.SetBool("isAlive", true);
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
            if (disableSimulation)
            {
                rb.simulated = false;
            }
           
            physicsCollider.enabled = value;
        }

    }

    public void OnHit(float damage)
    {
        if(unkillable == false)
        {
            Health -= damage;
        }
        
       
    }
    public void OnHit(float damage, Vector2 knockback)
    {
        if (unkillable == false)
        {
            Health -= damage;
            //apply force to the bat
            rb.AddForce(knockback, ForceMode2D.Impulse);
            Debug.Log("Force" + knockback);
        }
       
    }
    public void OnObjectDestroyed()
    {
        /*Timer timer = new Timer(timeForDestroy);   
        timer.Elapsed += OnTimerDeath;  
        timer.AutoReset = false;
        timer.Start();
        Debug.Log("timer start");
        */
        Destroy(gameObject);

    }
    private void OnTimerDeath(object sender, ElapsedEventArgs e)
    {


        /*Timer timer = (Timer)sender;
        timer.Stop();*/


    }





    void FixedUpdate()
    {
       



    }


   


}
