using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camTarget : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform player;
    [SerializeField] float threshold;
    private PlayerControls playerControls;
    Vector2 mousePos;
    [SerializeField] bool lockPos = false;

    private void Start() 
    {
        playerControls = GameManager.Instance.GetPlayerControls();
    }

    void Update()
    {
        if(lockPos)
        {
            return;
        }
        mousePos = playerControls.Tank.LookAt.ReadValue<Vector2>();
        Vector2 cursorPosOnScreen = cam.ScreenToWorldPoint(mousePos);
        Vector2 targetpos = ((Vector2)player.position + cursorPosOnScreen) /2f;

        targetpos.x = Mathf.Clamp(targetpos.x, -threshold + player.position.x, threshold + player.position.x);
        targetpos.y = Mathf.Clamp(targetpos.y, -threshold + player.position.y, threshold + player.position.y);

        transform.position = targetpos;
    }

    /*public void playMode()
    {
        threshold = 2;
    }

    public void buyMode()
    {
        threshold = 8;
    }*/
}
