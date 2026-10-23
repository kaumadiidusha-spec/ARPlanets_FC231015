
using UnityEngine;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    public static InfoPanel instance;

    public GameObject panel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI factText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        panel.SetActive(false);
    }

    public void Show(Planet planet)
    {
        titleText.text = planet.planetName;
        factText.text = planet.fact;
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}

