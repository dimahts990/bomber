using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Grenade")]
public class AssetGrenade :ScriptableObject, GrenadeInfo
{
    public string name => _name;
    public Sprite Icon => _Icon;
    
    [SerializeField]private string _name;
    [SerializeField]private Sprite _Icon;
}