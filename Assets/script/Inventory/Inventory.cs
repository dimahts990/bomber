using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<AssetGrenade> grenadesList = new List<AssetGrenade>();
    [SerializeField] private Transform viewInventoryTransform;
    [SerializeField] private InventoryCell _inventoryCell;
    [SerializeField] private PlayerFire _playerFire;

    private Transform _selectedInventoryCellTransform;

    private int numSelectedCallInInventory()
    {
        int x = 0;
        for (int i = 0; i < viewInventoryTransform.childCount; i++)
        {
            if (viewInventoryTransform.GetChild(i).GetComponent<InventoryCell>().SelectedThisBool)
            {
                x = i;
                break;
            }
        }
        return x;
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
            moveToSelectedGrenadeToInventory(true);
        if(Input.GetKeyDown(KeyCode.LeftArrow))
            moveToSelectedGrenadeToInventory(false);
    }

    public void AddInventory(AssetGrenade _assetGrenade)
    {
        var cell = Instantiate(_inventoryCell, viewInventoryTransform);
        cell.Render(_assetGrenade);
        selectGrenadeToInventory(cell.transform);
    }

    public void RemoveFromInventory()
    {
        int numSelect = numSelectedCallInInventory();
        Destroy(viewInventoryTransform.GetChild(numSelect).gameObject);
        moveToSelectedGrenadeToInventory(false);
    }

    private void moveToSelectedGrenadeToInventory(bool moveRightBool)
    {
        int numSelect = numSelectedCallInInventory();
        int allInventoryCell = viewInventoryTransform.childCount;
        
        for (int i = 0; i < allInventoryCell; i++)
        {
            if (viewInventoryTransform.GetChild(i).GetComponent<InventoryCell>().SelectedThisBool)
            {
                numSelect = i;
                break;
            }
        }

        switch (moveRightBool)
        {
            case true:
                if (numSelect + 1 < allInventoryCell)
                    numSelect++;
                else
                    numSelect = 0;
                break;
            case false:
                if (numSelect - 1 >= 0)
                    numSelect--;
                else
                    numSelect = allInventoryCell - 1;
                break;
        }

        selectGrenadeToInventory(viewInventoryTransform.GetChild(numSelect));
    }

    private void selectGrenadeToInventory(Transform newInventoryCell)
    {
        if (_selectedInventoryCellTransform != null)
            _selectedInventoryCellTransform.GetComponent<InventoryCell>().SelectedThis(false);
        newInventoryCell.GetComponent<InventoryCell>().SelectedThis(true);
        _playerFire.ActiveGrenadeGameObject = newInventoryCell.GetComponent<InventoryCell>().Grenade;
        _selectedInventoryCellTransform = newInventoryCell;
    }
}
