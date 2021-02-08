using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMP_Text dialogue = null;
    [SerializeField] private float displayDelay = 0f;

    [Header("Sprites")]
    [SerializeField] private GameObject imageLeft = null;
    [SerializeField] private GameObject imageRight = null;

    private Coroutine LetterByLetterRepeating;

    private bool canAutoComplete = false;

    private int dialogueIndex = 0;

    private Sprite[] sprites;
    private string[] dialogues;
    private string[] spriteIndex;

    private string currentAnimation = null;

    public void SetDialogues(NPCDialogue npcDialogue)
    {
        dialogueIndex = 0;      
        dialogue.text = null;   

        spriteIndex = new string[npcDialogue.Dialogues.Count];     
        dialogues = new string[npcDialogue.Dialogues.Count];
        sprites = new Sprite[npcDialogue.Dialogues.Count];

        for (int i = 0; i < npcDialogue.Dialogues.Count; i++)
        {
            spriteIndex[i] = npcDialogue.Dialogues[i].dialogueImagePosition;       
            dialogues[i] = npcDialogue.Dialogues[i].dialogueText;
            sprites[i] = npcDialogue.Dialogues[i].dialogueImage;
        }
        ChangeSprite();
        ChangeDialogueBoxAnimation("Show");
    }

    public void DisplayDialogue()
    {
        if (canAutoComplete)
        {
            AutoCompleteDialogue(dialogues);
        }
        else
        {
            LetterByLetterRepeating = StartCoroutine(AddDialogueLetterBytLetter(dialogues));
        }
    }

    private IEnumerator AddDialogueLetterBytLetter(string[] testText)
    {
        if (dialogueIndex < testText.Length)
        {
            canAutoComplete = true;
            dialogue.text = null;

            ChangeSprite();

            for (int i = 0; i < testText[dialogueIndex].Length; i++)
            {
                yield return new WaitForSeconds(displayDelay);
                dialogue.text += testText[dialogueIndex][i];
            }
            dialogueIndex++;
            canAutoComplete = false;
        }
        else
        {
            ChangeSpriteAnimation(false, false);
            ChangeDialogueBoxAnimation("Hide");
        }
    }

    private void AutoCompleteDialogue(string[] testText)
    {
        dialogue.text = testText[dialogueIndex];
        dialogueIndex++;
        canAutoComplete = false;
        StopCoroutine(LetterByLetterRepeating);
    }

    private void ChangeSprite()
    {
        switch (spriteIndex[dialogueIndex])
        {
            case "0":
                imageLeft.GetComponent<Image>().sprite = sprites[dialogueIndex];
                ChangeSpriteAnimation(true, false);
                break;
            case "1":
                imageRight.GetComponent<Image>().sprite = sprites[dialogueIndex];
                ChangeSpriteAnimation(false, true);
                break;
            default:
                break;
        }
    }

    private void ChangeSpriteAnimation(bool leftBool, bool rightBool)
    {
        imageLeft.GetComponent<Animator>().SetBool("Show", leftBool);
        imageRight.GetComponent<Animator>().SetBool("Show", rightBool);
    }

    private void ChangeDialogueBoxAnimation(string nextAnimation)
    {
        if (nextAnimation != currentAnimation)
        {
            GetComponent<Animator>().Play(nextAnimation);
            currentAnimation = nextAnimation;
        }
    }
}