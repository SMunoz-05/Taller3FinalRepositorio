using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class InventoryItem : MonoBehaviour
{
    public ItemSO itemScriptableObject;

    [SerializeField] Image iconImage;


    void Update()
    {
        iconImage.sprite = itemScriptableObject.icon;
    }
}
