using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Zone : MonoBehaviour
{
    public UnityEvent noCollider2D;
    public List<Collider2D> Collider2D = new List<Collider2D>();
    Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D.Add(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Collider2D.Remove(collision);
        if (Collider2D.Count <= 0)
        {
            noCollider2D.Invoke(); 
        }
    }

}

