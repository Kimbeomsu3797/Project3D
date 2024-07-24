using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janken : MonoBehaviour
{
    bool flgJanken = false; //가위바위보 모드 플래그
    
    int modeJanken = 0;
    //가위바위보 관련된 유니티짱 보이스
    public AudioClip voice_janken_start;
    public AudioClip voice_janken_pon;
    public AudioClip voice_janken_goo;
    public AudioClip voice_janken_choki;
    public AudioClip voice_janken_par;
    public AudioClip voice_janken_win;
    public AudioClip voice_janken_loose;
    public AudioClip voice_janken_draw;

    //상수 선언
    //const : 초기화 이후 값을 변경 할 수 없다.
    //선언할 때만 초기화 할 수 있다.

    //가위바위보 상태를 나타내는 상수
    const int JANKEN = 0;
    const int GOO = 1;
    const int CHOKI = 2;
    const int PAR = 3;
    // 가위바위보 결과를 나타내는 상수
    const int DRAW = 4;
    const int WIN = 5;
    const int LOOSE = 6;

    int myHand;
    int unityHand;

    int flgResult;

    float waitTime;

    private Animator animator;
    private AudioSource univoice;

    //버튼을 on/off를 할 수 있게 할 변수 선언
    public GameObject bt;
    public GameObject btG;
    // Start is called before the first frame update
    void Start()
    {
        //게임을 시작했을 때 가위, 바위, 보 버튼 off
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
                case 0: // 가위바위보 시작
                    UnityChanAction(JANKEN);
                    modeJanken++;
                    break;
                case 1: // 플레이어의 입력을 기다린다.
                    //애니메이션 초기화
                    animator.SetBool("Janken", false);
                    //Draw 주의(파라미터)
                    animator.SetBool("Aiko", false);
                    animator.SetBool("Goo", false);
                    animator.SetBool("Choki", false);
                    animator.SetBool("Par", false);
                    animator.SetBool("Win", false);
                    animator.SetBool("Loose", false);
                    break;
                case 2: // 판정
                    flgResult = -1;
                    //유니티짱이 무엇을 낼지 무작위로 정한다.
                    //Range 가(1, 4)이면(int) 1,2,3이 반환된다.
                    unityHand = Random.Range(GOO, PAR + 1);
                    // 유니티짱의 동작
                    UnityChanAction(unityHand);

                    // 비기는 상황
                    if (myHand == unityHand)
                    {
                        flgResult = DRAW;
                    }//이지는 상황      
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
                        // 비기거나 지는 상황이 아니면 이기는 것
                        if (flgResult != LOOSE)
                        {
                            flgResult = WIN;
                        }
                    }

                    modeJanken++;
                    break;
                case 3: //결과
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
    // 유니티짱의 액션
    // 정수형 변수 action을 매개변수로 받는 함수
    void UnityChanAction(int action)
    {
        //비교할 변수 action으로 스위치 문을 만든다.

        //JANKEN일 경우
        //애니메이터 파라미터 Janken을 이용해 해당 애니메이션이 동작하도록 한다.
        //해당 동작은 다 작성되어있으니 파라미터에 전달만하면된다.
        //오디오 소스 클립에 voice_janken_start 를 대입시킨다.

        //GOO일 경우 애니메이터 파라미터 GOO를 이용해 해당 애니메이션이 동작하도록한다.
        //해당 동작은 다 작성되어있으니 파라미터에 전달만하면된다.
        //오디오 소스 클립에 voice_janken_goo 를 대입시킨다.

        //CHOKI일 경우 애니메이터 파라미터 Choki를 이용해 해당 애니메이션이 동작하도록한다.
        //해당 동작은 다 작성되어있으니 파라미터에 전달만하면된다.
        //오디오 소스 클립에 voice_janken_choki 를 대입시킨다.

        //PAR일 경우 애니메이터 파라미터 Par를 이용해 해당 애니메이션이 동작하도록한다.
        //해당 동작은 다 작성되어있으니 파라미터에 전달만하면된다.
        //오디오 소스 클립에 voice_janken_Par 를 대입시킨다.
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
    // 가위바위보 시작 버튼을 클릭 시 시작 버튼 off, 가위, 바위, 보 버튼 on
    public void JankenStart()
    {
        flgJanken = true;
        bt.SetActive(false);
        btG.SetActive(true);
    }
    
    //바위
    public void Goo()
    {
        //만약 modeJanken이 1라면
        //myHand 에 GOO를 대입
        //modeJanken 을 하나 증가 시켜
        //판정 모드가 될 수 있도록 한다
        if(modeJanken == 1)
        {
            myHand = GOO;
            modeJanken++;
        }
    }
   
    //가위
    public void Choki()
    {
        //만약 modeJanken이 1라면
        //myHand 에 CHOKI를 대입
        //modeJanken 을 하나 증가 시켜
        //판정 모드가 될 수 있도록 한다
        if (modeJanken == 1)
        {
            myHand = CHOKI;
            modeJanken++;
        }
    }
   
    //보
    public void Par()
    {
        //만약 modeJanken이 1라면
        //myHand 에 PAR를 대입
        //modeJanken 을 하나 증가 시켜
        //판정 모드가 될 수 있도록 한다
        if (modeJanken == 1)
        {
            myHand = PAR;
            modeJanken++;
        }
    }
    public void CheckGame()
    {
        //내핸드와 유니티의핸드가 같다면무승부
        //내핸드와 유니티의 핸드가 같지 않다면
        //내가 GOO일때 유니티가 par라면 유니티의win
        //내가 GOO일때 유니티가 Choki라면 유니티의 loose
        //내가 Choki일때 유니티가 Goo라면 유니티의 win
        //내가 Choki일때 유니티가 Par라면 유니티의 loose
        //내가 par일때 유니티가 Choki라면 유니티의 win
        //내가 par일때 유니티가 Goo라면 유니티의 loose
        
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
