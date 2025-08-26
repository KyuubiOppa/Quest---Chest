using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [Header("Current Inventory Items")]
    [SerializeField] private List<SO_Item> currentItemsInInventory = new List<SO_Item>();
    [SerializeField] private Inventory_InvSlot[] inventory_InvSlots;
    [Header("Inventory Save File")]
    [SerializeField] private string inventorySaveFile = "inventory.json";

    [Header("All Items")]
    [SerializeField] private SO_Item[] allAvailableItems; // ใช้เก็บ Item ทั้งหมดในเก
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(InitializeInventory());
    }

/// <summary>
/// หน่วง 1 เฟรม
/// </summary>
/// <returns></returns>
    private IEnumerator InitializeInventory()
    {
        yield return null;
        LoadInventory();
        UpdateInventoryUI();
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveInventory();
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveInventory();
        }
    }

    void OnApplicationQuit()
    {
        SaveInventory();
        Instance = null;
    }

    #region Item Get & Clear
    public void AddItemToInventory(SO_Item item)
    {
        if (!currentItemsInInventory.Contains(item))
        {
            currentItemsInInventory.Add(item);
        }
        UpdateInventoryUI();
        SaveInventory();
    }

    public void ClearInventory()
    {
        currentItemsInInventory.Clear();
        UpdateInventoryUI();
        SaveInventory();
    }
    #endregion

    #region Check Item Inventory
    public bool HasItem(SO_Item item)
    {
        return currentItemsInInventory.Contains(item);
    }
    #endregion

    #region Update Inventory UI
    public void UpdateInventoryUI()
    {
        for (int i = 0; i < inventory_InvSlots.Length; i++)
        {
            if (i < currentItemsInInventory.Count)
            {
                inventory_InvSlots[i].SetItem(currentItemsInInventory[i]);
            }
            else
            {
                inventory_InvSlots[i].SetItem(null);
            }
        }
    }
    #endregion

    #region Save/Load Methods
/// <summary>
/// Save Inv
/// </summary>
    public void SaveInventory()
    {
        try
        {
            string data = SaveInventoryData();
            string path = GetSavePath();
            File.WriteAllText(path, data);
            Debug.Log("บันทึก Inventory สำเร็จที่: " + path);
        }
        catch (System.Exception e)
        {
            Debug.LogError("ผิดพลาดในการบันทึก Inventory: " + e.Message);
        }
    }
/// <summary>
/// Load Lnv
/// </summary>
    public void LoadInventory()
    {
        try
        {
            string path = GetSavePath();
            Debug.Log("พยายาม Load Inventory จาก: " + path);
            if (File.Exists(path))
            {
                string data = File.ReadAllText(path);
                LoadInventoryData(data);
                Debug.Log("Load Inventory สำเร็จ");
            }
            else
            {
                Debug.Log("ไม่มีไฟล์ Save - เริ่มต้นใหม่");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("ผิดพลาดในการ Load Inventory: " + e.Message);
        }
    }

    private string GetSavePath()
    {
        string folderPath = Application.persistentDataPath;
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        return Path.Combine(folderPath, inventorySaveFile);
    }

    private string SaveInventoryData()
    {
        InventoryData invData = new InventoryData(this);
        return JsonUtility.ToJson(invData, true);
    }

    private void LoadInventoryData(string data)
    {
        if (!string.IsNullOrEmpty(data))
        {
            try
            {
                InventoryData invData = JsonUtility.FromJson<InventoryData>(data);
                SetInventoryData(invData);
            }
            catch (System.Exception e)
            {
                Debug.LogError("ผิดพลาดในการแปลง JSON: " + e.Message);
            }
        }
    }

    private void SetInventoryData(InventoryData inventoryData)
    {
        currentItemsInInventory.Clear();
        
        for (int i = 0; i < inventoryData.slotDatas.Length && i < inventory_InvSlots.Length; i++)
        {
            if (!string.IsNullOrEmpty(inventoryData.slotDatas[i].itemFileName))
            {
                SO_Item loadedItem = FindItemByName(inventoryData.slotDatas[i].itemFileName);
                
                if (loadedItem != null)
                {
                    currentItemsInInventory.Add(loadedItem);
                    inventory_InvSlots[i].SetItem(loadedItem);
                }
                else
                {
                    Debug.LogWarning("ไม่เจอ Item: " + inventoryData.slotDatas[i].itemFileName);
                    inventory_InvSlots[i].SetItem(null);
                }
            }
            else
            {
                inventory_InvSlots[i].SetItem(null);
            }
        }
        UpdateInventoryUI();
    }

    private SO_Item FindItemByName(string itemName)
    {
        if (allAvailableItems != null)
        {
            foreach (SO_Item item in allAvailableItems)
            {
                if (item != null && item.name == itemName)
                {
                    return item;
                }
            }
        }
        return null;
    }
    #endregion

    [Serializable]
    public class InventoryData
    {
        public InventorySlotData[] slotDatas;
        
        public InventoryData(InventoryManager inv)
        {
            slotDatas = new InventorySlotData[inv.inventory_InvSlots.Length];

            for (int i = 0; i < inv.inventory_InvSlots.Length; i++)
            {
                slotDatas[i] = new InventorySlotData(inv.inventory_InvSlots[i]);
            }
        }
    }

    [Serializable]
    public class InventorySlotData
    {
        public string itemFileName;
        public InventorySlotData(Inventory_InvSlot slot)
        {
            if (slot.item != null)
            {
                itemFileName = slot.item.name;
            }
            else
            {
                itemFileName = "";
            }
        }
    }
}