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
    public void Exit()
    {
        Application.Quit();
    }
}
