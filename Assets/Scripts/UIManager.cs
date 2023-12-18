using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Canvas gameCanvas;
    private void OnEnable()
    {
        CharacterEvents.CharacterTookDamaged += CharacterTookDamage;
        CharacterEvents.CharacterHealthed += CharacterHealthed;

    }
    private void OnDisable()
    {
        CharacterEvents.CharacterTookDamaged -= CharacterTookDamage;
        CharacterEvents.CharacterHealthed -= CharacterHealthed;
    }
    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }
    // Start is called before the first frame update
    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        Vector3 spawnPosition= Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText= Instantiate(damageTextPrefab,spawnPosition,Quaternion.identity,gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text=damageReceived.ToString();
    }
    public void CharacterHealthed(GameObject character, int healthReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = healthReceived.ToString();
    }
}
