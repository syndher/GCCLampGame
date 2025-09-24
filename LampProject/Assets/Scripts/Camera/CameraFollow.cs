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
        
        float targetIdle = _moving ? 0f : 1f;
        float targetMoving = _moving ? 1f : 0f;

        IdleLamp.intensity = Mathf.MoveTowards(IdleLamp.intensity, targetIdle, fadeSpeed * Time.deltaTime);
        MovingLamp.intensity = Mathf.MoveTowards(MovingLamp.intensity, targetMoving, fadeSpeed * Time.deltaTime);
    }

    
}
