using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayo : MonoBehaviour
{
    public Transform final;
    public int cantidadDePuntos;
    public float dispersion;
    public float frecuencia;
    public float velocidadY; // Velocidad en el eje Y

    public float minY; // Límite mínimo en el eje Y
    public float maxY; // Límite máximo en el eje Y

    private LineRenderer line;
    private float tiempo = 0;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = cantidadDePuntos; // Establecer la cantidad inicial de puntos en el LineRenderer

        // Actualizar los puntos del LineRenderer en el inicio
        ActualizarPuntos();
    }

    void Update()
    {
        tiempo += Time.deltaTime;

        if (tiempo > frecuencia)
        {
            ActualizarPuntos();
            tiempo = 0;
        }

        // Mover el Rayo solo en el eje Y dentro de los límites minY y maxY
        Vector3 newPosition = transform.position;
        newPosition.y += velocidadY * Time.deltaTime; // Mover en el eje Y
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY); // Aplicar restricciones en Y
        transform.position = newPosition;
    }

    private void ActualizarPuntos()
    {
        // Obtener la posición del transform actual y la posición final
        Vector3 principio = transform.position;
        Vector3 fin = final.position;

        // Interpolar los puntos entre el principio y el final
        List<Vector3> puntos = InterpolarPuntos(principio, fin, cantidadDePuntos);

        // Establecer los puntos en el LineRenderer
        line.SetPositions(puntos.ToArray());
    }

    private List<Vector3> InterpolarPuntos(Vector3 principio, Vector3 fin, int totalPoints)
    {
        List<Vector3> lista = new List<Vector3>();

        for (int i = 0; i < totalPoints; i++)
        {
            float t = (float)i / (totalPoints - 1);
            Vector3 puntoInterpolado = Vector3.Lerp(principio, fin, t) + DesfaseAleatorio();
            lista.Add(puntoInterpolado);
        }

        return lista;
    }

    private Vector3 DesfaseAleatorio()
    {
        return Random.insideUnitCircle.normalized * dispersion;
    }
}
