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
        //방향설정
        moveDirection = new Vector3(h, 0, v);
        //각도계산
        float rad = Mathf.Atan2(h, v) * Mathf.Rad2Deg;
        //회전
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(0, rad, 0);
        }
        //이동
        characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        */

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // 두 점간의 거리에 제곱의 루트를 한 값
        // 두 점간의 거리의 차이를 2차원 함수값으로 계산
        // Vector3.Distance가 sqrMagnitude보다 연산속도가 느려서 단순하게 두 점간의 거리를 구할 땐 sqrMagnitude가 낫다
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
