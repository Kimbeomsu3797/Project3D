using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nav : MonoBehaviour
{
    NavMeshAgent Navi;  // 네이게이션을 사용하기 위한 네비게이션 변수
    void Start()
    {
        Navi = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        // 화면 터치 시 이동
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
                Navi.SetDestination(hit.point); // 매개변수의 위치까지 이동
            }
        }

        // 네비게이션의 목표지점과 현재 캐릭터의 위치가 같으면 함수를 빠져나감
        if (transform.position == Navi.destination) return;

        // 네비게이션 목표지점과 현재 캐릭터의 위치 사이의 거리를 구한다
        float dist = Vector3.Distance(Navi.destination, transform.position);

        if (dist < 0.1)
        {
            transform.position = Navi.destination;
        }
    }
}
