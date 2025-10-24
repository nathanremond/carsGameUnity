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

    void Start()
    {
        if (PlayerPrefs.HasKey("MeilleurTemps"))
        {
            meilleurTemps = PlayerPrefs.GetFloat("MeilleurTemps");
        }

        if (meilleurTempsText != null)
            meilleurTempsText.text = "Meilleur Temps: " + ObtenirMeilleurTemps();
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
        temps = 0f;
        enCours = true;
        Debug.Log("Chrono démarré !");
    }

    public void ArreterChrono()
    {
        enCours = false;

        if (temps < meilleurTemps)
        {
            meilleurTemps = temps;
            PlayerPrefs.SetFloat("MeilleurTemps", meilleurTemps);
            PlayerPrefs.Save();

            Debug.Log("Nouveau record : " + FormatTemps(meilleurTemps));
        }

        if (meilleurTempsText != null)
            meilleurTempsText.text = "Meilleur Temps : " + ObtenirMeilleurTemps();
    }

    public string FormatTemps(float t)
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
