using UnityEngine;

public class LigneArrivee : MonoBehaviour
{
    [Header("Référence du chronomètre")]
    public Chronometre chrono;

    private bool premierPassage = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            if (!premierPassage)
            {
                chrono.DemarrerChrono();
                premierPassage = true;
                Debug.Log("Chrono démarré !");
            }
            else
            {
                chrono.ArreterChrono();
                premierPassage = false;
                Debug.Log("Chrono arrêté !");
            }
        }
    }
}

