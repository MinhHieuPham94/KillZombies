using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI score;
    public Slider health;
    private PlayerController playerControl;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerController>();
        health.maxValue = playerControl.playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health.value = playerControl.playerCurHealth;
        Score();
    }

    void Score()
    {
        int point = GameObject.Find("Player").GetComponent<PlayerController>().point;
        score.text = "Zombie Killed: "+ point;
    }
}
