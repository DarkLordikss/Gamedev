using UnityEngine;

public class LookToCamera : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        GameObject[] objectsToLookAtCamera = GameObject.FindGameObjectsWithTag("PlayerStaring");

        foreach (GameObject obj in objectsToLookAtCamera)
        {
            // ���������� ������ �� ������
            obj.transform.LookAt(mainCamera.transform);

            // ���������� ������� �� ���� X � Z
            Vector3 eulerAngles = obj.transform.rotation.eulerAngles;
            obj.transform.rotation = Quaternion.Euler(90f, eulerAngles.y + 180f, 180f);
        }
    }
}
