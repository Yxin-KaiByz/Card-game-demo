using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Inventory/new Inventory")]
public class InventoryManager : ScriptableObject
{
    public List<Cards> cardList = new List<Cards>();

}
