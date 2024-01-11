/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEditor.IMGUI.Controls;
using UnityEditor.VersionControl;
using UnityEngine.UI;

public class StoryScript : MonoBehaviour
{
    public TextAsset inkFile;
    public GameObject textBox;
    public GameObject customButton;
    public GameObject ChoicePanel;
    public bool isTalking = false;

    static Story story;
    Text nametag;
    Text message;
    List<string> tags;
    static Choice choiceSelected;

    // Start is called before the first frame update
    void Start()
    {
        story = Story(inkFile.text);
        nametag = textBox.transform.GetChild(0).GetComponent<Text>();
        message = textBox.transform.GetChild(1).GetComponent<Text>();
        tags = new List<string>();
        choiceSelected = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(story.canContinue) 
            {
                nametag.text = "Edgeworth";
                AdvanceDialogue();

                if(story.currentChoices.Count != 0) 
                {
                    StartCoroutine(ShowChoices());
                }
            }
        }

        else
        {
            FinishDialogue();
        }
    }

    private void FinishDialogue()
    {
        Debug.Log("End of dialogue");
    }

    void AdvanceDialogue()
    {
        string currentSentence = story.Continue();
        ParseTags();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    void ParseTags()
    {
        tags = story.currentTags;
        foreach (string t in tags)
        {
            string prefix = t.Split(' ')[0];
            string param = t.Split(" ")[1];

            switch (prefix.ToLower())
            {
                case "anim":
                    SetAnimation(param);
                    break;
                case "color":
                    SetTextColor(param);
                    break;
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        message.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            message.text += letter;
            yield return null;
        }

        CharacterScript tempSpeaker = GameObject.FindObjectOfType<CharacterScript>();
        if (tempSpeaker.isTalking) 
        {
            SetAnimation("idle");
        }
        yield return null;
    }

    IEnumerator ShowChoices() 
    {
        List<Ink.Runtime.Choice> choices = story.currentChoices;

        for(int i = 0; i < choices.Count; i++) 
        {
            GameObject temp = Instantiate(customButton, ChoicePanel.transform);
            temp.transform.GetChild(0).GetComponent<Text>().text = choices[i].text;
            temp.AddComponent<Selectable>();
            temp.GetComponent<Selectable>().element = choices[i];
            temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<Selectable>().Decide(); });

        }

        ChoicePanel.SetActive(true);

        yield return new WaitUntil(() => { return choiceSelected != null; });

        AdvanceFromDecision();
    }

    public static void SetDecision(object element)
    {
        choiceSelected = (Choice)element;
        story.ChooseChoiceIndex(choiceSelected.index);
    }

    void AdvanceFromDecision()
    {
        ChoicePanel.SetActive(false);
        for (int i = 0; i < ChoicePanel.transform.childCount; i++) 
        {
            Destroy(ChoicePanel.transform.GetChild(i).gameObject);
        }
        choiceSelected = null;
        AdvanceDialogue();
    }
}
*/