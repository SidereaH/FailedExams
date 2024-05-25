using System;
using UnityEngine;
using UnityEngine.UI;


public class isActiveBut : MonoBehaviour
{
    // Start is called before the first frame update
    Button button;
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        
        if(PlayerPrefs.HasKey("lastScene") == true)
        {
            Debug.Log(PlayerPrefs.GetString("lastScene"));
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
