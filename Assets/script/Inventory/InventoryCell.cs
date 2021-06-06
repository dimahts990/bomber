using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    //public AssetGrenade _AssetGrenade;
    [SerializeField] private Text _name;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _selectIcon;

    public GameObject Grenade;
    public bool SelectedThisBool = false;
    public void Render(AssetGrenade _assetGrenade)
    {
        _name.text = _assetGrenade.Name;
        _icon.sprite = _assetGrenade.Icon;
        Grenade = _assetGrenade._GrenadeGameObject;
    }

    public void SelectedThis(bool enable)
    {
        _selectIcon.enabled = enable;
        SelectedThisBool = enable;
    } 
}
