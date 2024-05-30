using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Ҫ���@�����ܿ������ֿ�

public class Shooting : MonoBehaviour
{
    [Header("�������")]
    public Camera PlayerCamera;
    public Transform attackPoint;

    [Header("�ӏ��A�����")]
    public GameObject bullet;

    [Header("��֦�O��")]
    public int magazineSize;        // �O�����A���ԷŶ����w�ӏ���
    public int bulletsLeft;         // �ӏ�߀�ж����w��(����]��Ҫ�yԇ��������O���� Private)
    public float reloadTime;        // �O���Q���A����Ҫ�ĕr�g
    public float recoilForce;       // ��������

    bool reloading;             // ����׃���������ǲ������ړQ���A�Ġ�B��True�����ړQ���A��False���Q���A�Ą����ѽY��

    [Header("UI���")]
    public TextMeshProUGUI ammunitionDisplay; // �����@ʾ
    public TextMeshProUGUI reloadingDisplay;  // �@ʾ�ǲ������ړQ���A��

    public Animator animatorObject;  // �Ӯ��������M��

    private void Start()
    {
        bulletsLeft = magazineSize;        // �[��һ�_ʼ���A�O����ȫ�M��B
        reloadingDisplay.enabled = false;  // ���@ʾ���ړQ���A����Ļ�[������

        ShowAmmoDisplay();                 // �������@ʾ
    }

    private void Update()
    {
        MyInput();
    }

    // �������ɜy��Ҳ�����B
    private void MyInput()
    {
        // �Дࣺ�Л]�а������I��
        if (Input.GetMouseButtonDown(0) == true)
        {
            // ���߀���ӏ����K�қ]���������b�ӏ����Ϳ������
            if (bulletsLeft > 0 && !reloading)
            {
                Shoot();
            }
        }

        // �Дࣺ1.�а���R�I��2.�ӏ�������춏��A�ȵď�����3.���ǓQ���A�Ġ�B�������l�����M�㣬�Ϳ��ԓQ���A
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            Reload();
    }

    // �������������
    private void Shoot()
    {
        Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));  // �ĔzӰ�C���һ�l�侀
        RaycastHit hit;  // ����һ������c
        Vector3 targetPoint;  // ����һ��λ���c׃�������r������д򵽖|�����ʹ浽�@��׃��

        // ����侀�д򵽾߂���ײ�w�����
        if (Physics.Raycast(ray, out hit) == true)
            targetPoint = hit.point;         // ���������λ���c���M targetPoint
        else
            targetPoint = ray.GetPoint(75);  // ����]�д�����������L��75��ĩ���cȡ��һ���c�����M targetPoint

        Debug.DrawRay(ray.origin, targetPoint - ray.origin, Color.red, 10); // �����@�l�侀

        Vector3 shootingDirection = targetPoint - attackPoint.position; // �����c�c�K�c֮�g���cλ�ã�Ӌ����侀�ķ���
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); // �ڹ����c����a��һ���ӏ�
        currentBullet.transform.forward = shootingDirection.normalized; // ���ӏ��w�з����c�侀����һ��

        currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * 20, ForceMode.Impulse); // �����w�з��������ӏ�
        //currentBullet.GetComponent<Rigidbody>().AddForce(PlayerCamera.transform.up * , ForceMode.Impulse);

        bulletsLeft--;    // �����A�е��ӏ��pһ�����µČ�������һ�ӵ���˼
                          //bulletsLeft -= 1;               
                          //bulletsLeft = bulletsLeft - 1;  // ���^���µČ���

        // ������ģ�M
        this.GetComponent<Rigidbody>().AddForce(-shootingDirection.normalized * recoilForce, ForceMode.Impulse);

        ShowAmmoDisplay();                 // �������@ʾ

        animatorObject.SetTrigger("Fire");  // �|�l��Fire�����|�l׃��
    }

    // �������Q���A�����t�r�g�O��
    private void Reload()
    {
        reloading = true;                      // ���Ȍ��Q���A��B�O���飺���ړQ���A
        reloadingDisplay.enabled = true;       // �����ړQ���A����Ļ�@ʾ����
        Invoke("ReloadFinished", reloadTime);  // ����reloadTime���O���ēQ���A�r�g�������r�g��0�r����ReloadFinished����
    }

    // �������Q���A
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;            // ���ӏ���M
        reloading = false;                     // ���Q���A��B�O���飺���Q���A�Y��
        reloadingDisplay.enabled = false;      // �����ړQ���A����Ļ�[�أ��Y���Q���A�Ą���
        ShowAmmoDisplay();
    }

    // �������������@ʾ
    private void ShowAmmoDisplay()
    {
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText($"Ammo {bulletsLeft} / {magazineSize}");
    }
}
