using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamagePopup : MonoBehaviour
{

    public static DamagePopup Create(Vector3 position, int damageAmount, string type) {
        Transform damagePopupTransform = Instantiate(GameAssets.i.prefabDmgPopUp, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, type);

        return damagePopup;
    }

    private static int sortingOrder;

    private const float DISAPPEAR_TIMER_MAX = 1f;

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;

    private void Awake() {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, string type) {
        textMesh.SetText(damageAmount.ToString());
        if (type == "Player") 
        {
            Color red = new Color(1, 0.28f, 0.28f, 1);
            textMesh.fontSize = 3;
            textColor = red;
        }
        else if (type == "Enemy")
        {
            textMesh.fontSize = 2;
            textColor = Color.white;
        }



        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        moveVector = new Vector3(.3f, 1) * 5f;
    }

    private void Update() {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f) {
            // First half of the popup lifetime
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        } else {
            // Second half of the popup lifetime
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0) {
            // Start disappearing
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0) {
                Destroy(gameObject);
            }
        }
    }
}
