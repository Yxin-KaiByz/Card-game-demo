using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagCardScript : MonoBehaviour
{
    public Cards thisCard;
    public InventoryManager inventoryManager;


    public void addCard()
    {
       
        inventoryManager.cardList.Add(thisCard);
        Destroy(gameObject);    
    } 
}
