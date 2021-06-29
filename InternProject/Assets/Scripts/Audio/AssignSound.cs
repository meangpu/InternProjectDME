using UnityEngine;

public class AssignSound : MonoBehaviour
{
    [SerializeField] private ObjSound objSound = null;
    [SerializeField] private AudioSource audioSource = null;

    public void PlaySound()
    {
        audioSource.clip = objSound.GetRandomClip();
        audioSource.Play();
    }

    public void SetSound(ObjSound sound)
    {
        objSound = sound;
    }
}
