using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI textoContBalas;
    public TextMeshProUGUI victoryMessage;
    public GameObject BulletGIF;
    public Image blackImage;

    [SerializeField] private Image bloodImage;
    [SerializeField] private float alpha = 0;

    public Player player;

    public void ShowDamage(int mulitplier)
    {
        alpha = .1f * mulitplier;
    }

    private void Start()
    {
        interactText.enabled = false;
    }

    private void Update()
    {
        bloodImage.color = new Color(1, 1, 1, alpha);
        if (alpha > 0)
        {
            alpha -= .05f * Time.deltaTime;

            if (alpha < 0)
            {
                alpha = 0;
            }
        }
    }   
}
