using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Takes and handles input and movement for a player character
public class Player : MonoBehaviour
{
    public float moveSpeed = 500f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    [SerializeField] GameObject stepPrefab;
    [SerializeField] int time, moveTime;
    [SerializeField] GameObject slimeStepPref;

    [SerializeField] ScoreManager score;
    //����� SCRIPT SWORD ATTACK ��������� ���������� ������ � ���� SWORDATTACK
    SwordAttack swordAttack;
    public float maxSpeed = 8f;
    public float idleFriction = 0.9f;
    Vector2 movementInput = Vector2.zero;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator gunAnimator;
    GameObject gun;
    SpriteRenderer gunRenderer;
    Transform gunObject;
    private float _timeBtwShots;
    bool inSlime = false;
    DamageableCharacter character;
    DetectEnemies detectEnemies;

    bool IsRunning
    {
        set
        {
            isRunning = value;
            animator.SetBool("isRunning", isRunning);
        }
    }

    bool isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("lastScene", SceneManager.GetActiveScene().name);
        Debug.Log(PlayerPrefs.GetString("lastScene"));
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();     
        spriteRenderer = GetComponent<SpriteRenderer>();  
        animator.SetBool("isSafety", true);
        detectEnemies = gameObject.transform.GetChild(0).GetComponent<DetectEnemies>();
        character = gameObject.GetComponent<DamageableCharacter>();
    }
    void Update()
    {
        if (movementInput != Vector2.zero && character.Health > 0)
        {
            if (time == 0)
            {
                time = moveTime;
                if (inSlime == true)
                {
                    GameObject _temp = Instantiate(slimeStepPref, transform.position, Quaternion.identity);
                    _temp.GetComponent<AudioSource>().Play();
                    Destroy(_temp, 1);
                }
                else
                {
                    GameObject _temp = Instantiate(stepPrefab, transform.position, Quaternion.identity);
                    _temp.GetComponent<AudioSource>().Play();
                    Destroy(_temp, 1);
                }
            }


        }
    }
    private void FixedUpdate()
    {
        gun = transform.GetChild(1).gameObject;
        gunAnimator = gun.GetComponent<Animator>();
        gunRenderer = gun.GetComponent<SpriteRenderer>();
        swordAttack = gun.GetComponent<SwordAttack>();

        if (time > 0)
        {
            time--;
        }
        // If movement input is not 0, try to move
        //�������� ������ � ��� �����������
        if (movementInput != Vector2.zero && character.Health > 0)
        {
            //rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed);

            rb.AddForce(movementInput * moveSpeed * Time.deltaTime);
            if (rb.velocity.magnitude > maxSpeed)
            {

                float limitedSpeed = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, idleFriction);
                rb.velocity = rb.velocity.normalized * limitedSpeed;
            }
            //������� ����������� �����, ����
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
                //gunRenderer.flipX = true;
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
                //gunRenderer.flipX = false;

            }

            IsRunning = true;

        }
        else
        {
            //���� ��� �������� -> ������������ �������� ������������ � ����
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
            IsRunning = false;
        }



    }



    //�������� �������� �� ����� ������� ��� ��������
    void OnMove(InputValue movementValue)
    {

        movementInput = movementValue.Get<Vector2>();
    }

    public void addGold(int goldCost){
        score.addGold(goldCost);
    }
    void OnAttack()
    {
        if (animator.GetBool("isSafety") == false)
        {
            swordAttack.Attack();
        }

    }

    public void getSlowed()
    {
        maxSpeed = 1f;
        moveSpeed = 100f;
        inSlime = true;
    }
    public void getUnSlowed()
    {
        maxSpeed = 5f;
        moveSpeed = 500f;
        inSlime = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Dialogue")
            {
                rb.simulated = false;
            }
        }
    }

}
