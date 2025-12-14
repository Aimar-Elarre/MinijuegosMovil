using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImage))]
public class BasketTrackedImage : MonoBehaviour
{
    public GameObject cestaMoneda;
    public GameObject cestaMonstruo;

    private ARTrackedImage trackedImage;

    void Awake()
    {
        trackedImage = GetComponent<ARTrackedImage>();
    }

    void OnEnable()
    {
        ActualizarContenido();
    }

    void Update()
    {
        bool tracking = trackedImage.trackingState == TrackingState.Tracking;

        if (cestaMoneda != null) cestaMoneda.SetActive(tracking && trackedImage.referenceImage.name == "imagen_moneda");
        if (cestaMonstruo != null) cestaMonstruo.SetActive(tracking && trackedImage.referenceImage.name == "imagen_monstruo");
    }

    void ActualizarContenido()
    {
        if (cestaMoneda != null) cestaMoneda.SetActive(false);
        if (cestaMonstruo != null) cestaMonstruo.SetActive(false);
    }
}

