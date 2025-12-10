using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MarkerTracker : MonoBehaviour
{
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
            if (img.trackingState == TrackingState.Tracking)
                gameManager.OnMarkerFound();
        }

        foreach (var img in args.updated)
        {
            if (img.trackingState == TrackingState.Tracking)
                gameManager.OnMarkerFound();
            else
                gameManager.OnMarkerLost();
        }

        foreach (var img in args.removed)
        {
            gameManager.OnMarkerLost();
        }
    }
}
