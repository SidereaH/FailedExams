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
        killsText.text = scoreManager.kills.ToString();
        goldText.text = scoreManager.gold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
