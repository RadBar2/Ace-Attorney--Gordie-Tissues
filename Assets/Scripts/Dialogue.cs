using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textdisplay;
    public string[] sentences;
    private int index = 0;
    public float typingSpeed;

    private void Start()
    {
        StartCoroutine(TypeSentences());
    }

    IEnumerator TypeSentences()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textdisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if(index < sentences.Length - 1 && ) 
        {
            index++;
            textdisplay.text = "";
            StartCoroutine(TypeSentences());
        }
        else textdisplay.text = "";
    }
}
