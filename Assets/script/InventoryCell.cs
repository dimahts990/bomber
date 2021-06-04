using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    public event Action Ejecting;
    
    [HideInInspector]
    public GameObject grenadeObject;
    [SerializeField] private Text _name;
    [SerializeField] private Image Icon;
    [SerializeField] private Image selectIcon;

    public void Render(GrenadeInfo grenade)
    {
        _name.text = grenade.name;
        Icon.sprite = grenade.Icon;
        
        
        /*
        if(transform.parent.GetChild(selectedNumberInt)==transform)
            SelectedThis(true);*/
    }

    public void Inject()
    {
        Ejecting?.Invoke();
    }

    public void SelectedThis(bool status)
    {            
        selectIcon.enabled = status;
    }
}