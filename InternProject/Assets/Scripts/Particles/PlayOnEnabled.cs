using UnityEngine;

public class PlayOnEnabled : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource = null;

    private void OnEnable()
    {
        audioSource.Play();
    }
} 
