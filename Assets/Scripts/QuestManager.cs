using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest[] quest;
    public bool[] isQuestCompleted;

    private DialogManager dialogManager;
    // Start is called before the first frame update
    void Start()
    {
        isQuestCompleted = new bool[quest.Length];
        dialogManager = FindObjectOfType<DialogManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowQuestText(string questText)
    {
        string[] dialogLines = new string[]
        {
            questText
        };

        dialogManager.ShowDialog(dialogLines);
    }
}
