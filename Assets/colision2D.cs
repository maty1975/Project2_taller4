using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class colision2D : MonoBehaviour
{
    public string tag;
    public UnityEvent Colision_enter;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            Colision_enter.Invoke();
        }
    }
}
