using UnityEngine;

public class PickAndDrop: MonoBehaviour
{
    public Transform gunPos;
    public Camera playerCam;
    public Camera fixCamera;
    public float range;
    public GameObject currentWeapon;
    public GameObject weapon;
    public PlayerUI ui;
    public GunSystem gunSystem;
    public MeleeSystem meleeSystem;

    public Rigidbody weaponRigidbody;

    public float forceMagnitude;

    bool canGrab;

    private void Start()
    {
        gunSystem = FindObjectOfType<GunSystem>();
        meleeSystem = FindObjectOfType<MeleeSystem>();
    }
    private void Update()
    {
        CheckWeapons();

        if (canGrab)
        {

            if (Input.GetKeyDown(KeyCode.E) && gunSystem.pickedUp == false)
            {
                if (currentWeapon != null)
                {           
                    Drop();
                    fixCamera.gameObject.SetActive(false);
                }

                Pickup();
                fixCamera.gameObject.SetActive(true);            
            }
        }

        if (currentWeapon != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Drop();
                fixCamera.gameObject.SetActive(false);
            }               
        }
    }

    private void CheckWeapons()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            if (hit.transform.tag == "Weapon")
            {
                ui.interactText.enabled = true;
                canGrab = true;
                weapon = hit.transform.gameObject;
            }
        }
        else
        {
            canGrab = false;
            ui.interactText.enabled = false;
        }       
    }

    private void Pickup()
    {
        if (gunSystem.pickedUp == true)
        {
            return;
        }

        currentWeapon = weapon;
        currentWeapon.transform.parent = gunPos;
        currentWeapon.transform.position = gunPos.position;
        currentWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

        ui.textoContBalas.gameObject.SetActive(true);
        ui.BulletGIF.gameObject.SetActive(true);
        weaponRigidbody = currentWeapon.GetComponent<Rigidbody>();
        
        currentWeapon.GetComponent<Rigidbody>().useGravity = false;
        currentWeapon.GetComponent<Animator>().enabled = true;
        currentWeapon.GetComponent<Rigidbody>().freezeRotation = true;
        currentWeapon.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        currentWeapon.GetComponentInChildren<BoxCollider>().isTrigger = true;

        if (currentWeapon != null)
        {
            GunSystem gunSystem = currentWeapon.GetComponent<GunSystem>();
            MeleeSystem meleeSystem = currentWeapon.GetComponent<MeleeSystem>();

            if (gunSystem != null)
            {
                gunSystem.readyToShoot = true;
                gunSystem.enabled = true;
                gunSystem.pickedUp = true;
            }

            if (meleeSystem != null)
            {
                meleeSystem.readyToAttack = true;
                meleeSystem.enabled = true;
                meleeSystem.pickedUp = true;
            }
        }
    }

    private void Drop()
    {
        if (currentWeapon != null)
        {
            GunSystem gunSystem = currentWeapon.GetComponent<GunSystem>();
            MeleeSystem meleeSystem = currentWeapon.GetComponent<MeleeSystem>();

            if (gunSystem != null)
            {
                gunSystem.readyToShoot = false;
                gunSystem.pickedUp = false;
                gunSystem.enabled = false;
            }

            if (meleeSystem != null)
            {
                meleeSystem.readyToAttack = false;
                meleeSystem.pickedUp = false;
                meleeSystem.enabled = false;
            }
        }

        currentWeapon.gameObject.transform.parent = null;
        ui.textoContBalas.gameObject.SetActive(false);
        ui.BulletGIF.gameObject.SetActive(false);

        currentWeapon.GetComponent<Rigidbody>().freezeRotation = false;
        weaponRigidbody = currentWeapon.GetComponent<Rigidbody>();
        weaponRigidbody.useGravity = true;
        weaponRigidbody.AddForce(currentWeapon.transform.forward * forceMagnitude, ForceMode.Impulse);

        currentWeapon.GetComponent<Animator>().enabled = false;
        currentWeapon.GetComponentInChildren<BoxCollider>().isTrigger = false;
        currentWeapon = null;
        fixCamera.gameObject.SetActive(false);
    }
}
