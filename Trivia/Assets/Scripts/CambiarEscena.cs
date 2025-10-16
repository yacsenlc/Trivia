using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CambiarScena : MonoBehaviour
{
 /* public AudioClip sonidoInicio;
 private AudioSource audioSource; */
 public void CambiarScenaClick(string Juego)
 {
    /* audioSource = GetComponent<AudioSource>();

    if (sonidoInicio != null)
    {
        audioSource.PlayOneShot(sonidoInicio);
    } */
 SceneManager.LoadScene(Juego);
 }
 public void SalirJuego()
 {
 Application.Quit();
 }
}