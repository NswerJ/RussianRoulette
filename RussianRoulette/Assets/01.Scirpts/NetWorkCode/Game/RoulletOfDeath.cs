using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking.Types;
using UnityEngine.UIElements;

public class RoulletOfDeath : MonoBehaviour
{
    private enum GameProgress
    {
        None = 0,       // 게임 시작 전.
        Ready,          // 게임 시작 신호 표시.
        Turn,           // 게임 진행 중.
        Result,         // 결과 표시.
        GameOver,       // 게임 종료.
        Disconnect,     // 연결 끊기.

    };
    

    private enum Turn
    {
        Player = 0,     // 플레이어 턴.
        Opponent,       // 상대 턴.
    };

    private enum ChamberState
    {
        Empty = 0,      // 총실이 비어있는 상태.
        Bullet,         // 총실에 총알이 있는 상태.
    };

    private enum Result
    {
        None = 0,       // 아직 결과 없음.
        Win,            // 이겼음.
        Lose,           // 졌음.
    };

    // 시합 시작 전의 신호표시 시간.
    private const float waitTime = 1.0f;
    // 대기 시간.
    private float currentTime;

    private GameProgress progress;
    private Turn turn;
    private ChamberState[] chambers;    // 러시안 룰렛 총실 배열.
    private int currentChamberIndex;    // 현재 총실 인덱스.
    private Result result;              // 게임 결과.
    private bool isGameOver;            // 게임 종료 플래그.
    private NetworkManager network = null;


    // 게임 시작 시 초기화 함수.
    void Start()
    {
        GameObject obj = GameObject.Find("NetworkManager");
        network = obj.GetComponent<NetworkManager>();
        if (network != null)
        {
            network.RegisterEventHandler(EventCallback);
        }

        ResetGame();
    }

    public void GameStart()
    {
        // 게임 시작 상태로 설정합니다.
        progress = GameProgress.Ready;

        // 서버가 먼저 시작하게 설정합니다.
        if (network.IsServer())
        {
            turn = Turn.Player;
        }
        else
        {
            turn = Turn.Opponent;
        }
        Debug.Log("게임 시작..");
        // 총실에 총알을 랜덤하게 넣습니다.
        int bulletIndex = Random.Range(0, chambers.Length);
        chambers[bulletIndex] = ChamberState.Bullet;

        // 이전 설정을 초기화합니다.
        isGameOver = false;
    }

    // 게임 초기화 함수.
    void ResetGame()
    {
        progress = GameProgress.None;
        turn = Turn.Player;
        chambers = new ChamberState[6];  // 6개의 총실.
        currentChamberIndex = 0;
        result = Result.None;
        isGameOver = false;

        // 총알을 랜덤하게 1개만 넣음.
        int bulletIndex = Random.Range(0, chambers.Length);
        chambers[bulletIndex] = ChamberState.Bullet;
    }

    // 게임 업데이트 함수.
    void Update()
    {
        switch (progress)
        {
            case GameProgress.Ready:
                UpdateReady();
                // 게임 시작 신호 표시.
                // 완전히 러시안 룰렛 게임으로 변경하려면 여기에 해당하는 코드를 추가해야 합니다.
                break;
            case GameProgress.Turn:
                // 게임 진행 중.
                if (turn == Turn.Player)
                {
                    // 플레이어의 턴일 때의 처리.
                    PlayerTurn();
                    Debug.Log("플레이어 턴");
                }
                else
                {
                    // 상대의 턴일 때의 처리.
                    OpponentTurn();
                    Debug.Log("적 턴");
                }
                break;
            case GameProgress.Result:
                // 결과 표시.
                DisplayResult();
                break;
            case GameProgress.GameOver:
                // 게임 종료.
                // 게임 종료 후의 처리를 수행할 수 있습니다.
                break;
        }
    }
    void UpdateReady()
    {
        // 시합 시작 신호 표시를 기다립니다.
        currentTime += Time.deltaTime;

        if (currentTime > waitTime)
        {
            // 게임 시작입니다.
            progress = GameProgress.Turn;

        }
    }

    // 플레이어의 턴 처리 함수.
    void PlayerTurn()
    {
        // 플레이어가 방아쇠를 당김.
        if (Input.GetMouseButton(0))
        {
            PullTrigger();

            Debug.Log("플레이어가 총쏨");
            turn = Turn.Opponent;
        }

        // 총알이 발사되었는지 확인.
        if (chambers[currentChamberIndex] == ChamberState.Bullet)
        {
            // 총알이 발사되었을 때의 처리.
            result = Result.Lose;
            progress = GameProgress.Result;
            isGameOver = true;
        }
        
    }

    // 상대의 턴 처리 함수.
    void OpponentTurn()
    {
        // 상대가 방아쇠를 당김.
        if (Input.GetMouseButton(0))
        {
            PullTrigger();
            Debug.Log("적이 총쏨");

            turn = Turn.Player;

        }

        // 총알이 발사되었는지 확인.
        if (chambers[currentChamberIndex] == ChamberState.Bullet)
        {
            // 총알이 발사되었을 때의 처리.
            result = Result.Win;
            progress = GameProgress.Result;
            isGameOver = true;
        }
        
    }

    // 방아쇠를 당기는 함수.
    void PullTrigger()
    {
        // 현재 총실의 상태를 확인하고 다음 총실로 이동.
        chambers[currentChamberIndex] = ChamberState.Empty;
        currentChamberIndex = (currentChamberIndex + 1) % chambers.Length;
    }

    // 결과 표시 함수.
    void DisplayResult()
    {
        // 결과에 따라 UI 등을 통해 승패를 표시할 수 있습니다.
    }

    // 게임 종료 여부 확인 함수.
    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void EventCallback(NetEventState state)
    {
        switch (state.type)
        {
            case NetEventType.Disconnect:
                if (progress < GameProgress.Result && isGameOver == false)
                {
                    progress = GameProgress.Disconnect;
                }
                break;
        }
    }
    }