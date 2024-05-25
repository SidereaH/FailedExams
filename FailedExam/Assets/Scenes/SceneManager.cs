using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesChange : MonoBehaviour
{
    public void SceneLoad(string nameOfScene)
    {
        SceneManager.LoadScene(nameOfScene);
    }
    public void ContinueGame()
    {
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
}
