using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class BasketController : MonoBehaviour
{
    public ARTrackedImageManager gestor;
    public GameObject cesta1;
    public GameObject cesta2;

    void OnEnable()
    {
        gestor.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        gestor.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            CrearCesta(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).gameObject.SetActive(
                    trackedImage.trackingState == TrackingState.Tracking
                );
            }
        }
    }

    void CrearCesta(ARTrackedImage trackedImage)
    {
        GameObject prefab = null;

        switch (trackedImage.referenceImage.name)
        {
            case "imagen_moneda":
                prefab = cesta1;
                break;

            case "imagen_monstruo":
                prefab = cesta2;
                break;
        }

        if (prefab == null) return;

        Instantiate(
            prefab,
            trackedImage.transform.position,
            trackedImage.transform.rotation,
            trackedImage.transform
        );
    }
}
