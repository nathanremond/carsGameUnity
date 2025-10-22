using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jouer()
    {
        SceneManager.LoadScene("Jeux");
    }

    public void Quitter()
    {
        Application.Quit();
        Debug.Log("Quitter le jeu"); 
    }
}
