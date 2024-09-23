using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room camera
    private float currentPosX;
    private float currentPosY;
    private Vector3 velocity = Vector3.zero;

    //Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;
    [SerializeField] private float verticalOffset; // Добавлено смещение по вертикали

    private void Update()
    {
        //Room camera
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, currentPosY, transform.position.z), ref velocity, speed);

        //Follow player
        float targetX = player.position.x + lookAhead;
        float targetY = player.position.y + verticalOffset; // Новая целевая позиция Y

        // Плавное следование за игроком по обеим осям
        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraSpeed);

        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
        currentPosY = _newRoom.position.y; // Обновление позиции Y для новой комнаты
    }
}
