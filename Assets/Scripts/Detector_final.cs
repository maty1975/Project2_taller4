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
    [SerializeField] private bool cinemáticaActivada = false;

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

        // Comprobar si ambos jugadores están presentes
        if (player1 != null && player2 != null && !cinemáticaActivada)
        {
            cinemáticaActivada = true; // Marcar la cinemática como activada
            //cinematica.Play(); // Ejecutar la cinemática
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
