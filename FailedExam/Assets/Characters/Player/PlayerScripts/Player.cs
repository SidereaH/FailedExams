using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Takes and handles input and movement for a player character
public class Player : MonoBehaviour
{
    public float moveSpeed = 500f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    //ЧТОБЫ SCRIPT SWORD ATTACK ЗАРАБОТАЛ ПЕРЕТАЩИТЕ ОРУЖИЕ В ПОЛЕ SWORDATTACK
    SwordAttack swordAttack;
    public float maxSpeed = 8f;
    public float idleFriction = 0.9f;
    Vector2 movementInput = Vector2.zero;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator gunAnimator;
    SpriteRenderer gunRenderer;
    Transform gunObject;
    

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
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gunAnimator = transform.GetChild(0).GetComponent<Animator>();
        gunRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        swordAttack = transform.GetChild(0).gameObject.GetComponent<SwordAttack>();
        animator.SetBool("isSafety", true);
       
    }
    private void FixedUpdate()
    {
       
            // If movement input is not 0, try to move
           //ускоряем игрока в его направлении
            if (movementInput != Vector2.zero)
            {
            //rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed);
             rb.AddForce(movementInput * moveSpeed * Time.deltaTime); 
                if(rb.velocity.magnitude > maxSpeed)
            {
              
                float limitedSpeed = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, idleFriction);
                rb.velocity = rb.velocity.normalized * limitedSpeed;
            }
            //контрол направление право, лево
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
            //если нет движения -> интерполяция скорости приближается к нулю
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
                IsRunning = false;
            }

            
        
    }

    /*private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // Check for potential collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            // Can't move if there's no direction to move in
            return false;
        }

    }*/

    //получаем значения из инпут система для движения
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
 

    void OnAttack()
    {
        gunAnimator.SetTrigger("isAttack");
        SwordAttack();
    }

    public void SwordAttack()
    {
        
            swordAttack.Attack();
     
        
    }
    public void getSlowed()
    {
        maxSpeed = 1f;
        moveSpeed = 100f;
    }
    public void getUnSlowed()
    {
        maxSpeed = 5f;
        moveSpeed = 500f;

    }

    
    
    
    
}
