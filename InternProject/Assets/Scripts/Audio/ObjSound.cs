using UnityEngine;

[CreateAssetMenu(fileName = "New Sound FX", menuName = "Sound/Create New Sound FX")]
public class ObjSound : ScriptableObject
{
    [SerializeField] private AudioClip[] audioClips;

    public AudioClip GetRandomClip()
    {
        return audioClips[Random.Range(0, audioClips.Length)];
    }
}
