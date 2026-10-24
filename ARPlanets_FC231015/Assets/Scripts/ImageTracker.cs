
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    [System.Serializable]
    public class ImagePrefab
    {
        public string imageName;
        public GameObject prefab;
    }

    public ARTrackedImageManager trackedImageManager;
    public List<ImagePrefab> imagePrefabs = new List<ImagePrefab>();
    public float heightAboveImage = 0.05f;

    [Header("Sound (optional)")]
    public AudioSource audioSource;
    public AudioClip foundSound;

    private Dictionary<TrackableId, GameObject> spawned =
        new Dictionary<TrackableId, GameObject>();

    void OnEnable()
    {
        if (trackedImageManager != null)
            trackedImageManager.trackablesChanged.AddListener(OnImagesChanged);
    }

    void OnDisable()
    {
        if (trackedImageManager != null)
            trackedImageManager.trackablesChanged.RemoveListener(OnImagesChanged);
    }

    void OnImagesChanged(
        ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (ARTrackedImage image in args.added)
        {
            SpawnFor(image);
        }

        foreach (ARTrackedImage image in args.updated)
        {
            if (spawned.TryGetValue(image.trackableId, out GameObject obj))
            {
                obj.SetActive(
                    image.trackingState == TrackingState.Tracking
                );
            }
        }
    }

    void SpawnFor(ARTrackedImage image)
    {
        string name = image.referenceImage.name;

        foreach (ImagePrefab item in imagePrefabs)
        {
            if (item.imageName == name && item.prefab != null)
            {
                GameObject obj = Instantiate(item.prefab, image.transform);

                obj.transform.localPosition =
                    new Vector3(0f, heightAboveImage, 0f);

                spawned[image.trackableId] = obj;

                if (audioSource != null && foundSound != null)
                    audioSource.PlayOneShot(foundSound);

                return;
            }
        }

        Debug.Log("No prefab for image: " + name);
    }
}

