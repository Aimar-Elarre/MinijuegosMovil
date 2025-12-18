using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MarkerTracker : MonoBehaviour
{
    public GameObject cestaMoneda;
    public GameObject cestaMonstruo;

    public ARTrackedImageManager trackedImageManager;
    public GameManagerAR gameManager;

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var img in args.added)
        {
            UpdateImage(img);
        }

        foreach (var img in args.updated)
        {
            UpdateImage(img);
        }

        foreach (var img in args.removed)
        {
            ClearImage(img);
            gameManager.OnMarkerLost();
        }
    }

    void UpdateImage(ARTrackedImage img)
    {
        //SIEMPRE limpiamos primero (clave para evitar conflictos)
        ClearImage(img);

        if (img.trackingState != TrackingState.Tracking)
        {
            gameManager.OnMarkerLost();
            return;
        }

        gameManager.OnMarkerFound();

        GameObject prefab = null;

        if (img.referenceImage.name == "imagen_monstruo")
            prefab = cestaMonstruo;
        else if (img.referenceImage.name == "imagen_moneda")
            prefab = cestaMoneda;

        if (prefab == null)
            return;

        GameObject obj = Instantiate(prefab);
        obj.transform.SetParent(img.transform, false);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;
    }

    void ClearImage(ARTrackedImage img)
    {
        for (int i = img.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(img.transform.GetChild(i).gameObject);
        }
    }
}
