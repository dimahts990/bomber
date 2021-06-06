using System;
using UnityEngine;

interface IGrenade
{
    string Name { get; }
    Sprite Icon { get; }
    int DamageRadius { get; }
    GameObject GrenadeGameObject { get; set; }
}
