using UnityEngine;


[CreateAssetMenu(menuName = "Grenade")]
public class AssetGrenade :ScriptableObject,IGrenade
{
    public string Name => _name;
    public Sprite Icon => _Icon;
    public int DamageRadius => _DamageRadius;
    public GameObject GrenadeGameObject
    {
        get => _GrenadeGameObject;
        set => _GrenadeGameObject = null;
    }

    [SerializeField] private string _name;
    [SerializeField] private Sprite _Icon;
    [SerializeField] private int _DamageRadius;
    public GameObject _GrenadeGameObject;

}