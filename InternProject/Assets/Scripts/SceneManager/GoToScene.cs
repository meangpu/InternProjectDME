using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [HideInInspector]
    public string destination;
    [HideInInspector]
    public int sceneIndex;

    public void ToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void selfGoToLevel()
    {
        if (destination != null)
        {
            SceneManager.LoadScene($"Level/{destination}");
            Debug.Log($"Level/{destination}");
        }
    }

    public void goLevelByIndex()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
