using UnityEngine;

public class LigneArrivee : MonoBehaviour
{
    [Header("Référence du chronomètre")]
    public Chronometre chrono;

    private bool chronoEnCours = false;
    private bool cooldown = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!cooldown && collision.gameObject.CompareTag("Car"))
        {
            cooldown = true;
            Invoke(nameof(ReinitialiserCooldown), 1f);

            if (!chronoEnCours)
            {
                chrono.DemarrerChrono();
                chronoEnCours = true;
                Debug.Log("🚦 Début du chrono !");
            }
            else
            {
                chrono.ArreterChrono();
                Debug.Log("🏁 Tour terminé !");
            }
        }
    }

    private void ReinitialiserCooldown()
    {
        cooldown = false;
    }
}
