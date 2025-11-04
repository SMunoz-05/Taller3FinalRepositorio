using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject[]hotbarSlots = new GameObject[3];
    [SerializeField] GameObject[] slots = new GameObject[12];
    [SerializeField] GameObject inventoryParent;
    [SerializeField] Transform handParent;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Camera cam;
    [SerializeField] private Transform itemsParent;


    GameObject draggedObject;
    GameObject lastItemSlot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    bool isInventoryOpened;

    int selectedHotbarSlot = 0;
    void Start()
    {
            HotbarItemChanged();

    }

    // Update is called once per frame
    void Update()
    {
        CheckForHotbarInput();  
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isInventoryOpened = !isInventoryOpened;

            if (isInventoryOpened)
            {
                Time.timeScale = 0f; // Pausar el juego
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None; // Cursor desbloqueado para UI
            }
            else
            {
                Time.timeScale = 1f; // Reanudar juego
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked; // Cursor bloqueado para juego shooter
            }
        }

        inventoryParent.SetActive(isInventoryOpened);

        if (draggedObject != null)
        {
            draggedObject.transform.position = Input.mousePosition;
        }
    }

    private void CheckForHotbarInput()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            selectedHotbarSlot = 0;
            HotbarItemChanged();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            selectedHotbarSlot = 1;
            HotbarItemChanged();

        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            selectedHotbarSlot = 2;
            HotbarItemChanged();

        }
    }
    private void HotbarItemChanged()
    {
        for (int i =0; i < handParent.childCount; i++)
        {
            handParent.GetChild(i).gameObject.SetActive(false);
        }
        foreach(GameObject slot in hotbarSlots)
        {
            Vector3 scale;
            if (slot == hotbarSlots[selectedHotbarSlot])
            {
                scale = new Vector3(1.1f, 1.1f, 1.1f);

                if (slot.GetComponent<InventorySlot>().heldItem != null)
                {
                    for(int i =0; i < handParent.childCount; i++)
                    {
                        if (handParent.GetChild(i).GetComponent<ItemHand>().itemScriptableObject 
                            == hotbarSlots[selectedHotbarSlot].GetComponent<InventorySlot>().heldItem.GetComponent<InventoryItem>().itemScriptableObject)
                        {
                            handParent.GetChild(i).gameObject.SetActive(true);
                        }
                    }
                }
            }
            else
            {
                scale = new Vector3(0.9f, 0.9f, 0.9f);
            }
            slot.transform.localScale = scale; 
        }
    }


    public void OnPointerDown (PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
            InventorySlot slot = clickedObject.GetComponent<InventorySlot>();

            if (slot != null && slot.heldItem != null)
            {
                draggedObject = slot.heldItem;
                slot.heldItem = null;
                lastItemSlot = clickedObject;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       if (draggedObject != null && eventData.pointerCurrentRaycast.gameObject != null && eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
            InventorySlot slot = clickedObject.GetComponent<InventorySlot> ();

            if (slot != null && slot.heldItem == null)
            {
                slot.SetHeldItem(draggedObject);
                draggedObject.transform.SetParent(slot.transform.parent.parent.GetChild(1));


            }
            else if (slot != null && slot.heldItem != null)
            {
                lastItemSlot.GetComponent<InventorySlot>().SetHeldItem(slot.heldItem);
                slot.heldItem.transform.SetParent(slot.transform.parent.parent.GetChild(1));
                slot.SetHeldItem(draggedObject);
                draggedObject.transform.SetParent(slot.transform.parent.parent.GetChild(1));




            }
            else if (clickedObject.name != "DropItem")
            {
                lastItemSlot.GetComponent<InventorySlot>().SetHeldItem(draggedObject);
                draggedObject.transform.SetParent(slot.transform.parent.parent.GetChild(1));

            }
            else
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                Vector3 position = ray.GetPoint(3);

                GameObject newItem = Instantiate(draggedObject.GetComponent<InventoryItem>().itemScriptableObject.prefab, position, Quaternion.identity);
                newItem.GetComponent<ItemPickable>().itemScriptableObject = draggedObject.GetComponent<InventoryItem>().itemScriptableObject;
                newItem.transform.SetParent(itemsParent);


                lastItemSlot.GetComponent<InventorySlot>().heldItem = null;
                Destroy(draggedObject);
            }
            HotbarItemChanged();
            draggedObject = null;
        }
    }
    public void ItemPicked(GameObject pickedItem)
    {
        GameObject emptySlot = null; 
        for(int i=0; i <slots.Length; i++)
        {
            InventorySlot slot = slots[i].GetComponent<InventorySlot>();

            if (slot.heldItem== null)
            {
                emptySlot = slots[i];
                break;
            }
        }

        if (emptySlot != null)
        {
            GameObject newItem = Instantiate(itemPrefab);
            newItem.GetComponent<InventoryItem>().itemScriptableObject = pickedItem.GetComponent<ItemPickable>().itemScriptableObject;
            newItem.transform.SetParent(itemsParent, false);


            emptySlot.GetComponent<InventorySlot>().SetHeldItem(newItem);
            newItem.transform.localScale = new Vector3(1, 1, 1);
            Destroy(pickedItem);
        }
    }
}
