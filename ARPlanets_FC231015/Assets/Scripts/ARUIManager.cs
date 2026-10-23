
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class ARUIManager : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public PlaceOnPlane placeOnPlane;
    public TextMeshProUGUI hintText;

    private bool planesVisible = true;

    void Update()
    {
        if (planeManager.trackables.count == 0)
            hintText.text = "Move your phone slowly to scan the floor or a table...";
        else if (!placeOnPlane.HasPlacedObject)
            hintText.text = "Surface found! Tap it to place the solar system.";
        else
            hintText.text = "Tap a planet for facts. Pinch to resize, twist to rotate.";
    }

    public void TogglePlanes()
    {
        planesVisible = !planesVisible;
        planeManager.enabled = planesVisible;

        foreach (ARPlane plane in planeManager.trackables)
            plane.gameObject.SetActive(planesVisible);
    }

    public void ResetScene()
    {
        placeOnPlane.RemovePlacedObject();
    }
}


