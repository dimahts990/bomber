using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public playerAttackSystem PlayerAttackSystem;
    public PlayerMove PlayerMove;
    public Inventory Inventory;
    public GrabGrenade GrabGrenade;

    private void Awake()
    {
        PlayerAttackSystem = GetComponent<playerAttackSystem>();
        PlayerMove = GetComponent<PlayerMove>();
        Inventory = GetComponent<Inventory>();
    }
}