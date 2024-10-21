using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Events;
using UnityEngine;

public class DetectorMutuo : MonoBehaviour
{
    public interruptor_colaiders interruptor;
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
            if (interruptor != null)
            {
                interruptor.switch_collider();
            }
            Debug.Log("se ejecuto");
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

        // Reiniciar la cinemática si ambos jugadores salen
        if (player1 == null && player2 == null)
        {
            SE_SALIO.Invoke();
            cinemáticaActivada = false; // Reiniciar el estado de la cinemática
        }
    }
}
