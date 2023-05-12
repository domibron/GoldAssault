using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public partial class PlayerInteractionText : MonoBehaviour
{
    public TMP_Text displayUIText;

    List<DisplayText> displayTexts = new List<DisplayText>();

    // Start is called before the first frame update
    void Start()
    {
        // displayTexts.Add(new DisplayText("text", 2));
        // displayTexts.Add(new DisplayText("tesadasdasdxt", 1));
        // displayTexts.Add(new DisplayText("115252125125", 26));

        displayTexts.Sort((a, b) => a.priority.CompareTo(b.priority));

        // string temp = "";
        // foreach (DisplayText dt in displayTexts)
        // {
        //     temp += dt.text + " ";
        // }
        // print(temp);
    }

    // Update is called once per frame
    void Update()
    {
        if (displayTexts.Count > 0)
        {
            displayTexts.Sort((a, b) => a.priority.CompareTo(b.priority));
            displayUIText.text = displayTexts[0].text;
        }
        else
        {
            displayUIText.text = "";
        }

    }

    public void AddInteractionText(DisplayText display)
    {
        displayTexts.Add(display);
    }

    public void RemoveInteractionText(DisplayText display)
    {
        displayTexts.Remove(display);
    }

    public bool IsInTheList(DisplayText display)
    {
        if (displayTexts.Contains(display))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
