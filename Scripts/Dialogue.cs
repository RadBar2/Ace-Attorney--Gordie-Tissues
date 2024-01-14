using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textdisplay;
    public string[] sentences;
    private int index = 0;
    public float typingSpeed;

    public Button continueButton;

    public Image background;

    public Sprite[] backgrounds;
    public int backgroundIndex = 0;

    private void Start()
    {
        if (continueButton == null) continueButton = GetComponent<Button>();
        continueButton.onClick.AddListener(TaskOnClick);
        StartCoroutine(TypeSentences());
    }

    private void Update()
    {
        if (Input.GetKeyDown("space") && textdisplay.text == sentences[index]) NextSentence();
    }

    void TaskOnClick()
    {
        if(textdisplay.text == sentences[index]) NextSentence();
    }

    IEnumerator TypeSentences()
    {
        textdisplay.text = "";

        foreach (char letter in sentences[index].ToCharArray())
        {
            textdisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textdisplay.text = "";
            StartCoroutine(TypeSentences());
        }
        else
        {
            textdisplay.text = "";
            
            backgroundIndex++;
            ChangeBackground.changeBackground(background, backgrounds, backgroundIndex);
            
        }
    }
}
