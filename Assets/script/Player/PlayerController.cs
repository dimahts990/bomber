using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int HP;
    [SerializeField] private Slider HPShow;
    [SerializeField] private GameOver _gameOver;

    private void Start()
    {
        renderHP(true);
    }

    public void DamageMe(int damage)
    {
        HP -= damage;
        renderHP();
        if (HP <= 0)
        {
            _gameOver.ShowGameOverCanvas();
            Destroy(gameObject);
        }
    }

    private void renderHP(bool newGame = false)
    {
        if (newGame)
            HPShow.maxValue = HP;
        HPShow.value = HP;
    }
}
