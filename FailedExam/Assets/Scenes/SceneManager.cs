using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Numerics;

public class ScenesChange : MonoBehaviour
{
    [SerializeField] GameObject parentGameobj;
    GameObject menuUi;
    [SerializeField] Player player;
    public bool isActivemenu;
    private void Start()
    {
        Time.timeScale = 1;
        if (parentGameobj != null)
        {
            menuUi = parentGameobj.transform.GetChild(0).gameObject;
        }
        isActivemenu = false;

    }
    public void SceneLoad(string nameOfScene)
    {
        SceneManager.LoadScene(nameOfScene);
        
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().name != "Menu")
        {
            
            if (parentGameobj != null)
            {
                if(gameObject.name == "MenuManager")
                {
                    if (parentGameobj.tag == "Menu")
                    {

                        if (Keyboard.current.escapeKey.wasPressedThisFrame == true)
                        {
                            
                            
                            if (menuUi.activeInHierarchy == false)
                            {
                                menuUi.SetActive(true);
                                player.isPaused(true);
                                Time.timeScale = 0;
                            }
                            else
                            {
                                menuUi.SetActive(false);
                                isActivemenu = false;
                                Time.timeScale = 1;
                                player.isPaused(false);
                            }
                        }
                    }
                }
               
            }
        }
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetString("lastScene") != null)
        {
            string lastGame = PlayerPrefs.GetString("lastScene");
            SceneManager.LoadScene(lastGame);
        }
        else
        {

        }

    }
    public void Exit()
    {
        Application.Quit();
    }
    public void PrefsToNull()
    {
        PlayerPrefs.DeleteAll();
    }
    public void ClosePanel()
    {
        menuUi.SetActive(false);
        Time.timeScale = 1;
        player.isPaused(false);
    }
}
