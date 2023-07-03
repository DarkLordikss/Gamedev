using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    private Vector3 initialPosition;
    public Vector3 finalPosition;

    private bool isMoving = false;

    public Image screenOverlay;
    public PlayerControl playerControl;

    private void Start()
    {
        initialPosition = transform.position;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (finalPosition - transform.position).normalized;
        float distance = speed * Time.deltaTime;

        if (distance >= Vector3.Distance(transform.position, finalPosition))
        {
            // ���� ������ ������ ����, ��������������� ��� �� ��������� �������
            transform.position = initialPosition;
        }
        else
        {
            // ���� ������ ��� �� ������ ����, ���������� ��������
            transform.position += direction * distance;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerControl.isDead = true;

            Time.timeScale = 0.1f; // ��������� ����� � 10 ���
            StartCoroutine(RestartScene());
        }
    }

    private System.Collections.IEnumerator RestartScene()
    {
        // ��������� �����
        float duration = 0.5f; // ������������ �������� ����������
        float targetAlpha = 1f; // ������� ������������ (��������� ������������)

        Color initialColor = screenOverlay.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, targetAlpha);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            screenOverlay.color = Color.Lerp(initialColor, targetColor, t);

            yield return null;
        }

        // ������������� �����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerControl.isDead = false;
        Time.timeScale = 1f;
    }
}
