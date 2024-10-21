using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class interruptor_colaiders : MonoBehaviour
{
    bool activado = true;
    public Color coloron;
    public Color coloroff;

    public bool EJECUTAR = true;
    public LayerMask capadetect;
    public LayerMask capa_no_detect;

    [Header("PRIMERA LISTA")]
    public Material material_de_colision1;
    public Collider2D[] colisiones1;

    [Header("SEGUNDA LISTA")]
    public Material material_de_colision2;
    public Collider2D[] colisiones2;

    [Header("EVENTOS")]
    public UnityEvent SE_ENCENDIO;
    public UnityEvent SE_APAGO;

    public float duracionTransicion = 1.0f; // Duración de la transición de color

    // Start is called before the first frame update
    void Start()
    {
        if (EJECUTAR)
        {
            switch_collider();
        }
    }

    [ContextMenu("EJECUTAR")]
    public void switch_collider()
    {
        if (activado)
        {
            if (colisiones1 != null && colisiones1.Length > 0)
            {
                for (int i = 0; i < colisiones1.Length; i++)
                {
                    if (colisiones1[i] != null) // Asegúrate de que no sea null
                    {
                        colisiones1[i].enabled = true;
                        colisiones1[i].gameObject.layer = LayerMaskToLayer(capadetect); // Simplificado
                    }
                    else
                    {
                        Debug.LogWarning("colisiones1[" + i + "] es null");
                    }
                }
            }
            if (colisiones2 != null && colisiones2.Length > 0)
            {
                for (int j = 0; j < colisiones2.Length; j++)
                {
                    if (colisiones2[j] != null) // Asegúrate de que no sea null
                    {
                        colisiones2[j].enabled = false;
                        colisiones2[j].gameObject.layer = LayerMaskToLayer(capa_no_detect); // Simplificado
                    }
                    else
                    {
                        Debug.LogWarning("colisiones2[" + j + "] es null");
                    }
                }
            }

            // Cambiar los colores de los materiales suavemente a "coloron"
            StartCoroutine(InterpolateColor(material_de_colision1, coloron));
            StartCoroutine(InterpolateColor(material_de_colision2, coloroff));

            SE_ENCENDIO.Invoke();
            activado = false;
        }
        else
        {
            for (int i = 0; i < colisiones1.Length; i++)
            {
                if (colisiones1[i] != null)
                {
                    colisiones1[i].enabled = false;
                    colisiones1[i].gameObject.layer = LayerMaskToLayer(capa_no_detect); // Simplificado
                }
            }
            for (int j = 0; j < colisiones2.Length; j++)
            {
                if (colisiones2[j] != null)
                {
                    colisiones2[j].enabled = true;
                    colisiones2[j].gameObject.layer = LayerMaskToLayer(capadetect); // Simplificado
                }
            }

            // Cambiar los colores de los materiales suavemente a "coloroff"
            StartCoroutine(InterpolateColor(material_de_colision1, coloroff));
            StartCoroutine(InterpolateColor(material_de_colision2, coloron));

            SE_APAGO.Invoke();
            activado = true;
        }
    }

    // Corrutina para cambiar el color de un material de forma suave
    IEnumerator InterpolateColor(Material material, Color targetColor)
    {
        if (material == null) yield break; // Comprobación de material nulo
        Color currentColor = material.color;
        float tiempo = 0;

        while (tiempo < duracionTransicion)
        {
            // Interpolación de color
            material.color = Color.Lerp(currentColor, targetColor, tiempo / duracionTransicion);
            tiempo += Time.deltaTime;
            yield return null; // Esperar un frame
        }

        // Asegurar que al final se aplique el color exacto
        material.color = targetColor;
    }

    // Función para convertir LayerMask a layer index
    int LayerMaskToLayer(LayerMask mask)
    {
        int layer = (int)Mathf.Log(mask.value, 2);
        return layer;
    }
}
