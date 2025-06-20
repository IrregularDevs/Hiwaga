using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "NPCCharacter", menuName = "Scriptable Objects/NPCCharacter")]
public class NPCCharacter : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
}
