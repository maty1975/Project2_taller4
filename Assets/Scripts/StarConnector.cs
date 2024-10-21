using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Importar el espacio de nombres para UnityEvent

public class StarConnector : MonoBehaviour
{
    public List<Transform> stars; // Lista de estrellas
    public Material lineMaterial; // Material de la l�nea
    public float lineDrawDuration = 1.0f; // Duraci�n para dibujar cada l�nea (en segundos)
    public bool se_ejecuta;
    private LineRenderer lineRenderer;

    // Nueva propiedad para establecer el orden en la capa de renderizado
    public int lineOrderInLayer = 0;

    // UnityEvent que se ejecutar� al completar el dibujo de la l�nea
    public UnityEvent onLinesComplete;

    private void Start()
    {
        if (se_ejecuta)
        {
            StartDrawingLines(); // Llamar a la funci�n para dibujar l�neas al inicio
        }
    }

    // Funci�n p�blica para iniciar el dibujo de l�neas
    [ContextMenu("Ejecutar Camino")]
    public void StartDrawingLines()
    {
        // Crea el LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.05f; // Grosor de la l�nea
        lineRenderer.endWidth = 0.05f; // Grosor de la l�nea
        lineRenderer.positionCount = stars.Count; // N�mero de puntos
        lineRenderer.useWorldSpace = true; // Usa el espacio mundial

        // Establecer el orden en la capa de renderizado
        lineRenderer.sortingOrder = lineOrderInLayer;

        // Inicia el Coroutine para dibujar las l�neas
        StartCoroutine(DrawLinesGradually());
    }

    private IEnumerator DrawLinesGradually()
    {
        for (int i = 0; i < stars.Count - 1; i++)
        {
            lineRenderer.SetPosition(i, stars[i].position); // Establece la posici�n inicial
            lineRenderer.SetPosition(i + 1, stars[i + 1].position); // Establece la posici�n final

            // Hacer que la l�nea aparezca gradualmente
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime / lineDrawDuration; // Incrementar t con el tiempo seg�n la duraci�n
                lineRenderer.startWidth = Mathf.Lerp(0, 0.05f, t); // Cambia el grosor de la l�nea
                lineRenderer.endWidth = lineRenderer.startWidth;
                yield return null; // Esperar al siguiente frame
            }

            // Aseg�rate de que la l�nea est� completamente dibujada
            lineRenderer.SetPosition(i + 1, stars[i + 1].position);
        }

        // Invocar el evento una vez que todas las l�neas hayan sido dibujadas
        onLinesComplete.Invoke();
    }

    // Funci�n para dibujar Gizmos en la escena y visualizar la conexi�n entre las estrellas
    private void OnDrawGizmos()
    {
        if (stars == null || stars.Count < 2)
            return;

        // Configura el color del Gizmo para las l�neas
        Gizmos.color = Color.yellow;

        // Recorre la lista y dibuja una l�nea entre cada estrella
        for (int i = 0; i < stars.Count - 1; i++)
        {
            if (stars[i] != null && stars[i + 1] != null)
            {
                Gizmos.DrawLine(stars[i].position, stars[i + 1].position);
            }
        }
    }
}
