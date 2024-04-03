using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealBar : MonoBehaviour
{
    Damage playerDamage;
    public TMP_Text healBarText;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {

        slider.value = CalculateliderPerentage(playerDamage.Health, playerDamage.MaxHealth);
        healBarText.text = " HP " + playerDamage.Health + " / " + playerDamage.MaxHealth;
    }
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamage = player.GetComponent<Damage>();

    }
    private void OnEnable()
    {
        playerDamage.healChange.AddListener(PlayerHealthChange);
    }
    private void OnDisable()
    {
        playerDamage.healChange.RemoveListener(PlayerHealthChange);
    }

    private float CalculateliderPerentage(float currentHeath, float maxHealth) 
    {
        return currentHeath / maxHealth;
    }
    void PlayerHealthChange(int newHeal, int maxHealth)
    {

        slider.value = CalculateliderPerentage(newHeal, maxHealth);
        healBarText.text = " HP " + newHeal + " / " + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
