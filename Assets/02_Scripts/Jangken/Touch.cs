using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    public AudioClip voice_01;
    public AudioClip voice_02;

    Animator anim;
    AudioSource univoice;

    GameObject hitObject;

    void Start()
    {
        anim = GetComponent<Animator>();
        univoice = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        anim.SetBool("Touch", false);
        anim.SetBool("TouchHead", false);

        Ray ray;
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                hitObject = hit.collider.gameObject;

                if (hitObject.gameObject.CompareTag("Head"))
                {
                    anim.SetBool("TouchHead", true);
                    univoice.clip = voice_01;
                    univoice.Play();
                }
                if (hitObject.gameObject.CompareTag("Breast"))
                {
                    anim.SetBool("Touch", true);
                    univoice.clip = voice_02;
                    univoice.Play();
                }
            }
        }
    }
}
