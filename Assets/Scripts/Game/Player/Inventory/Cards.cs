using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_CardType
{
    Attack,
    Defend,
    Other
}

//�����½�һ��Itemͨ���Ҽ�
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


