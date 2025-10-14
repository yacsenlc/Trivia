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

    public void SelecionarPregunta()
    {
        // se elige un indice del arreglo al azar
        preguntaAlAzar = Random.Range(0, bancoDePreguntas[nivelPregunta].preguntas.Length);
        // sacamos el texto del banco de preguntas y
        // lo ponemos en el UI donde se despliega el enunciado.
        enunciado.text = bancoDePreguntas[nivelPregunta].preguntas[preguntaAlAzar].enunciado;
        // cargar los textos de cada boton del UI
        for (int i = 0; i < respuesta.Length; i++)
        {
            respuesta[i].text =
           bancoDePreguntas[nivelPregunta].preguntas[preguntaAlAzar].respuestas[i].texto;
        }
        //string json = JsonUtility.ToJson(bancoDePreguntas);
        //Debug.Log(json);
    }

    

void Start()
 {
 nivelPregunta = 0;
 SelecionarPregunta();
 }




public bool EvaluarPregunta(int respuestaJugador)
 {
 if(respuestaJugador ==
bancoDePreguntas[nivelPregunta].preguntas[preguntaAlAzar].respuestaCorrecta)
 {
 // reinicio del problema con mayor dificultad
 nivelPregunta++;
 if (nivelPregunta == bancoDePreguntas.Length)
 {
 // desplegar la pantalla de fin de juego ganado!
 SceneManager.LoadScene("Gane");
 }
 else
 {
 //Desplegar el panel de información complementaria
 //ante una respuesta correcta
 try
 {
 panelComplementario.Desplegar();
 }
 catch(System.Exception ex)
 {
 Debug.LogError("Se te olvido configurar el panel de información complementaria: " + ex.Message);
 }

 HabilitarRespuestas();
 }
 return true;
 }
 else
 {
 SceneManager.LoadScene("Perder");
 return false;
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
 catch(System.Exception ex)
 {
 Debug.LogError("Falta configurar los botones");
 }

 }
 }


public void Respuesta(int respuestaJugador)
 {
 Debug.Log("Ha selecionado la opción " + respuestaJugador.ToString() );

 EvaluarPregunta(respuestaJugador);
 }



public int PreguntaActual()
 {
 return preguntaAlAzar;
 }
 
}





