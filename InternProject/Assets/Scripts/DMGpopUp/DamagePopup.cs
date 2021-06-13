using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamagePopup : MonoBehaviour
{
    private static int sortingOrder;

    private const float DISAPPEAR_TIMER_MAX = 1f;

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;

    public enum DamageType
    {
        Player,
        Enemy,
        Gold
    }

    private void Awake() 
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, DamageType type) {
        if (type == DamageType.Player) 
        {
            Color red = new Color(1, 0.28f, 0.28f, 1);
            textMesh.fontSize = 3;
            textColor = red;
            textMesh.SetText(damageAmount.ToString());
        }
        else if (type == DamageType.Enemy)
        {
            textMesh.fontSize = 2;
            textColor = Color.white;
            textMesh.SetText(damageAmount.ToString());
        }
        else if (type == DamageType.Gold)
        {
            textMesh.fontSize = 6;
            textColor = Color.yellow;
            textMesh.SetText($"+{damageAmount.ToString()} Golds");
        }

        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
        transform.localScale = Vector3.one;

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
                // Destroy(gameObject);
                PoolingSingleton.Instance.PopUpPool.ReturnObject(gameObject);
            }
        }
    }

    public static void Create(Vector3 position, int damageAmount, DamageType type)
    {
        PoolingSingleton.Instance.PopUpPool.SpawnPopup(position, Quaternion.identity, damageAmount, type);
    }
}
