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
using UnityEngine.EventSystems;

public class StoryScript : MonoBehaviour
{
    public Button continueButton;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueTextDisplay;
    [SerializeField] public GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [SerializeField] private TextAsset inkJSON;

    private Story currentStory;
    private int currentStoryIndex = 0;

    private bool dialogueIsPlaying { get; set; }

    private bool canContinueToNextLine = false;

    private Coroutine typeLineCoroutine;

    private static StoryScript instance;

    public Image background;

    public Sprite[] backgrounds;
    public int backgroundIndex = 0;


    private void Awake()
    {
        if (instance == null) instance = this;
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

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
        if (canContinueToNextLine && Input.GetKeyDown("space")) ContinueStory();
    }

    void TaskOnClick()
    {
        switch (currentStoryIndex)
        {
            case 0:
                ChangeBackground.changeBackground(background, backgrounds, ++backgroundIndex);
                break;
            case 3:
                ChangeBackground.changeBackground(background, backgrounds, ++backgroundIndex);
                break;
            case 5:
                ChangeBackground.changeBackground(background, backgrounds, ++backgroundIndex);
                break;
            case 9:
                ChangeBackground.changeBackground(background, backgrounds, 0);
                break;
            case 10:
                ChangeBackground.changeBackground(background, backgrounds, ++backgroundIndex);
                break;
        }
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
            if(typeLineCoroutine != null)
            {
                StopCoroutine(typeLineCoroutine);
            }
            typeLineCoroutine = StartCoroutine(TypeLine(currentStory.Continue()));
            currentStoryIndex++;
            // TypeSentences();
            DisplayChoices();

        }
        else
        {
            ExitDialogueMode();
        }
    }

    private IEnumerator TypeLine(string line)
    {
        dialogueTextDisplay.text = "";

        foreach (char letter in line.ToCharArray())
        {
            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                dialogueTextDisplay.text = line;
                break;
            }

            dialogueTextDisplay.text += letter;
            yield return new WaitForSeconds(0.04f);
        }

        canContinueToNextLine = true;
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length) return;

        int index = 0;

        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }

}