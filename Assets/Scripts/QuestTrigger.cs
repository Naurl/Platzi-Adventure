using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class QuestTrigger : MonoBehaviour
{
    private QuestManager manager;
    public int QuestID;
    public bool startPoint, endPoint;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(!manager.isQuestCompleted[QuestID])
            {
                if(startPoint && manager.quest[QuestID].gameObject.activeInHierarchy)
                {
                    manager.quest[QuestID].gameObject.SetActive(true);
                    manager.quest[QuestID].StartQuest();
                }

                if(endPoint && manager.quest[QuestID].gameObject.activeInHierarchy)
                {
                    manager.quest[QuestID].CompleteQuest();
                }
            }
        }
    }
}
