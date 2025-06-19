using TMPro;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public Sprite image;
    public string Name;
    public bool stackable;
    public bool isInvisible;
}
