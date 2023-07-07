using UnityEngine;
using System.Threading.Tasks;

public class Pan : MeleeSystem
{

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            var enemy = other.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    private async void OnEnable()
    {
        //Sonido de pick up
        await Task.Delay(1500);
        pickedUp = false;
    }
}
