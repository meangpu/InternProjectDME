using UnityEngine;

public class LoadOnEnabled : MonoBehaviour
{
    [SerializeField] private GameSaveManager saveManager = null;

    private void OnEnable()
    {
        saveManager.LoadGame();
    }
}
