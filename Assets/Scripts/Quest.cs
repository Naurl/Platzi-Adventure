using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int questID;
    public string startText, completeText;
    private QuestManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartQuest()
    {
        manager.ShowQuestText(startText);
    }

    public void CompleteQuest()
    {
        manager.isQuestCompleted[questID] = true;
        manager.ShowQuestText(completeText);
        gameObject.SetActive(false);
    }
}
