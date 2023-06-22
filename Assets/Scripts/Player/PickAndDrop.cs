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

    public float forceMagnitude;

    bool canGrab;

    private void Update()
    {
        CheckWeapons();

        if (canGrab)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentWeapon != null)
                {
                    currentWeapon.GetComponent<GunSystem>().readyToShoot = false;               
                    Drop();
                    fixCamera.gameObject.SetActive(false);
                }

                Pickup();
                fixCamera.gameObject.SetActive(true);
                currentWeapon.GetComponent<GunSystem>().readyToShoot = true;
            }
        }


        if (currentWeapon != null)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                currentWeapon.GetComponent<GunSystem>().readyToShoot = false;
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
        currentWeapon = weapon;
        currentWeapon.transform.parent = gunPos;
        currentWeapon.transform.position = gunPos.position;
        currentWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        ui.textoContBalas.gameObject.SetActive(true);
        ui.BulletGIF.gameObject.SetActive(true);
        currentWeapon.GetComponent<Rigidbody>().useGravity = false;
        currentWeapon.GetComponent<Animator>().enabled = true;
        currentWeapon.GetComponentInChildren<BoxCollider>().isTrigger = true;
        currentWeapon.gameObject.GetComponent<GunSystem>().enabled = true;
        currentWeapon.gameObject.GetComponent<GunSystem>().pickedUp = true;
    }

    private void Drop()
    {
        currentWeapon.gameObject.transform.parent = null;
        ui.textoContBalas.gameObject.SetActive(false);
        ui.BulletGIF.gameObject.SetActive(false);

        Rigidbody weaponRigidbody = currentWeapon.GetComponent<Rigidbody>();
        weaponRigidbody.useGravity = true;
        weaponRigidbody.AddForce(currentWeapon.transform.forward * forceMagnitude, ForceMode.Impulse);

        currentWeapon.GetComponent<Animator>().enabled = false;
        currentWeapon.GetComponentInChildren<BoxCollider>().isTrigger = false;
        currentWeapon.gameObject.GetComponent<GunSystem>().enabled = false;
        currentWeapon.gameObject.GetComponent<GunSystem>().pickedUp = false;
        currentWeapon = null;
        fixCamera.gameObject.SetActive(false);
    }

}
