using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;

public class Objetivo : MonoBehaviour
{
    Espaunear espaunear;
    [Header("HAY QUE ASIGNARSELOS")]
    public AudioSource audioSource;
    public AudioClip audio;
    public PlayableDirector cinematica_Final;
    public GameObject jugador;

    private void Start()
    {
        espaunear = GetComponent<Espaunear>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
            audioSource.PlayOneShot(audio);
            if (espaunear != null)
            {
                jugador.SetActive(false);
                espaunear.Spawn();
                this.gameObject.SetActive(false);
            }
           
            cinematica_Final.Play();
        }
    }
}
