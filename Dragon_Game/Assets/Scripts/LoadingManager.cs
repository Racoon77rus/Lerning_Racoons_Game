using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using static PlayerMovement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private string levelScene; // Сцена для LevelDoor
    [SerializeField] private string[] randomScenes; // Массив сцен для LevelDoorRandom

    private Coroutine loadSceneCoroutine; // Хранит ссылку на корутину загрузки сцены
    private bool isPlayerInTrigger = false; // Флаг, указывающий, находится ли игрок в триггере

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true; // Устанавливаем флаг
            if (gameObject.CompareTag("LevelDoor"))
            {
                loadSceneCoroutine = StartCoroutine(LoadSceneOnKeyPress(levelScene));
            }
            else if (gameObject.CompareTag("LevelDoorRandom"))
            {
                // Сохраняем позицию игрока перед загрузкой случайной сцены
                PlayerPosition.Position = collision.transform.position;
                loadSceneCoroutine = StartCoroutine(LoadRandomSceneOnKeyPress());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false; // Сбрасываем флаг
            if (loadSceneCoroutine != null)
            {
                StopCoroutine(loadSceneCoroutine);
                loadSceneCoroutine = null; // Сбрасываем ссылку на корутину
            }

            // Сбрасываем позицию игрока на начальную
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                PlayerPosition.Position = playerMovement.initialPosition; // Устанавливаем позицию в начальное значение
            }
        }
    }

    private IEnumerator LoadSceneOnKeyPress(string sceneName)
    {
        while (isPlayerInTrigger && !Input.GetKeyDown(KeyCode.W))
        {
            yield return null; // Ждем следующего кадра
        }
        if (isPlayerInTrigger)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private IEnumerator LoadRandomSceneOnKeyPress()
    {
        while (isPlayerInTrigger && !Input.GetKeyDown(KeyCode.W))
        {
            yield return null; // Ждем следующего кадра
        }
        if (isPlayerInTrigger)
        {
            int randomIndex = Random.Range(0, randomScenes.Length);
            string randomScene = randomScenes[randomIndex];
            SceneManager.LoadScene(randomScene);
        }
    }
}
