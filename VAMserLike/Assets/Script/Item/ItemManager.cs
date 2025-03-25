using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    public static ItemManager aInstance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new ItemManager();
            }
            return sInstance;
        }
    }

    public void SpawnItem(string InItemId, Vector3 InSpawnPos)
    {
        ItemBase ItemObject = GamePoolManager.aInstance.DequeueItemPool(InItemId);
        if (ItemObject == null)
        {
            ItemBase NewItemObject = GameDataManager.aInstance.GetItemObject(InItemId);
            ItemObject = GameObject.Instantiate<ItemBase>(NewItemObject,
                                                        GameDataManager.aInstance.GetItemRootTransform());
        }

        if (ItemObject == null)
        {
            Debug.LogError("Not Exist ItemObject : " + InItemId);
            return;
        }
        ItemObject.transform.position = InSpawnPos;
        ItemObject.mItemData = GameDataManager.aInstance.GetItemData(InItemId);
        ItemObject.gameObject.SetActive(true);
    }

    public void DespawnItem(ItemBase InItemBase)
    {
        InItemBase.gameObject.SetActive(false);
        GamePoolManager.aInstance.EnqueueItemPool(InItemBase);
    }

    private static ItemManager sInstance = null;
}
