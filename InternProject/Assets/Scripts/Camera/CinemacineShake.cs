using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemacineShake : MonoBehaviour
{
    public static CinemacineShake Instance {get; private set;}

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    float shakeTimer;
    float shakeTotal;
    float startIntensity;

    private void Awake() 
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCam(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin mulperin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        mulperin.m_AmplitudeGain = intensity;
        shakeTimer = time;
        shakeTotal = time;
        startIntensity = intensity;
    }

    private void Update() 
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin mulperin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            mulperin.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0f, 1-(shakeTimer/shakeTotal));

        }
    }
}
