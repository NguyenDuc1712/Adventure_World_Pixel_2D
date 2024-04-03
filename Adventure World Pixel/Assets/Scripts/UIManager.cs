using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Canvas gameCanvas;
    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();

    }
    private void OnEnable() // Bật kích hoạt
    {

        CharacterAction.characterDamaged += (CharacterTooKDamage);
        CharacterAction.characterHeal += (CharacterHealth);
    }
    private void OnDisable() //Tắt kích hoạt 
    {
        CharacterAction.characterDamaged -= (CharacterTooKDamage);
        CharacterAction.characterHeal -= (CharacterHealth);
    }

    public void CharacterTooKDamage(GameObject character, int damageReceived)
    {
        //tạo văn bản tại ký tự khi danh
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = damageReceived.ToString();
    }
    public void CharacterHealth(GameObject character, int healthRestored)
    {
        // tao van ban ki tu khi phuc hoi suc khoe
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponentInChildren<TMP_Text>();
        tmpText.text = healthRestored.ToString();
    }
    
    public void OnExit(InputAction.CallbackContext context)

    {

    }
}