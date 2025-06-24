using System.Collections;
using UnityEngine;

public static class DialogueManagerExtensions
{
    public static IEnumerator StartDialogueRoutine(this DialogueManager manager, Dialogue dialogue)
    {
        bool isDone = false;
        manager.StartDialogue(dialogue, () => isDone = true); // Uses callback-enabled StartDialogue
        while (!isDone)
        {
            yield return null;
        }
    }
}
