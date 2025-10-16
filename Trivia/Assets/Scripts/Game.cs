using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public Dificultad[] bancoDePreguntas;
    public Text enunciado;
    public Text[] respuesta;
    public int nivelPregunta;
    public int preguntaAlAzar;
    public PanelComplementario panelComplementario;
    public Button[] btn_respuesta;

    public AudioClip sonidoInicio;
    public AudioClip sonidoGanar;
    public AudioClip sonidoPerder;

    public Text textoTimer; // ← (opcional) Mostrar el temporizador en UI

    private AudioSource audioSource;

    private float tiempoMaximo = 10f;
    private Coroutine temporizadorCoroutine;
    private bool preguntaRespondida;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (sonidoInicio != null)
        {
            audioSource.PlayOneShot(sonidoInicio);
        }

        nivelPregunta = 0;
        SelecionarPregunta();
    }

    public void SelecionarPregunta()
    {
        preguntaRespondida = false;

        preguntaAlAzar = Random.Range(0, bancoDePreguntas[nivelPregunta].preguntas.Length);
        enunciado.text = bancoDePreguntas[nivelPregunta].preguntas[preguntaAlAzar].enunciado;

        for (int i = 0; i < respuesta.Length; i++)
        {
            respuesta[i].text = bancoDePreguntas[nivelPregunta].preguntas[preguntaAlAzar].respuestas[i].texto;
        }

        // Inicia el temporizador
        if (temporizadorCoroutine != null)
        {
            StopCoroutine(temporizadorCoroutine);
        }
        temporizadorCoroutine = StartCoroutine(TemporizadorPregunta());
    }

    IEnumerator TemporizadorPregunta()
    {
        float tiempoRestante = tiempoMaximo;

        while (tiempoRestante > 0)
        {
            if (textoTimer != null)
                textoTimer.text = "Tiempo: " + Mathf.CeilToInt(tiempoRestante).ToString();

            yield return new WaitForSeconds(1f);
            tiempoRestante--;

            // Si ya respondió, salimos de la corutina
            if (preguntaRespondida)
            
                yield break;
        }

        // Si se acaba el tiempo y no respondió, pierde
        if (!preguntaRespondida)
        {
            Debug.Log("Tiempo agotado. Has perdido.");
            StartCoroutine(Perder());
        }
    }

    public bool EvaluarPregunta(int respuestaJugador)
    {
        // Marca como respondida y detiene el temporizador
        preguntaRespondida = true;
       

        if (temporizadorCoroutine != null)
        {  
            StopCoroutine(temporizadorCoroutine);
        }

        if (respuestaJugador == bancoDePreguntas[nivelPregunta].preguntas[preguntaAlAzar].respuestaCorrecta)
        { 
            nivelPregunta++;
            if (nivelPregunta == bancoDePreguntas.Length)
            {
                
                StartCoroutine(Ganar());
            }
            else
            {
                try
                {
                    
                    panelComplementario.Desplegar();
                    
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Se te olvido configurar el panel de información complementaria: " + ex.Message);
                }

                HabilitarRespuestas();
                 SelecionarPregunta();  // ← Muestra la siguiente pregunta
            }
            return true;
        }
        else
        {
            StartCoroutine(Perder());
            return false;
        }
    }

    IEnumerator Ganar()
    {
        if (sonidoGanar != null)
        {
            SceneManager.LoadScene("Gane");
            audioSource.PlayOneShot(sonidoGanar);
            yield return new WaitForSeconds(sonidoGanar.length);
        }

        
    }

    IEnumerator Perder()
    {
        if (sonidoPerder != null)
        {
            SceneManager.LoadScene("Perder");
            audioSource.PlayOneShot(sonidoPerder);
            yield return new WaitForSeconds(sonidoPerder.length);
        }

        
    }

    public void HabilitarRespuestas()
    {
        for (int i = 0; i < respuesta.Length; i++)
        {
            try
            {
                btn_respuesta[i].gameObject.SetActive(true);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Falta configurar los botones");
            }
        }
    }

    public void Respuesta(int respuestaJugador)
    {
        Debug.Log("Ha seleccionado la opción " + respuestaJugador.ToString());
        EvaluarPregunta(respuestaJugador);
    }

    public int PreguntaActual()
    {
        return preguntaAlAzar;
    }
}