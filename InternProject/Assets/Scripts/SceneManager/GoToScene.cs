using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public string destination;

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
}
