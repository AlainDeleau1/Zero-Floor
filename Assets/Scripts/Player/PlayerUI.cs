using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI textoContBalas;
    public TextMeshProUGUI victoryMessage;
    public GameObject BulletGIF;

    [SerializeField] private Image bloodImage;
    [SerializeField] private float alpha = 0;

    public Player player;

    public void ShowDamage()
    {
        alpha = .2f;
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
            alpha -= .02f * Time.deltaTime;

            if (alpha < 0)
            {
                alpha = 0;
            }
        }
    }     
}
