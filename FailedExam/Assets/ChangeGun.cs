using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeGun : MonoBehaviour
{
    Collider2D col;
    bool inTrigger = false;
    Collider2D collision;
    DetectEnemies detectEnemies;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (inTrigger)
        {
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                
                detectEnemies = collision.transform.GetChild(0).GetComponent<DetectEnemies>();

                if (detectEnemies.isDanger == false)
                {
                    GameObject tempGun = gameObject.transform.GetChild(0).gameObject;

                    tempGun.GetComponent<SwordAttack>().pickUp();
                    GameObject oldGun = collision.gameObject.transform.GetChild(1).gameObject;
                    oldGun.GetComponent<SwordAttack>().pickDown();
                    oldGun.transform.GetComponent<SpriteRenderer>().enabled = true;
                    tempGun.transform.GetComponent<SpriteRenderer>().enabled = false;
                    Instantiate(oldGun, gameObject.transform);
                    Instantiate(tempGun, collision.gameObject.transform);
                    Destroy(oldGun);
                    Destroy(gameObject.transform.GetChild(0).gameObject);
                }
                else
                {
                    GameObject tempGun = gameObject.transform.GetChild(0).gameObject;

                    GameObject oldGun = collision.gameObject.transform.GetChild(1).gameObject;
                    
                    GameObject oldObj = Instantiate(oldGun, gameObject.transform);
                    GameObject newObj = Instantiate(tempGun, collision.gameObject.transform);
                    newObj.GetComponent<SwordAttack>().pickUp();
                    oldObj.GetComponent<SwordAttack>().pickDown();
                    
                    tempGun.name = "Gun";
                    Destroy(gameObject.transform.GetChild(0).gameObject);
                    Destroy(collision.gameObject.transform.GetChild(1).gameObject);
                }


            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inTrigger = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                inTrigger = true;
                this.collision = collision;
            }
        }
    }
}
