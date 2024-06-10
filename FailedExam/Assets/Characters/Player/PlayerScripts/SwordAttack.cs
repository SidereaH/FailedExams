
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] TargetType targetType;
    Animator gunanimator;
    public GunType guntype;
    Collider2D swordCollider;

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
    public GameObject[] soundsShot;
    public bool inPlayer;
    [SerializeField] enum TargetType { Player, EnemyBoss };
    GameObject target;
    Slime enemySlime;
    Player playerScr;
    Animator parentAnimator;

    [SerializeField] GameObject swordAttack;
    AttackInside attackinsideSword;
    SpriteRenderer attackRenderer;
    //Collider2D attackinsideSwordCol;


    public enum GunType {Default, Enemy};

    public float randRacbros;
    private void Start() {
        if(swordAttack != null)
        {
            attackinsideSword = swordAttack.transform.GetComponent<AttackInside>();
            attackRenderer = swordAttack.transform.GetComponent<SpriteRenderer>();  
           // attackinsideSwordCol = swordAttack.transform.transform.GetComponent<Collider2D>();
        }

        gunanimator = GetComponent<Animator>();
        if(transform.parent.tag == "Player")
        {
            playerScr = gameObject.transform.parent.GetComponent<Player>();
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
            
            swordCollider = GetComponent<Collider2D>();
            
            if (gameObject.tag != "SwordGun")
            {
                shotPoint = transform.GetChild(0).GetComponent<Transform>();
            }

            playerAnimator = player.GetComponent<Animator>();
        }
        else if(player.tag == "Enemy")
        {
            parentAnimator = transform.parent.GetComponent<Animator>();
            enemySlime = player.GetComponent<Slime>();
            swordCollider = GetComponent<Collider2D>();
            
            
            target = GameObject.FindGameObjectWithTag(targetType.ToString());
            if (gameObject.tag != "SwordGun")
            {
                shotPoint = transform.GetChild(0).GetComponent<Transform>();
            }
        }
       
    }
    void Update()

    {
        
        if(player.tag == "Player" && GunType.Default == guntype && playerScr.isActiveMenu == false)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            if (rotZ > 90f)
            {
                spriteRenderer.flipY = true;
                if(swordAttack != null)
                {
                    attackRenderer.flipY = true;
                }
                
            }
            else if (rotZ < -90f && rotZ > -180f)
            {
                spriteRenderer.flipY = true;
                if (swordAttack != null)
                {
                    attackRenderer.flipY = true;
                }
                
            }
            else if (rotZ > -90f)
            {
                spriteRenderer.flipY = false;
                if (swordAttack != null)
                {
                    attackRenderer.flipY = false;
                }
                
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
                    parentAnimator.SetTrigger("Attack");
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
                RangeAttack();
            }
            else
            {
                gunanimator.SetBool("isAttacking", true);
                
                attackinsideSword.Attack();
            }         
        }  
    }
    public void StateOfCollider()
    {
        Debug.Log(spriteRenderer.enabled);
    }
    public void RangeAttack()
    {
        if (player.tag == "Player")
        {
            float razbr = Random.Range(0, randRacbros);
            
            GameObject soundShot = soundsShot[Random.Range(0, soundsShot.Length)];
            GameObject _temp = Instantiate(soundShot, transform.position, Quaternion.identity);
            _temp.GetComponent<AudioSource>().Play();
            Destroy(_temp, 1);
            Instantiate(effect, shotPoint.position, Quaternion.identity);
            Instantiate(bullet, shotPoint.position, transform.rotation);
            isAttacking = false;
        }
        else
        {

            GameObject soundShot = soundsShot[Random.Range(0, soundsShot.Length)];
            GameObject _temp = Instantiate(soundShot, transform.position, Quaternion.identity);
            _temp.GetComponent<AudioSource>().Play();
            Destroy(_temp, 1);
            //Instantiate(effect, shotPoint.position, Quaternion.identity);
            Instantiate(bullet, shotPoint.position, transform.rotation);
            isAttacking = false;
        }
        
    }
    public void pickDown()
    {

        inPlayer = false;
        gameObject.GetComponent<Animator>().SetBool("isPicked", false);
        transform.Rotate(0, 0, 0);
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }
    public void pickUp()
    {

        inPlayer = true;
        gameObject.GetComponent<Animator>().SetBool("isPicked", true);
        player = transform.GetComponentInParent<Transform>().gameObject;

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
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;

        gameObject.transform.parent.GetComponent<Player>().pickUpGun();


        //swordCollider.enabled=true;
    }
    public void StopAttack() {
        gunanimator.SetBool("isAttacking", false);
    }
    /*
    private void OnTriggerEnter2D(Collider2D other) {
       
        if (gameObject.tag == "SwordGun")
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
    */
    public float getStartTime()
    {
        return startTimeBtwShots;
    }
}
