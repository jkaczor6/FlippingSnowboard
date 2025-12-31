using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float baseSpeed = 20f;
    [SerializeField] float boostSpeed = 25f;
    [SerializeField] ParticleSystem powerupParticles;
    [SerializeField] ScoreManager scoreManager;
    InputAction moveAction;
    Rigidbody2D rb2d;
    SurfaceEffector2D se2d;
    Vector2 moveVector;
    bool canControlPlayer = true;
    float previousRotation = 0f;
    float totalRotation = 0f;
    int powerupCount = 0;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb2d = GetComponent<Rigidbody2D>();
        se2d = FindAnyObjectByType<SurfaceEffector2D>();
    }

    void Update()
    {
        if (canControlPlayer)
        {
            RotatePlayer();
            BoostPlayer();
            CalculateFlips();
        }
    }

    void RotatePlayer()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        if (moveVector.x < 0)
        {
            rb2d.AddTorque(torqueAmount);
        }
        else if (moveVector.x > 0)
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }

    void BoostPlayer()
    {
        if (moveVector.y > 0)
        {
            se2d.speed = boostSpeed;
        }
        else
        {
            se2d.speed = baseSpeed;
        }
    }

    public void DisableControls()
    {
        canControlPlayer = false;
    }

    void CalculateFlips()
    {
        float currentRotation = transform.rotation.eulerAngles.z;

        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation);

        if(totalRotation > 350f || totalRotation < -350f)
        {
            totalRotation = 0f;
            scoreManager.AddScore(100);
        }

        previousRotation = currentRotation;
    }

    public void ActivatePowerup(PowerUpSO powerUp)
    {
        powerupParticles.Play();
        powerupCount++;
        if(powerUp.getPowerupType() == "speed")
        {
            baseSpeed += powerUp.getValueChange();
            boostSpeed += powerUp.getValueChange();
        } else if(powerUp.getPowerupType() == "torque")
        {
            torqueAmount += powerUp.getValueChange();
        }
    }
    public void DeactivatePowerup(PowerUpSO powerUp)
    {
        powerupCount--;
        if(powerupCount == 0)
        {
            powerupParticles.Stop();
        }
        if(powerUp.getPowerupType() == "speed")
        {
            baseSpeed -= powerUp.getValueChange();
            boostSpeed -= powerUp.getValueChange();
        } else if(powerUp.getPowerupType() == "torque")
        {
            torqueAmount -= powerUp.getValueChange();
        }
    }
}
