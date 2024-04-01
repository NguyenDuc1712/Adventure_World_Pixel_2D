using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    // pixel moi giay
    public Vector3 moveSpeed = new Vector3(0, 80, 0);
    public float timeFade = 0.5f;
    RectTransform textTransform;

    TextMeshProUGUI TMP;
    private float timeElapsed = 0f;
    private Color startColor;
    // Start is called before the first frame update
    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        TMP = GetComponent<TextMeshProUGUI>();
        startColor = TMP.color;
    }

    // Update is called once per frame
    void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;
        timeElapsed += Time.deltaTime;
        if (timeElapsed < timeFade)
        {
            float fadeAlpha = startColor.a * (1 - timeElapsed / timeFade);
            TMP.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
