
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class DamageableCharacter : MonoBehaviour, IDamageable
{
    public GameObject healthText;
    public bool disableSimulation = false;
    Animator animator;
    Rigidbody2D rb;
    public bool unkillable = false;
    public float knockForce = 100f;
    Collider2D physicsCollider;
    //public DetectionZone detectionZone;
    public float _maxHealth = 3;
    public float _health = 3;
    bool _targetable = true;
    public float invincibilityTime = 1f;
    public bool canTurnInvincible = false;
    //public int timeForDestroy = 10;
    public bool _invincible = false;
    private float invincibleTimeElapsed = 1;
    TextMeshProUGUI textValue;
    public Slider slider;
    public ScoreManager scoreManager;
    public GameObject panel;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        physicsCollider = GetComponent<Collider2D>();
        //damagebleCharacter = GetComponent(<DamageableCharacter>());
        textValue = healthText.GetComponent<TextMeshProUGUI>();
        if(gameObject.tag == "Player")
        {
            slider.maxValue = _maxHealth;
        }
        
    }
    public bool Invincible
    {
        get
        {
            return _invincible;

        }
        set
        {
            _invincible = value;

            if (_invincible == true)
            {
                invincibleTimeElapsed = 1f;

            }

           
            
        }
    }
    public float Health
    {
        set
        {
            if(value > _health)
            {
                
                    animator.SetTrigger("heal");
                    slider.value = value;
               
            }
            else if(value < _health)
            {
                animator.SetTrigger("hit");
                textValue.text = (_health - value).ToString();
                RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);
                    if(slider != null )
                    {
                        slider.value = value;
                    }
            }
            _health = value;
            if (_health <= 0)
            {
                animator.SetBool("isAlive", false);
                Targetable = false;
                rb.simulated = false;
                if(slider != null)
                {
                    slider.value = 0;
                }
                if(panel != null)
                {
                    panel.SetActive(true);
                }

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
        if (!Invincible)
        {
            if (unkillable == false)
            {
                Health -= damage;
                if (canTurnInvincible == true)
                {
                    //включение задержки
                    Invincible = true;
                   
                }
            }

        }   
    }
    public void OnHeal(float heal, GameObject healBot)
    {
        if(_health != _maxHealth)
        {
            _health += heal;
            slider.value = _health;
            Destroy(healBot);

        }        
    }
    public void addKill()
    {
        scoreManager.addKill();
        Destroy(gameObject);
    }
    public void OnHit(float damage, Vector2 knockback)
    {
        if(!Invincible) {
            if (unkillable == false)
            {
                Health -= damage;
                //apply force to the bat
                rb.AddForce(knockback, ForceMode2D.Impulse);
                

                if (canTurnInvincible == true)
                {
                    //включение задержки
                    Invincible = true;
                   
              
                }
            }
        }
        
       
    }
    public void OnObjectDestroyed()
    {
        Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        if (Invincible == true)
        {
            invincibleTimeElapsed += Time.deltaTime;

            if(invincibleTimeElapsed > invincibilityTime)
            {
                Invincible = false;
            }
        }
    }
    public float getCurHealth()
    {
        float curHealth = Health;
        return curHealth;
    }
}
