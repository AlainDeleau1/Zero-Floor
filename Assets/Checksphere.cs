using UnityEngine;
using UnityEngine.AI;

public class Checksphere : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        print(other.tag);

        var enemy = other.GetComponent<NavMeshAgent>();
        print(enemy);
        if (enemy != null)
        {
            print("smt");
        }
    }
}

