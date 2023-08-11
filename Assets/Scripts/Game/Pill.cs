using UnityEngine;

public class Pill : MonoBehaviour
{
    public GameObject pill;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            print("Player");
            var player = other.GetComponent<Player>();
            if (player)
            {
                print(player.currentHealth);
                player.currentHealth = 100;
            }
        }
    }
}