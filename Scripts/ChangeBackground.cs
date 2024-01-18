using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    public static void changeBackground(Image background, Sprite[] backgrounds, int index)
    {
        background.sprite = backgrounds[index];
    }
}
