using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class Detector_final : MonoBehaviour
{
    public BoxCollider2D player1;
    public BoxCollider2D player2;
    public PlayableDirector cinematica;
    public UnityEvent SE_SALIO;
    [SerializeField] private bool cinem�ticaActivada = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Detectar a ambos jugadores
        if (collision.CompareTag("Player"))
        {
            player1 = collision.GetComponent<BoxCollider2D>();
        }
        else if (collision.CompareTag("Player2"))
        {
            player2 = collision.GetComponent<BoxCollider2D>();
        }

        // Comprobar si ambos jugadores est�n presentes
        if (player1 != null && player2 != null && !cinem�ticaActivada)
        {
            cinem�ticaActivada = true; // Marcar la cinem�tica como activada
            //cinematica.Play(); // Ejecutar la cinem�tica
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player1 = null;
        }
        else if (collision.CompareTag("Player2"))
        {
            player2 = null;
        }
    }
}
