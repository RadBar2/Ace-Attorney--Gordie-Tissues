using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEditor.IMGUI.Controls;
using UnityEditor.VersionControl;
using UnityEngine.UI;
using Unity.PlasticSCM.Editor.WebApi;
using TMPro;
using System;
using System.Reflection;

public class StoryScript : MonoBehaviour
{
    public Button continueButton;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueTextDisplay;

    [SerializeField] private TextAsset inkJSON;

    private Story currentStory;

    private bool dialogueIsPlaying { get; private set; };

    private static StoryScript instance;

    public Image background;

    public Sprite[] backgrounds;
    public int backgroundIndex = 0;


    private void Awake()
    {
        if(instance == null) instance = this;
    }

    public static StoryScript GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        if (continueButton == null) continueButton = GetComponent<Button>();
        continueButton.onClick.AddListener(TaskOnClick);

        EnterDialogueMode(inkJSON);
    }

    private void Update()
    {
        if(!dialogueIsPlaying) 
        {
            return;
        }
        if(Input.GetKeyDown("space")) ContinueStory();
    }

    void TaskOnClick()
    {
        ContinueStory();
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueTextDisplay.text = "";
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueTextDisplay.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueTextDisplay.text = currentStory.Continue();
            // TypeSentences();

        }
        else
        {
            ExitDialogueMode();
        }
    }

    IEnumerator TypeSentences()
    {
        dialogueTextDisplay.text = "";

        foreach (char letter in currentStory.Continue().ToCharArray())
        {
            dialogueTextDisplay.text += letter;
            yield return new WaitForSeconds(0.2f);
        }
    }
}