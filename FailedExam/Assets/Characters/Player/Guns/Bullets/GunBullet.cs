using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public int damage;
    public float distance;
    public LayerMask whatIsSolid;
    public float knockBackForce = 1f;
    [SerializeField] GameObject soundHeadshot;
    [SerializeField] GameObject soundBodyshot;
    Collider2D parentCol;
    [SerializeField] bool enemyBulet;
    private void Update()
    {
        
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
            if (hitInfo.collider != null)
            {

                if (enemyBulet == false)
                 {
                    parentCol = hitInfo.collider.GetComponentInParent<Collider2D>();
                    if (parentCol.CompareTag("Enemy"))
                    {
                       
                        DamageableCharacter damagableObject = hitInfo.collider.GetComponentInParent<DamageableCharacter>();
                        if (damagableObject != null)

                        {


                            Vector3 parentPosition = transform.root.position;
                            Vector2 direction = (hitInfo.collider.transform.position - parentPosition).normalized;
                            Vector2 knockback = direction * knockBackForce;



                            if (hitInfo.collider.name == "Head")
                            {

                                GameObject _temp = Instantiate(soundHeadshot, transform.position, Quaternion.identity);
                                damagableObject.OnHit(damage * 2, knockback);

                                Destroy(_temp, 1);

                            }
                            else if (hitInfo.collider.name == "Body")
                            {
                                //GameObject _temp = Instantiate(soundBodyshot, transform.position, Quaternion.identity);
                                //Destroy(_temp, 2);

                                damagableObject.OnHit(damage * 1, knockback);

                            }
                            else
                            {

                                //GameObject _temp = Instantiate(soundBodyshot, transform.position, Quaternion.identity);
                                //Destroy(_temp, 2);
                                damagableObject.OnHit(damage * 0.5f, knockback);

                            }
                            Destroy(gameObject);




                        }
                    }
                }
                if(enemyBulet == true)
                {
                    
                    DamageableCharacter damagableObject = hitInfo.collider.GetComponentInParent<DamageableCharacter>();
                    if (damagableObject != null)

                    {
                        

                        Vector3 parentPosition = transform.root.position;
                        Vector2 direction = (hitInfo.collider.transform.position - parentPosition).normalized;
                        Vector2 knockback = direction * knockBackForce;

                        damagableObject.OnHit(damage, knockback);
                        /*
                         if (hitInfo.collider.name == "Head")
                         {

                             GameObject _temp = Instantiate(soundHeadshot, transform.position, Quaternion.identity);
                             damagableObject.OnHit(damage * 2, knockback);

                             Destroy(_temp, 1);

                         }
                         else if (hitInfo.collider.name == "Body")
                         {
                             //GameObject _temp = Instantiate(soundBodyshot, transform.position, Quaternion.identity);
                             //Destroy(_temp, 2);

                             damagableObject.OnHit(damage * 1, knockback);

                         }
                         else
                         {

                             //GameObject _temp = Instantiate(soundBodyshot, transform.position, Quaternion.identity);
                             //Destroy(_temp, 2);
                             damagableObject.OnHit(damage * 1f, knockback);

                         }
                        */
                        Destroy(gameObject);
                    }
                }
                
                Destroy(gameObject);
            }
        
        
        
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
