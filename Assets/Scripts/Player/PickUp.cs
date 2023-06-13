using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUp : MonoBehaviour
{
    [SerializeField] private GameObject handPoint;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private SoundManager sm;
    [SerializeField] private GunSystem gunSystem;

    private GameObject pickedObject = null;


    private void Start()
    {
        ammoText.enabled = false;
    }
    private void Update()
    {
        if (pickedObject != null && Input.GetKey(KeyCode.G))
        {
            pickedObject.gameObject.transform.SetParent(null);        
            gunSystem.readyToShoot = false;
            ammoText.enabled = false;
            pickedObject.GetComponent<CapsuleCollider>().isTrigger = false;
            pickedObject.GetComponent<Rigidbody>().useGravity = true;
            pickedObject.GetComponent<Rigidbody>().isKinematic = false;
            gunSystem = null;
            pickedObject = null;
        } 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            if (Input.GetKey(KeyCode.E) && pickedObject == null)
            {
                gunSystem = other.GetComponent<GunSystem>();
                ammoText.enabled = true;
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;
                other.isTrigger = true;
                other.transform.position = handPoint.transform.position;
                other.transform.rotation = handPoint.transform.rotation;
                other.gameObject.transform.SetParent(handPoint.gameObject.transform);
                pickedObject = other.gameObject;
                gunSystem.readyToShoot = true;
                sm.PickedWeaponSound();
            }
        }
    }


}

