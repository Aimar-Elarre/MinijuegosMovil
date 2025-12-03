using UnityEngine;
using UnityEngine.UI;
using TMPro;
// using CesiumForUnity; // descomenta y ajusta si usas CesiumGlobeAnchor

public class GPSPlayerController : MonoBehaviour
{
    [Header("UI Coordenadas")]
    public Text latText;
    public Text lonText;
    public Text altText;



    private bool gpsReady = false;

    private void Start()
    {
        StartCoroutine(StartLocationService());
    }

    private System.Collections.IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogWarning("Localización desactivada.");
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0 || Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogWarning("No se pudo obtener la ubicación GPS.");
            yield break;
        }

        gpsReady = true;
    }

    private void Update()
    {
        if (!gpsReady) return;

        LocationInfo li = Input.location.lastData;

        if (latText != null) latText.text = "LAT: " + li.latitude.ToString("F6");
        if (lonText != null) lonText.text = "LON: " + li.longitude.ToString("F6");
        if (altText != null) altText.text = "ALT: " + li.altitude.ToString("F1") + " m";


    }
}
