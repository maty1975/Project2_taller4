using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Importar el espacio de nombres para UnityEvent

public class StarConnector : MonoBehaviour
{
    public List<Transform> stars; // Lista de estrellas
    public Material lineMaterial; // Material de la línea
    public float lineDrawDuration = 1.0f; // Duración para dibujar cada línea (en segundos)
    public bool se_ejecuta;
    private LineRenderer lineRenderer;

    // Nueva propiedad para establecer el orden en la capa de renderizado
    public int lineOrderInLayer = 0;

    // UnityEvent que se ejecutará al completar el dibujo de la línea
    public UnityEvent onLinesComplete;

    private void Start()
    {
        if (se_ejecuta)
        {
            StartDrawingLines(); // Llamar a la función para dibujar líneas al inicio
        }
    }

    // Función pública para iniciar el dibujo de líneas
    [ContextMenu("Ejecutar Camino")]
    public void StartDrawingLines()
    {
        // Crea el LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.05f; // Grosor de la línea
        lineRenderer.endWidth = 0.05f; // Grosor de la línea
        lineRenderer.positionCount = stars.Count; // Número de puntos
        lineRenderer.useWorldSpace = true; // Usa el espacio mundial

        // Establecer el orden en la capa de renderizado
        lineRenderer.sortingOrder = lineOrderInLayer;

        // Inicia el Coroutine para dibujar las líneas
        StartCoroutine(DrawLinesGradually());
    }

    private IEnumerator DrawLinesGradually()
    {
        for (int i = 0; i < stars.Count - 1; i++)
        {
            lineRenderer.SetPosition(i, stars[i].position); // Establece la posición inicial
            lineRenderer.SetPosition(i + 1, stars[i + 1].position); // Establece la posición final

            // Hacer que la línea aparezca gradualmente
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime / lineDrawDuration; // Incrementar t con el tiempo según la duración
                lineRenderer.startWidth = Mathf.Lerp(0, 0.05f, t); // Cambia el grosor de la línea
                lineRenderer.endWidth = lineRenderer.startWidth;
                yield return null; // Esperar al siguiente frame
            }

            // Asegúrate de que la línea esté completamente dibujada
            lineRenderer.SetPosition(i + 1, stars[i + 1].position);
        }

        // Invocar el evento una vez que todas las líneas hayan sido dibujadas
        onLinesComplete.Invoke();
    }

    // Función para dibujar Gizmos en la escena y visualizar la conexión entre las estrellas
    private void OnDrawGizmos()
    {
        if (stars == null || stars.Count < 2)
            return;

        // Configura el color del Gizmo para las líneas
        Gizmos.color = Color.yellow;

        // Recorre la lista y dibuja una línea entre cada estrella
        for (int i = 0; i < stars.Count - 1; i++)
        {
            if (stars[i] != null && stars[i + 1] != null)
            {
                Gizmos.DrawLine(stars[i].position, stars[i + 1].position);
            }
        }
    }
}
