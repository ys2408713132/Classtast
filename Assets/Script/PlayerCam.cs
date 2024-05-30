using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("�R�^�D�����ж�")]
    public float sensX;   // �R�^X�S�D�����ж�
    public float sensY;   // �R�^Y�S�D�����ж�

    float xRotation;
    float yRotaiton;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // �i�������Θ��ڮ�������
        Cursor.visible = false;                     // �[�ػ����Θ�
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;   // ȡ�û����Θ˵�X�S�Ƅ�
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;   // ȡ�û����Θ˵�Y�S�Ƅ�

        // ����A�O��XY�S�Ƅӷ�����UNITY�Ƿ��D�ģ��҂�Ҫ������X�S�D�������Y�S��Y�S�D��X�S
        xRotation -= mouseY; // ������Y�S�ƄӔ�ֵ"���D"�^��(��׃ؓؓ׃��)
        yRotaiton += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 30f); // �޶�X�S�D������30�ȵ�ؓ90���g(̧�^�͵��^�����ƽǶ�)

        transform.rotation = Quaternion.Euler(xRotation, yRotaiton, 0); // �O���zӰ�C�Ƕ�
    }
}
