using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    GameObject imagePref;
    RectTransform rectTransform;
    Image image;
    GameObject gunUI;
    Sprite DefSprite;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        imagePref = gameObject.transform.GetChild(1).gameObject;
        image = imagePref.GetComponent<Image>();
        rectTransform = imagePref.GetComponent<RectTransform>();
        try
        {
            if(player.transform.GetChild(1) != null)
        {
                gunUI = player.transform.GetChild(1).transform.GetChild(1).gameObject;
                float width = gunUI.transform.GetComponent<RectTransform>().rect.width;
                float height = gunUI.transform.GetComponent<RectTransform>().rect.height;
                rectTransform.sizeDelta = new Vector2(width, height);
            }
            else
            {
                image.sprite = DefSprite;
            }
        }
        catch{
        }
        
    }

    // Update is called once per frame
    public void ChangeGun(GameObject gun)
    {
        rectTransform.sizeDelta = new Vector2(gun.transform.GetComponent<RectTransform>().rect.width, gun.transform.GetComponent<RectTransform>().rect.height);

        image.sprite = gun.GetComponent<Image>().sprite;
    }
}
