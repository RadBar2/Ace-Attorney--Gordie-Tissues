using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class Textbox : MonoBehaviour
{
    string path = "D:\\Unity\\Ace Attorney Gordie Tissues\\Assets\\Script.txt";
    string[] txtArray;
    public TMP_Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            string txt = File.ReadAllText(path);
            txtArray = txt.Split('\n');
        }
        catch (Exception ex)
        {
            print(ex.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in txtArray)
        {
            if(Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Return))
            {
                textBox.text = "";
                textBox.text = item;
                
            }
            
        }
    }
}
