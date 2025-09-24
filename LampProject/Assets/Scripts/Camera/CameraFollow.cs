using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField] private PlayerController playerController;
    [SerializeField]private float fadeSpeed = 5f; //Light fade speed
    [SerializeField] private Light2D IdleLamp; //Lights when stopped
    [SerializeField] private Light2D MovingLamp; //Lights when moving
    [SerializeField] private float IdleDelay = 0.4f; //Time it takes to turn on IdleLamp
    [SerializeField] private float MovingDelay = 0.3f; //Time it takes to turn off MovingLamp

    private float IdleTimer = 0f;
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + new Vector3(0, 1, -5);
        UpdateLighting();
    }
    void UpdateLighting()
    {
        bool _moving = playerController.moving;
        if (_moving)
        {
            IdleTimer = 0f;
        }
        else
        {
            IdleTimer += Time.deltaTime;
        }
          
        float targetIdle = (IdleTimer >= IdleDelay) ? 1f: 0f;
        float targetMoving = (IdleTimer >= MovingDelay) ? 0f : 1f;

        IdleLamp.intensity = Mathf.MoveTowards(IdleLamp.intensity, targetIdle, fadeSpeed * Time.deltaTime);
        MovingLamp.intensity = Mathf.MoveTowards(MovingLamp.intensity, targetMoving, fadeSpeed * Time.deltaTime);
    }

    
}
