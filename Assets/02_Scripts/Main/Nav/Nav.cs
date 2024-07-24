using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nav : MonoBehaviour
{
    NavMeshAgent Navi;  // ���̰��̼��� ����ϱ� ���� �׺���̼� ����
    void Start()
    {
        Navi = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        // ȭ�� ��ġ �� �̵�
        PlayerTouchMove();
    }

    void PlayerTouchMove()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Navi.SetDestination(hit.point); // �Ű������� ��ġ���� �̵�
            }
        }

        // �׺���̼��� ��ǥ������ ���� ĳ������ ��ġ�� ������ �Լ��� ��������
        if (transform.position == Navi.destination) return;

        // �׺���̼� ��ǥ������ ���� ĳ������ ��ġ ������ �Ÿ��� ���Ѵ�
        float dist = Vector3.Distance(Navi.destination, transform.position);

        if (dist < 0.1)
        {
            transform.position = Navi.destination;
        }
    }
}
