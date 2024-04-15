using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBoxScript : MonoBehaviour
{
    [SerializeField] private SummonItem giftItem;
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Cat"))
        {
            InventoryManager.Instance.AddItem(giftItem, 1);
            Destroy(gameObject);
        }
    }
}
