using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Reload : MonoBehaviour

{
    public ScoreManager scoreManager;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI goldText;
    // Start is called before the first frame update
    void Start()
    {
        //killsText.text = scoreManager.kills.ToString();
        killsText.text = PlayerPrefs.GetInt("kills").ToString();
        //goldText.text = scoreManager.gold.ToString();
        goldText.text = PlayerPrefs.GetInt("gold").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.enterKey.wasPressedThisFrame)
        {
            PlayerPrefs.SetInt("kills", 0);
            SceneManager.LoadScene(PlayerPrefs.GetString("lastScene"));
        }
    }
}
