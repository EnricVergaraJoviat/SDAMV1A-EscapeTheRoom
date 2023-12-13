using System.Collections;
using TMPro;
using UnityEngine;

public class temporizadorManager : MonoBehaviour
{
    public TMP_Text timeManager;
    private float tiempoTranscurrido = 0f;

    void Start()
    {
        StartCoroutine(ActualizarTiempo());
    }

    IEnumerator ActualizarTiempo()
    {
        while (true)
        {
            tiempoTranscurrido += Time.deltaTime;

            int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60) % 100;

            int segundos = Mathf.FloorToInt(tiempoTranscurrido) % 60;

            string tiempoFormateado = string.Format("{0:00}:{1:00}", minutos, segundos);

            timeManager.text = tiempoFormateado;

            yield return null; 
        }
    }
}
