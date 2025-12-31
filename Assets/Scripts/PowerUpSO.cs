using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUpSO")]
public class PowerUpSO : ScriptableObject
{
    [SerializeField] string powerupType;
    [SerializeField] float valueChange;
    [SerializeField] float duration;
}
