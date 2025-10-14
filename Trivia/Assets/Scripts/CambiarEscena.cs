using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CambiarScena : MonoBehaviour
{
 public void CambiarScenaClick(string Juego)
 {
 SceneManager.LoadScene(Juego);
 }
 public void SalirJuego()
 {
 Application.Quit();
 }
}