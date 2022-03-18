using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogSystem : MonoBehaviour
{
   public int line = 0;
    float timeTotype = 3.0f;
    public float textPercent = 0f;
    public bool endWord = false;
    public TextAsset textFile;
    public Text dialog, textInButton;
    void Start()
    {
        Getdialog(0);
    }

    void Update()
    {
        textInButton.text = "Skip";
        Getdialog(line);
        if (Input.GetMouseButtonDown(0))
        {
            line += 1;
            endWord = false;
            textPercent = 0;
        }
    }
    void Getdialog(int line)
    {
        string[] lineInFile;
        lineInFile = textFile.text.Split('\n');
        if (line < lineInFile.Length)
        {
            callTyping(lineInFile[line]);
        }
        else
        {
            dialog.text = "กด Play เพื่อเล่นเกม";
            textInButton.text = "Play";
        }
    }
    void callTyping(string textToType)
    {
        int letterToShow = (int) (textToType.Length * textPercent);
        dialog.text = textToType.Substring(0, letterToShow);
        textPercent += Time.deltaTime / timeTotype;
        textPercent = Mathf.Min(1.0f, textPercent);
        if (textPercent >= 1)
        {
            endWord = true;
        }
    }
}
