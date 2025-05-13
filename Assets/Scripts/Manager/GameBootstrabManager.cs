using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace Jusul
{
  /// <summary>
  /// 게임 시작시 전체 초기화를 담당
  /// </summary>
  [DisallowMultipleComponent]
  public class GameBootstrapManager : MonoBehaviour
  {
    [Header("싱글턴")][Space]
    [SerializeField] WaveManager _waveManager;
    [SerializeField] LaneManager _laneManager;
    [SerializeField] BeamEffectManager _beamEffectManager;
    [SerializeField] DamageIndicationManager _damageIndicationManager;

    [Header("캐릭터 프리팹")][Space]
    [SerializeField] CharacterModel _characterModelPrefab;

    [Header("게임 참여 플레이어 정보")][Space]
    [SerializeField] PlayerInfo _playerInfo;
    [SerializeField] List<PlayerInfo> _otherPlayerInfos = new();

    [Header("AI 컨트롤러 프리팹")][Space]
    [SerializeField] AIController _aiControllerPrefab;

    [Header("플레이어 씬 레퍼런스")][Space]
    [SerializeField] PlayerController _playerController;
    [SerializeField] MainUIInitializationHandler _mainUI;

    List<JusulCharacterControllerBase> _controllerLaneOrder = new(4);

    void Awake()
    {
      // 싱글턴 초기화
      _waveManager.InitializeSingleton();
      _laneManager.InitializeSingleton();
      _beamEffectManager.InitializeSingleton();
      _damageIndicationManager.InitializeSingleton();

      // 이벤트 구독 등 먼저 일어나야 하는 것들 
      _mainUI.InitializationOnAwake();
    }

    void Start()
    {
      // 플레이어 생성 및 컨트롤러 연결

      // 플레이어용 컨트롤러 초기화
      // _playerController.Initialize(_playerInfo);
      _controllerLaneOrder.Add(_playerController);

      // 다른 플레이어 생성 및 컨트롤러 연결

      // 다른 플레이어 목록 중 랜덤으로 3개 추출
      HashSet<int> selectedIndices = new();
      while (selectedIndices.Count < 3) 
      {
        selectedIndices.Add(Random.Range(0, _otherPlayerInfos.Count));
      }

      foreach (int idx in selectedIndices)
      {
        // 컨트롤러 생성
        AIController apc = Instantiate(_aiControllerPrefab);
        apc.name = $"AIController_{_otherPlayerInfos[idx].PlayerId}";

        _controllerLaneOrder.Add(apc);
      }

      // 레인 오더를 랜덤으로
      for (int i = 0; i < _controllerLaneOrder.Count; ++i) 
      {
        int j = Random.Range(i, _controllerLaneOrder.Count);

        (_controllerLaneOrder[i], _controllerLaneOrder[j]) 
          = (_controllerLaneOrder[j], _controllerLaneOrder[i]);
      }

      // 레인 인덱스와 함께 한꺼번에 초기화

      int otherIndex = 0;
      List<int> randomAIPlayerIndices = selectedIndices.ToList();

      for (int laneIndex = 0; laneIndex < _controllerLaneOrder.Count; ++laneIndex)
      {
        JusulCharacterControllerBase controller = _controllerLaneOrder[laneIndex];

        if (controller is PlayerController)
        {
          controller.InitializeOnStart(laneIndex, _characterModelPrefab, _playerInfo);
        }
        else
        {
          controller.InitializeOnStart(laneIndex, _characterModelPrefab, _otherPlayerInfos[randomAIPlayerIndices[otherIndex]]);
          ++otherIndex;
        }
      }

      // 레인 매니저가 싱글턴이라 여기서 수행
      for (int laneIndex = 0; laneIndex < 4; ++laneIndex)
      {
        _controllerLaneOrder[laneIndex].MoveControllingCharacterToLaneStart();
        LaneManager.Instance.SetLanePlayer(laneIndex, _controllerLaneOrder[laneIndex]);
      }
    }
  }
}
