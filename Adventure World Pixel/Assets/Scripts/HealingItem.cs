using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    public int healingItem = 20;
    private Vector3 rotationItem = new Vector3(0, 180,0);
    // Start is called before the first frame update
    void Start()
    {

    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage = collision.GetComponent<Damage>();
        if (damage)
            damage.Heal(healingItem);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += rotationItem * Time.deltaTime;
    }
}
