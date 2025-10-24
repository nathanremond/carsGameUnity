using UnityEngine;
using TMPro;

public class Chronometre : MonoBehaviour
{
    [Header("Références UI")]
    public TextMeshProUGUI chronoText;
    public TextMeshProUGUI meilleurTempsText;

    private float temps = 0f;
    private bool enCours = false;
    private float meilleurTemps = Mathf.Infinity;
    private bool premierTour = true;

    void Start()
    {
        meilleurTemps = Mathf.Infinity;
        premierTour = true;

        if (meilleurTempsText != null)
            meilleurTempsText.text = "Meilleur Temps : --:--.---";
    }

    void Update()
    {
        if (enCours)
        {
            temps += Time.deltaTime;
            if (chronoText != null)
                chronoText.text = FormatTemps(temps);
        }
    }

    public void DemarrerChrono()
    {
        if (!enCours)
        {
            temps = 0f;
            enCours = true;
            Debug.Log("⏱️ Chrono démarré !");
        }
    }

    public void ArreterChrono()
    {
        enCours = false;

        if (premierTour)
        {
            meilleurTemps = temps;
            premierTour = false;
            Debug.Log("🏁 Premier tour : " + FormatTemps(meilleurTemps));
        }
        else
        {
            // Si c’est un meilleur temps
            if (temps < meilleurTemps)
            {
                meilleurTemps = temps;
                Debug.Log("🏆 Nouveau meilleur tour : " + FormatTemps(meilleurTemps));
            }
        }

        if (meilleurTempsText != null)
            meilleurTempsText.text = "Meilleur Temps : " + ObtenirMeilleurTemps();

        DemarrerChrono();
    }

    private string FormatTemps(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60f);
        int secondes = Mathf.FloorToInt(t % 60f);
        int millisecondes = Mathf.FloorToInt((t * 1000) % 1000);
        return string.Format("{0:00}:{1:00}.{2:000}", minutes, secondes, millisecondes);
    }

    public string ObtenirMeilleurTemps()
    {
        if (meilleurTemps < Mathf.Infinity)
            return FormatTemps(meilleurTemps);
        else
            return "--:--.---";
    }
}
