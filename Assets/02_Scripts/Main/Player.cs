using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 360f;

    CharacterController characterController;
    Vector3 moveDirection;

    Animator anim;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    
    void Update()
    {
        /*
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //���⼳��
        moveDirection = new Vector3(h, 0, v);
        //�������
        float rad = Mathf.Atan2(h, v) * Mathf.Rad2Deg;
        //ȸ��
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(0, rad, 0);
        }
        //�̵�
        characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        */

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // �� ������ �Ÿ��� ������ ��Ʈ�� �� ��
        // �� ������ �Ÿ��� ���̸� 2���� �Լ������� ���
        // Vector3.Distance�� sqrMagnitude���� ����ӵ��� ������ �ܼ��ϰ� �� ������ �Ÿ��� ���� �� sqrMagnitude�� ����
        if (direction.sqrMagnitude > 0.01f)
        {
            Vector3 forward = Vector3.Slerp(transform.forward, direction, rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));

            transform.LookAt(transform.position + forward);
        }
        characterController.Move(direction * moveSpeed * Time.deltaTime);
        anim.SetFloat("Speed", characterController.velocity.magnitude);

        if (GameObject.FindGameObjectsWithTag("Dot").Length == 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dot"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
