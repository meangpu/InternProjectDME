using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camTarget : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform player;
    [SerializeField] float threshold;
    [SerializeField] PlayerInputManager input;
    Vector2 mousePos;

    void Update()
    {
        mousePos = input.GetMousePosition();
        Vector2 cursorPosOnScreen = cam.ScreenToWorldPoint(mousePos);
        Vector2 targetpos = ((Vector2)player.position + mousePos) /2f;
        Debug.Log((Vector2)player.position);

        targetpos.x = Mathf.Clamp(targetpos.x, -threshold + player.position.x, threshold + player.position.x);
        targetpos.y = Mathf.Clamp(targetpos.y, -threshold + player.position.y, threshold + player.position.y);

        this.transform.position = targetpos;
        // Debug.Log(mousePos);
    }
}
