using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public void ToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
