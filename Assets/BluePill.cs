using UnityEngine;

public class BluePill : MonoBehaviour
{
    public GunSystem gs;
    public PickAndDrop gun;
    public GameObject bluePill;
    public Enemy enemy;

    bool taken = false;

    private void Start()
    {
        gs = FindObjectOfType<GunSystem>();
        enemy = FindObjectOfType<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!taken)
        {
            TakeBluePill();
            bluePill.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(bluePill, 6f);
            print("taken");
        }
        taken = true;
    }

    private void TakeBluePill()
    {
        gs.timeBetweenShooting /= 3;
        gs.duration /= 3;
        gs.bulletsLeft = gs.magazineSize;
        Invoke("LeaveBluePill", 5f);
    }

    public void LeaveBluePill()
    {
        gs.timeBetweenShooting *= 3;
        gs.duration *= 3;
        print("leave");
    }
}

