using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SistemaDeColisiones : MonoBehaviour
{

    public string tag;
    public UnityEvent onEnter2D,onStay2D,onExit2D;

    private void OnTriggerExit2D(Collider2D collision)
    {
        {
            if (collision.CompareTag(tag))
            {
                onExit2D.Invoke();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(tag))
        {
            onStay2D.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(tag))
        {
            onEnter2D.Invoke();
        }
    }
    public void Evento()
    {
        onEnter2D.Invoke();
    }



}
