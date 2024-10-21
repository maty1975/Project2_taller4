using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desactivar_players : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Player2"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
