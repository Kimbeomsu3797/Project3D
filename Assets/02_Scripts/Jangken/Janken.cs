using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janken : MonoBehaviour
{
    bool flgJanken = false; //���������� ��� �÷���
    
    int modeJanken = 0;
    //���������� ���õ� ����Ƽ¯ ���̽�
    public AudioClip voice_janken_start;
    public AudioClip voice_janken_pon;
    public AudioClip voice_janken_goo;
    public AudioClip voice_janken_choki;
    public AudioClip voice_janken_par;
    public AudioClip voice_janken_win;
    public AudioClip voice_janken_loose;
    public AudioClip voice_janken_draw;

    //��� ����
    //const : �ʱ�ȭ ���� ���� ���� �� �� ����.
    //������ ���� �ʱ�ȭ �� �� �ִ�.

    //���������� ���¸� ��Ÿ���� ���
    const int JANKEN = 0;
    const int GOO = 1;
    const int CHOKI = 2;
    const int PAR = 3;
    // ���������� ����� ��Ÿ���� ���
    const int DRAW = 4;
    const int WIN = 5;
    const int LOOSE = 6;

    int myHand;
    int unityHand;

    int flgResult;

    float waitTime;

    private Animator animator;
    private AudioSource univoice;

    //��ư�� on/off�� �� �� �ְ� �� ���� ����
    public GameObject bt;
    public GameObject btG;
    // Start is called before the first frame update
    void Start()
    {
        //������ �������� �� ����, ����, �� ��ư off
        btG.SetActive(false);
        animator = GetComponent<Animator>();
        univoice = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(flgJanken == true)
        {
            switch (modeJanken)
            {
                case 0: // ���������� ����
                    UnityChanAction(JANKEN);
                    modeJanken++;
                    break;
                case 1: // �÷��̾��� �Է��� ��ٸ���.
                    //�ִϸ��̼� �ʱ�ȭ
                    animator.SetBool("Janken", false);
                    //Draw ����(�Ķ����)
                    animator.SetBool("Aiko", false);
                    animator.SetBool("Goo", false);
                    animator.SetBool("Choki", false);
                    animator.SetBool("Par", false);
                    animator.SetBool("Win", false);
                    animator.SetBool("Loose", false);
                    break;
                case 2: // ����
                    flgResult = -1;
                    //����Ƽ¯�� ������ ���� �������� ���Ѵ�.
                    //Range ��(1, 4)�̸�(int) 1,2,3�� ��ȯ�ȴ�.
                    unityHand = Random.Range(GOO, PAR + 1);
                    // ����Ƽ¯�� ����
                    UnityChanAction(unityHand);

                    // ���� ��Ȳ
                    if (myHand == unityHand)
                    {
                        flgResult = DRAW;
                    }//������ ��Ȳ      
                    else
                    {
                        switch (unityHand)
                        {
                            case GOO:
                                if (myHand == PAR)
                                {
                                    flgResult = LOOSE;
                                }
                                break;

                            case CHOKI:
                                if (myHand == GOO)
                                {
                                    flgResult = LOOSE;
                                }
                                break;

                            case PAR:
                                if (myHand == CHOKI)
                                {
                                    flgResult = LOOSE;
                                }
                                break;
                        }
                        // ���ų� ���� ��Ȳ�� �ƴϸ� �̱�� ��
                        if (flgResult != LOOSE)
                        {
                            flgResult = WIN;
                        }
                    }

                    modeJanken++;
                    break;
                case 3: //���
                    waitTime += Time.deltaTime;
                    if(waitTime > 1.5F)
                    {
                        UnityChanAction(flgResult);

                        waitTime = 0;
                        modeJanken++;
                    }
                    break;
                case 4:
                    flgJanken = false;
                    modeJanken = 0;

                    btG.SetActive(false);
                    bt.SetActive(true);
                    break;
            }
        }
    }
    // ����Ƽ¯�� �׼�
    // ������ ���� action�� �Ű������� �޴� �Լ�
    void UnityChanAction(int action)
    {
        //���� ���� action���� ����ġ ���� �����.

        //JANKEN�� ���
        //�ִϸ����� �Ķ���� Janken�� �̿��� �ش� �ִϸ��̼��� �����ϵ��� �Ѵ�.
        //�ش� ������ �� �ۼ��Ǿ������� �Ķ���Ϳ� ���޸��ϸ�ȴ�.
        //����� �ҽ� Ŭ���� voice_janken_start �� ���Խ�Ų��.

        //GOO�� ��� �ִϸ����� �Ķ���� GOO�� �̿��� �ش� �ִϸ��̼��� �����ϵ����Ѵ�.
        //�ش� ������ �� �ۼ��Ǿ������� �Ķ���Ϳ� ���޸��ϸ�ȴ�.
        //����� �ҽ� Ŭ���� voice_janken_goo �� ���Խ�Ų��.

        //CHOKI�� ��� �ִϸ����� �Ķ���� Choki�� �̿��� �ش� �ִϸ��̼��� �����ϵ����Ѵ�.
        //�ش� ������ �� �ۼ��Ǿ������� �Ķ���Ϳ� ���޸��ϸ�ȴ�.
        //����� �ҽ� Ŭ���� voice_janken_choki �� ���Խ�Ų��.

        //PAR�� ��� �ִϸ����� �Ķ���� Par�� �̿��� �ش� �ִϸ��̼��� �����ϵ����Ѵ�.
        //�ش� ������ �� �ۼ��Ǿ������� �Ķ���Ϳ� ���޸��ϸ�ȴ�.
        //����� �ҽ� Ŭ���� voice_janken_Par �� ���Խ�Ų��.
        switch (action)
        {
            case JANKEN:
                animator.SetBool("Janken", true);
                univoice.clip = voice_janken_start;
                break;

            case GOO:
                animator.SetBool("Goo", true);
                univoice.clip = voice_janken_goo;
                break;

            case CHOKI:
                animator.SetBool("Choki", true);
                univoice.clip = voice_janken_choki;
                break;

            case PAR:
                animator.SetBool("Par", true);
                univoice.clip = voice_janken_par;
                break;

            case DRAW:
                animator.SetBool("Aiko", true);
                univoice.clip = voice_janken_draw;
                break;

            case WIN:
                animator.SetBool("Win", true);
                univoice.clip = voice_janken_win;
                break;

            case LOOSE:
                animator.SetBool("Loose", true);
                univoice.clip = voice_janken_loose;
                break;
        }
        univoice.Play();
    }
    // ���������� ���� ��ư�� Ŭ�� �� ���� ��ư off, ����, ����, �� ��ư on
    public void JankenStart()
    {
        flgJanken = true;
        bt.SetActive(false);
        btG.SetActive(true);
    }
    
    //����
    public void Goo()
    {
        //���� modeJanken�� 1���
        //myHand �� GOO�� ����
        //modeJanken �� �ϳ� ���� ����
        //���� ��尡 �� �� �ֵ��� �Ѵ�
        if(modeJanken == 1)
        {
            myHand = GOO;
            modeJanken++;
        }
    }
   
    //����
    public void Choki()
    {
        //���� modeJanken�� 1���
        //myHand �� CHOKI�� ����
        //modeJanken �� �ϳ� ���� ����
        //���� ��尡 �� �� �ֵ��� �Ѵ�
        if (modeJanken == 1)
        {
            myHand = CHOKI;
            modeJanken++;
        }
    }
   
    //��
    public void Par()
    {
        //���� modeJanken�� 1���
        //myHand �� PAR�� ����
        //modeJanken �� �ϳ� ���� ����
        //���� ��尡 �� �� �ֵ��� �Ѵ�
        if (modeJanken == 1)
        {
            myHand = PAR;
            modeJanken++;
        }
    }
    public void CheckGame()
    {
        //���ڵ�� ����Ƽ���ڵ尡 ���ٸ鹫�º�
        //���ڵ�� ����Ƽ�� �ڵ尡 ���� �ʴٸ�
        //���� GOO�϶� ����Ƽ�� par��� ����Ƽ��win
        //���� GOO�϶� ����Ƽ�� Choki��� ����Ƽ�� loose
        //���� Choki�϶� ����Ƽ�� Goo��� ����Ƽ�� win
        //���� Choki�϶� ����Ƽ�� Par��� ����Ƽ�� loose
        //���� par�϶� ����Ƽ�� Choki��� ����Ƽ�� win
        //���� par�϶� ����Ƽ�� Goo��� ����Ƽ�� loose
        
        if(myHand == unityHand)
        {
            flgResult = DRAW;
        }
        else
        {
            switch (unityHand)
            {
                case GOO:
                    if(myHand == PAR)
                    {
                        flgResult = LOOSE;
                    }
                    break;
                case CHOKI:
                    if(myHand == GOO)
                    {
                        flgResult = LOOSE;
                    }
                    break;
                case PAR:
                    if(myHand == CHOKI)
                    {
                        flgResult = LOOSE;
                    }
                    break;
            }
            if(flgResult != LOOSE)
            {
                flgResult = WIN;
            }
            
            
        }
        
    }
}
