using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float sensitivity = 5f; // Valor predeterminado
    public float sfxVolume = 0.1f; // Valor predeterminado
    public float musicVolume = 0.1f; // Valor predeterminado

    private static PlayerData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
