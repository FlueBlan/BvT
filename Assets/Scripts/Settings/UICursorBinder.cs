using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICursorBinder : MonoBehaviour
{
    private CursorManager cursorManager;

    void Start()
    {
        cursorManager = FindFirstObjectByType<CursorManager>();

        if (cursorManager == null)
        {
            Debug.LogError("CursorManager not found in scene. Please ensure it's in the active scene.");
            return;
        }

        Button[] allButtons = Resources.FindObjectsOfTypeAll<Button>();

        foreach (Button button in allButtons)
        {
            AddCursorEvents(button);
        }
    }



    private void AddCursorEvents(Button button)
    {
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = button.gameObject.AddComponent<EventTrigger>();
        }

        trigger.triggers.Clear();

        AddTrigger(trigger, EventTriggerType.PointerEnter, (e) => cursorManager.SetCursor(cursorManager.cursorHover));
        AddTrigger(trigger, EventTriggerType.PointerExit, (e) => cursorManager.SetCursor(cursorManager.cursorDefault));
        AddTrigger(trigger, EventTriggerType.PointerDown, (e) => cursorManager.SetCursor(cursorManager.cursorClick));
        AddTrigger(trigger, EventTriggerType.PointerUp, (e) => cursorManager.SetCursor(cursorManager.cursorHover));
    }

    private void AddTrigger(EventTrigger trigger, EventTriggerType type, UnityEngine.Events.UnityAction<BaseEventData> action)
    {
        var entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }
}
