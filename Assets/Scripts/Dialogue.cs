using UnityEngine;

[CreateAssetMenu(fileName = "dialogue test",menuName = "Dialogue System/dialogue")]
public class Dialogue : ScriptableObject
{
    public string dialogueReference;
    public string dialogueImagePosition;
    public Sprite dialogueImage;
    public string dialogueText;
    public int dialogueLength;
}
