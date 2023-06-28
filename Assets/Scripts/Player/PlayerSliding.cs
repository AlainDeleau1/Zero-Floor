using UnityEngine;
using UnityEngine.UI;

public class PlayerSliding : MonoBehaviour
{
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerObj;
    private PlayerMovement pm;
    [SerializeField] private float dashForce;
    [SerializeField] private float maxSlideTime;
    [SerializeField] private float slideForce;
    [SerializeField] private float slideTimer;

    [SerializeField] private float slideYScale;
    private float startYScale;

    [SerializeField] private KeyCode slideKey = KeyCode.LeftShift;
    private float horizontalInput;
    private float verticalInput;

    private float startPlayerSpeed;

    private bool sliding;

    [SerializeField] private Image dashEffectImage;
    [SerializeField] private float dashEffectMaxAlpha;
    [SerializeField] private float dashEffectFadeSpeed;

    private float currentDashEffectAlpha;

    public Camera playerCam;
    public Camera fixCamera;
    public float FOVMultiplier = 2f;
    float originalFOV;

    private FOVSlider fovSlider; // Referencia al script FOVSlider

    private void Start()
    {
        pm = GetComponent<PlayerMovement>();

        startPlayerSpeed = pm.playerSpeed;

        startYScale = playerObj.localScale.y;
        dashEffectImage.gameObject.SetActive(false);

        originalFOV = playerCam.fieldOfView;

        fovSlider = FindObjectOfType<FOVSlider>(); // Buscar el script FOVSlider en la escena
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0))
        {
            StartSlide();
        }

        if (Input.GetKeyUp(slideKey) && sliding)
        {
            StopSlide();
        }

        UpdateDashEffectAlpha();

        if (sliding)
        {
            ModifyFOV();
        }
        else
        {
            RestoreFOV();
        }
    }

    private void FixedUpdate()
    {
        if (sliding)
        {
            SlidingMovement();
        }
    }

    private void StartSlide()
    {
        sliding = true;

        pm.playerSpeed = pm.playerSpeed * dashForce;

        slideTimer = maxSlideTime;

        currentDashEffectAlpha = 0f;
        dashEffectImage.color = new Color(dashEffectImage.color.r, dashEffectImage.color.g, dashEffectImage.color.b, currentDashEffectAlpha);
        dashEffectImage.gameObject.SetActive(true);

        fovSlider.StartDash(); // Indicar al FOVSlider que se está iniciando el dasheo
    }

    private void SlidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;


        slideTimer -= Time.deltaTime;

        if (slideTimer <= 0)
        {
            StopSlide();
        }
    }

    private void StopSlide()
    {
        sliding = false;

        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);

        pm.playerSpeed = startPlayerSpeed;

        currentDashEffectAlpha = 0f;
        dashEffectImage.color = new Color(dashEffectImage.color.r, dashEffectImage.color.g, dashEffectImage.color.b, currentDashEffectAlpha);
        dashEffectImage.gameObject.SetActive(false);

        fovSlider.StopDash(); // Indicar al FOVSlider que se ha detenido el dasheo
    }

    private void UpdateDashEffectAlpha()
    {
        float targetDashEffectAlpha = Input.GetKey(slideKey) ? dashEffectMaxAlpha : 0f;
        currentDashEffectAlpha = Mathf.MoveTowards(currentDashEffectAlpha, targetDashEffectAlpha, dashEffectFadeSpeed * Time.deltaTime);

        dashEffectImage.color = new Color(dashEffectImage.color.r, dashEffectImage.color.g, dashEffectImage.color.b, currentDashEffectAlpha);
    }

    private void ModifyFOV()
    {
        float targetFOV = originalFOV * FOVMultiplier;
        playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, targetFOV, Time.deltaTime * 10f);
        fixCamera.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, targetFOV, Time.deltaTime * 10f);
    }

    private void RestoreFOV()
    {
        playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, originalFOV, Time.deltaTime * 10f);
        fixCamera.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, originalFOV, Time.deltaTime * 10f);
    }
}
