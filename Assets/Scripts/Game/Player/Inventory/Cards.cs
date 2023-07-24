using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_CardType
{
    Attack,
    Defend,
    Other
}

//可以新建一个Item通过右键
[CreateAssetMenu(fileName = "New Card", menuName = "Inventory/New Card")]
public class Cards : ScriptableObject
{
    public string cardName;
    public string cardId;
    [TextArea]
    public string cardInformation;
    public Sprite cardImage;
    public E_CardType cardType;
    
}


