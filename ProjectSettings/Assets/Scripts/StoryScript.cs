/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEditor.IMGUI.Controls;
using UnityEditor.VersionControl;

public class StoryScript : MonoBehaviour
{
    public TextAsset inkFile;
    static Story story;
    static Choice choiceSelected;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkFile.text);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(story.canContinue) 
            {
                nametag.text = "Phoenix";
                AdvanceDialogue();
            }
        }

        else
        {
            FinishDialogue();
        }
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

        CharacterScript tempSpeaker;
    }
}
*/