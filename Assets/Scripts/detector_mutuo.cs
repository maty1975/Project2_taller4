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
            if (interruptor != null)
            {
                interruptor.switch_collider();
            }
            Debug.Log("se ejecuto");
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

        // Reiniciar la cinem�tica si ambos jugadores salen
        if (player1 == null && player2 == null)
        {
            SE_SALIO.Invoke();
            cinem�ticaActivada = false; // Reiniciar el estado de la cinem�tica
        }
    }
}
