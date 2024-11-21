using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using static PlayerMovement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private string levelScene; // ����� ��� LevelDoor
    [SerializeField] private string[] randomScenes; // ������ ���� ��� LevelDoorRandom

    private Coroutine loadSceneCoroutine; // ������ ������ �� �������� �������� �����
    private bool isPlayerInTrigger = false; // ����, �����������, ��������� �� ����� � ��������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true; // ������������� ����
            if (gameObject.CompareTag("LevelDoor"))
            {
                loadSceneCoroutine = StartCoroutine(LoadSceneOnKeyPress(levelScene));
            }
            else if (gameObject.CompareTag("LevelDoorRandom"))
            {
                // ��������� ������� ������ ����� ��������� ��������� �����
                PlayerPosition.Position = collision.transform.position;
                loadSceneCoroutine = StartCoroutine(LoadRandomSceneOnKeyPress());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false; // ���������� ����
            if (loadSceneCoroutine != null)
            {
                StopCoroutine(loadSceneCoroutine);
                loadSceneCoroutine = null; // ���������� ������ �� ��������
            }

            // ���������� ������� ������ �� ���������
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                PlayerPosition.Position = playerMovement.initialPosition; // ������������� ������� � ��������� ��������
            }
        }
    }

    private IEnumerator LoadSceneOnKeyPress(string sceneName)
    {
        while (isPlayerInTrigger && !Input.GetKeyDown(KeyCode.W))
        {
            yield return null; // ���� ���������� �����
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
            yield return null; // ���� ���������� �����
        }
        if (isPlayerInTrigger)
        {
            int randomIndex = Random.Range(0, randomScenes.Length);
            string randomScene = randomScenes[randomIndex];
            SceneManager.LoadScene(randomScene);
        }
    }
}
