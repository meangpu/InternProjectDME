using UnityEngine;

public class InGameCursorController : MonoBehaviour
{
    [SerializeField] private Texture2D cursor = null;
    [SerializeField] private Texture2D arrowCursor = null;
    [SerializeField] private bool isLockInGamePreview;

    private void Awake()
    {
        ChangeCursor(arrowCursor);

        if (isLockInGamePreview)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        
    }

    private void ChangeCursor(Texture2D cursorType)
    {
        Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 2);
        Cursor.SetCursor(cursorType, hotspot, CursorMode.Auto);
    }

    public void ChangeCursorToArrow()
    {
        ChangeCursor(arrowCursor);
    }

    public void ChangeCursorToCrosshair()
    {
        ChangeCursor(cursor);
    }
}
