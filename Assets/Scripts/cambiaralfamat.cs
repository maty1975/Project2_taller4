using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambiaralfamat : MonoBehaviour
{
    // Asigna el material en el Inspector
    public Material materialToChange;

    // Rango de transparencia (0 = completamente transparente, 1 = completamente opaco)
    [Range(0, 1)] public float alphaLevel = 1f;

    // Update se llama en cada frame
    void Update()
    {
        if (materialToChange != null)
        {
            // Obtiene el color actual del material
            Color color = materialToChange.color;

            // Cambia el valor del alpha
            color.a = alphaLevel;

            // Asigna el color modificado de vuelta al material
            materialToChange.color = color;
        }
        else
        {
            Debug.LogWarning("No hay material asignado en 'materialToChange'.");
        }
    }
}
