using System.Collections;
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

    public IEnumerator PlaySoundAndDisable()
    {
        AudioClip clip = objSound.GetRandomClip();
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(clip.length);
        PoolingSingleton.Instance.AudioSourcePool.ReturnObject(gameObject);
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
