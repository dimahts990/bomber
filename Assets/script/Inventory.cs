using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    /*[HideInInspector]*/ public int SelectedActiveInt;
    [SerializeField] private playerAttackSystem _playerAttackSystem;
    [SerializeField] private List<AssetGrenade> Grenades;
    [SerializeField] private InventoryCell _inventoryCell;
    

    public void OnEnable()
    {
        Render(Grenades);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            rearrangingTheSelection(false);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            rearrangingTheSelection(true);
        
        if(Input.GetKeyDown(KeyCode.Q))
            Debug.Log(transform.childCount);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void rearrangingTheSelection(bool right)
    {
        if (transform.childCount != 0)
        {
            switch (right)
            {
                case true:
                    transform.GetChild(SelectedActiveInt).GetComponent<InventoryCell>().SelectedThis(false);
                    if (SelectedActiveInt + 1 >= transform.childCount)
                        SelectedActiveInt = 0;
                    else
                        SelectedActiveInt += 1;
                    transform.GetChild(SelectedActiveInt).GetComponent<InventoryCell>().SelectedThis(true);
                    _playerAttackSystem.ActiveGrenadeGameObject = transform.GetChild(SelectedActiveInt)
                        .GetComponent<InventoryCell>().grenadeObject;
                    break;
                case false:
                    transform.GetChild(SelectedActiveInt).GetComponent<InventoryCell>().SelectedThis(false);
                    if (SelectedActiveInt - 1 <= -1)
                        SelectedActiveInt = transform.childCount - 1;
                    else
                        SelectedActiveInt -= 1;
                    transform.GetChild(SelectedActiveInt).GetComponent<InventoryCell>().SelectedThis(true);
                    _playerAttackSystem.ActiveGrenadeGameObject = transform.GetChild(SelectedActiveInt)
                        .GetComponent<InventoryCell>().grenadeObject;
                    break;
            }
            _playerAttackSystem.ActiveGrenadeGameObject=transform.GetChild(SelectedActiveInt).GetComponent<InventoryCell>().grenadeObject;
        }
    }
    
    public void AddInventory(AssetGrenade _assetGrenade, GameObject grenadeObject)
    {
        Grenades.Add(_assetGrenade);
        Render(Grenades,grenadeObject);
        
    }
    
    public void Render(List<AssetGrenade> grenades,GameObject grenadeObject=null)
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        
        grenades.ForEach(GrenadeInfo =>
        {
            var cell = Instantiate(_inventoryCell, transform);
            if (grenadeObject != null)
                cell.grenadeObject = grenadeObject;
            cell.Render(GrenadeInfo);

            cell.Ejecting += () =>
            {
                grenades.RemoveAt(SelectedActiveInt);
                if(SelectedActiveInt>0)
                    SelectedActiveInt -= 1;
                Destroy(cell.gameObject);
            };
        });

        /*foreach (Transform child in transform)
        {
            if(transform.GetChild(SelectedActiveInt)==child.transform)
                child.GetComponent<InventoryCell>().SelectedThis(true);

        }*/
        
        
        
        /*if (viewPanel.childCount > 1)
        {
            Debug.Log($"{viewPanel.GetChild(SelectedActiveInt).name} {SelectedActiveInt}");
            viewPanel.GetChild(SelectedActiveInt).GetComponent<InventoryCell>().SelectedThis(true);
            _playerAttackSystem.ActiveGrenadeGameObject=viewPanel.GetChild(SelectedActiveInt).GetComponent<InventoryCell>().grenadeObject;
        }else if (viewPanel.childCount == 1 )
        {
            Debug.Log("1");
            SelectedActiveInt = 0;
            viewPanel.GetChild(SelectedActiveInt).GetComponent<InventoryCell>().SelectedThis(true);
            _playerAttackSystem.ActiveGrenadeGameObject=viewPanel.GetChild(SelectedActiveInt).GetComponent<InventoryCell>().grenadeObject;
        }*/
        //rearrangingTheSelection(true);
    }
}
