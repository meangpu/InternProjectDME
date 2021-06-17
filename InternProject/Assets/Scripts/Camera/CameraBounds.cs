using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    [SerializeField] private float boundMinX = -150f;
    [SerializeField] private float boundMaxX = 150f;
    [SerializeField] private float boundMinY = -150f;
    [SerializeField] private float boundMaxY = 150f;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        CameraInBounds();
    }

    private void CameraInBounds()
    {
        cam.orthographicSize = Mathf.Min(cam.orthographicSize, ((boundMaxY - boundMinY) / 2) - 0.001f);
        cam.orthographicSize = Mathf.Min(cam.orthographicSize, (Screen.height * (boundMaxX - boundMinX) / (2 * Screen.width)) - 0.001f);

        Vector2 margin = cam.ScreenToWorldPoint((Vector2.up * Screen.height / 2) + (Vector2.right * Screen.width / 2)) - cam.ScreenToWorldPoint(Vector2.zero);

        float marginX = margin.x;
        float marginY = margin.y;

        float camMaxX = boundMaxX - marginX;
        float camMaxY = boundMaxY - marginY;
        float camMinX = boundMinX + marginX;
        float camMinY = boundMinY + marginY;

        float camX = Mathf.Clamp(cam.transform.position.x, camMinX, camMaxX);
        float camY = Mathf.Clamp(cam.transform.position.y, camMinY, camMaxY);

        cam.transform.position = new Vector3(camX, camY, cam.transform.position.z);
    }
}
