using UnityEngine;
using static PlayerMovement;

public class ReturnToPlayerPosition : MonoBehaviour
{
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Перемещаем игрока на сохраненную позицию
            player.transform.position = PlayerPosition.Position;
        }
    }
}
