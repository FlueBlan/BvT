using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorDefault;
    public Texture2D cursorHover;
    public Texture2D cursorClick;
    public Vector2 hotSpot = Vector2.zero;
    private Texture2D currentCursor;

    void Start()
    {
        SetCursor(cursorDefault);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetCursor(cursorClick);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SetCursor(cursorDefault);
        }
    }

    public void SetCursor(Texture2D cursor)
    {
        if (cursor == currentCursor) return;
        Cursor.SetCursor(cursor, hotSpot, CursorMode.Auto);
        currentCursor = cursor;
    }

    public void OnHoverEnter()
    {
        SetCursor(cursorHover);
    }

    public void OnHoverExit()
    {
        SetCursor(cursorDefault);
    }
}
