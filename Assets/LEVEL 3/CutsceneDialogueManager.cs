using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutsceneDialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;
    public List<DialogueLine2> cutsceneLines;
    public List<Sprite> characterSprites;
    public AudioSource audioSource;

    private int currentLine = 0;

    void Start()
    {
        dialoguePanel.SetActive(true);
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (currentLine < cutsceneLines.Count)
        {
            DialogueLine2 line = cutsceneLines[currentLine];
            speakerNameText.text = line.speakerName;
            dialogueText.text = line.lineText;

            // Set sprite based on speaker name
            if (line.speakerName.Contains("Peaberry"))
                characterImage.sprite = characterSprites[0]; // Princess Peaberry
            else if (line.speakerName.Contains("CrunchWorth"))
                characterImage.sprite = characterSprites[1]; // CrunchWorth
            else
                characterImage.sprite = characterSprites[2]; // ??? or default

            // Play voice clip
            if (audioSource != null && line.voiceClip != null)
            {
                audioSource.clip = line.voiceClip;
                audioSource.Play();
            }

            currentLine++;
        }
        else
        {
            StartCoroutine(LoadLevel3AfterDelay());
        }
    }

    private IEnumerator LoadLevel3AfterDelay()
    {
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level3");
    }
}
