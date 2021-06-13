using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    private enum SceneName
    {
        mainmenu,
        testScene
    }

    public void ToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
