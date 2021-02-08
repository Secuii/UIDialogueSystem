using System.Collections.Generic;
using UnityEngine;

public class DialogueContainer : MonoBehaviour
{
    public List<Dialogue> GameDialogues = new List<Dialogue>();
    public List<Sprite> GameSprites = new List<Sprite>();

    const char NEWLINE = '\n' ;
    const char SEMICOLON = ';' ;
    //TODO COMENTARIO SOBRE COMO ENVIAR LA ESTRUCTURA DEL ARCHIVO CSV

    void Awake()
    {
        TextAsset dialogues = Resources.Load<TextAsset>("DialogueFile");
        string[] dialogueLines = dialogues.text.Split(NEWLINE, (char)System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < dialogueLines.Length; i++)
        {
            Dialogue newDialogue = ScriptableObject.CreateInstance<Dialogue>();

            string[] dialogueParameters = dialogueLines[i].Split(SEMICOLON, (char)System.StringSplitOptions.RemoveEmptyEntries);

            newDialogue.dialogueReference = dialogueParameters[0];
            newDialogue.dialogueImagePosition = dialogueParameters[1];

            for (int j = 0; j < GameSprites.Count; j++)
            {
                if(dialogueParameters[2] == GameSprites[j].name)
                {
                    newDialogue.dialogueImage = GameSprites[j];
                }
            }

            newDialogue.dialogueText= dialogueParameters[3];
            newDialogue.dialogueLength = dialogueParameters[3].Length;

            GameDialogues.Add(newDialogue);
        }
    }
}
