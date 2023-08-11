using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventarioForUI : MonoBehaviour
{

    public Image[] weaponSlots;
    public Sprite weaponSelectedSprite;
    public Sprite weaponNotSelectedSprite;

    private int currentSelectedIndex = 0;

    public void UpdateWeaponSelection(int newIndex)
    {
        if (newIndex == currentSelectedIndex)
            return;

        // Desactivar el arma anterior
        weaponSlots[currentSelectedIndex].sprite = weaponNotSelectedSprite;

        // Activar el nuevo arma
        weaponSlots[newIndex].sprite = weaponSelectedSprite;

        currentSelectedIndex = newIndex;
    }
}
