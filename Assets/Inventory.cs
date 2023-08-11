using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform handPoint; // Punto en la mano donde se colocan las armas
    public GameObject pan;
    public GameObject stapler;
    public GameObject rifle;
    public GameObject shotgun;
    public GameObject awp;

    private GameObject[] weapons; // Array de armas
    private int currentWeaponIndex = -1; // Índice de la arma actual
    public PlayerUI ui;

    void Start()
    {
        weapons = new GameObject[] { pan, stapler, rifle, shotgun, awp };
        SwitchWeapon(0); // Comienza con la primera arma activa (pan)
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0); // Pan
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1); // Stapler
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(2); // Rifle
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchWeapon(3); // Shotgun
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchWeapon(4); // AWP
        }
    }

    void SwitchWeapon(int newIndex)
    {
        if (newIndex == currentWeaponIndex)
            return;

        // Desactivar arma actual
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Length)
        {
            DeactivateWeapon(currentWeaponIndex);
        }

        // Activar nueva arma
        if (newIndex >= 0 && newIndex < weapons.Length)
        {
            ActivateWeapon(newIndex);

            // Agregar líneas para activar la municion de cada arma
            ui.textoContBalas.gameObject.SetActive(true);
            ui.BulletGIF.gameObject.SetActive(true);

            // Acceder al script GunSystem y activar ReadyToShoot
            GunSystem gunSystem = weapons[newIndex].GetComponent<GunSystem>();
            if (gunSystem != null)
            {
                gunSystem.readyToShoot = true;
            }
        }

        currentWeaponIndex = newIndex;
    }

    void DeactivateWeapon(int index)
    {
        GameObject weapon = weapons[index];
        weapon.SetActive(false);
    }

    void ActivateWeapon(int index)
    {
        GameObject weapon = weapons[index];
        weapon.SetActive(true);
        weapon.transform.SetParent(handPoint, false);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }
}

