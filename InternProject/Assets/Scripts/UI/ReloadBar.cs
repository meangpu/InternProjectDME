using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{
    [SerializeField] private Image reloadBar = null;

    private float currentProgress;
    private float maxProgress;
    private float progressImageVelocity;

    [SerializeField] private bool reloading = false;

    public void SetReloadTimer(float maxProgress)
    {
        currentProgress = 0;
        reloadBar.fillAmount = currentProgress;
        this.maxProgress = maxProgress;

        reloading = true;
    }

    private void Update()
    {
        if (!reloading) { return; }

        currentProgress += Time.deltaTime;
        ReloadBarUpdate();
    }

    private void ReloadBarUpdate()
    {
        float reloadProgress = currentProgress / maxProgress;

        if (reloadProgress >= 1)
        {
            reloadBar.fillAmount = 0;
            reloading = false;
        } else
        {
            reloadBar.fillAmount = Mathf.SmoothDamp(reloadBar.fillAmount, reloadProgress, ref progressImageVelocity, 0.01f);
        }
    }
}
