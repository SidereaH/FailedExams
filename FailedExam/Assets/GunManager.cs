using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    Image image;
    Sprite gunSprite;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        image = transform.GetComponent<Image>();
        try{
            if(player.transform.GetChild(1) != null)
        {
            gunSprite = player.transform.GetChild(1).transform.GetComponent<SpriteRenderer>().sprite;
            image.sprite = gunSprite;
        }
        }
        catch{
            Debug.Log("нету");
        }
        
    }

    // Update is called once per frame
    public void ChangeGun(GameObject gun)
    {
        gunSprite = gun.transform.GetComponent<SpriteRenderer>().sprite;
        image.sprite = gunSprite;
    }
}
