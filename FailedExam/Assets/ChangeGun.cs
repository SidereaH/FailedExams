using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ChangeGun : MonoBehaviour
{
    Collider2D col;
    bool inTrigger = false;
    Collider2D collision;
    DetectEnemies detectEnemies;
    
    // Start is called before the first frame update
    GunManager gunManager;
    void Start()
    {
        col = GetComponent<Collider2D>();
        gunManager = GameObject.FindGameObjectWithTag("GunManager").GetComponent<GunManager>();
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
                    bool moreThanOne;
                    if (collision.gameObject.transform.childCount >= 2)
                    {
                        moreThanOne = true;
                        GameObject oldGun = collision.gameObject.transform.GetChild(1).gameObject;
                        oldGun.GetComponent<SwordAttack>().pickDown();
                        oldGun.transform.GetComponent<SpriteRenderer>().enabled = true;
                        Instantiate(oldGun, gameObject.transform);
                        Destroy(collision.gameObject.transform.GetChild(1).gameObject);
                    }
                    else
                    {
                        moreThanOne = false;
                    }

                    GameObject newObj = Instantiate(tempGun, collision.gameObject.transform);
                    newObj.GetComponent<SwordAttack>().pickUp();
                    newObj.name = "Gun";
                    gunManager.ChangeGun(newObj.transform.GetChild(1).gameObject);
                    Destroy(gameObject.transform.GetChild(0).gameObject);
                    if (moreThanOne == false)
                    {
                        Destroy(gameObject);
                    }
                    collision.gameObject.GetComponent<Player>().pickUpGun();

                }
                else
                {
                    GameObject tempGun = gameObject.transform.GetChild(0).gameObject;
                    bool moreThanOne;
                    if(collision.gameObject.transform.childCount >=2)
                    {
                        moreThanOne = true;
                        GameObject oldGun = collision.gameObject.transform.GetChild(1).gameObject;
                        GameObject oldObj = Instantiate(oldGun, gameObject.transform);
                        oldObj.GetComponent<SwordAttack>().pickDown();
                        Destroy(collision.gameObject.transform.GetChild(1).gameObject);
                    }
                    else
                    {
                        
                        moreThanOne = false;

                    }

                    GameObject newObj = Instantiate(tempGun, collision.gameObject.transform);
                    newObj.GetComponent<SwordAttack>().pickUp();
                    newObj.name = tempGun.name;
                    gunManager.ChangeGun(newObj.transform.GetChild(1).gameObject);
                    Destroy(gameObject.transform.GetChild(0).gameObject);
                    if(moreThanOne == false)
                    {
                        Destroy(gameObject);
                    }
                    collision.gameObject.GetComponent<Player>().pickUpGun();
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
