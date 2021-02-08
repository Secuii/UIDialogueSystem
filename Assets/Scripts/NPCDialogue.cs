using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private string dialogueReference = null;
    [SerializeField] private DialogueContainer dialogueContainer = null;
    private int referenceCount = 1;

    public List<Dialogue> Dialogues = new List<Dialogue>();


    private void Start()
    {
        for (int i = 0; i < dialogueContainer.GameDialogues.Count; i++)
        {
            if (dialogueContainer.GameDialogues[i].dialogueReference == dialogueReference + "_" + referenceCount)
            {
                Dialogues.Add(dialogueContainer.GameDialogues[i]);
                referenceCount++;
                i = 0;
            }
        }
    }
}