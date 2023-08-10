using UnityEngine;
using System.Threading.Tasks;

public class Pan : MeleeSystem
{
    public override void MyInput()
    {
        base.MyInput();

        if (attacking)
        {
            PerformAttack();
            anim.SetTrigger("AttackAnim");
        }
    }

    private async void OnEnable()
    {
        //Sonido de pick up
        await Task.Delay(1500);
        pickedUp = false;
    }
}

