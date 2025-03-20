using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;


namespace Jusul
{
  [DisallowMultipleComponent]
  public class GameBootstrapManager : MonoBehaviour
  {
    [Header("Player Infos")][Space]
    [SerializeField] PlayerInfo _playerInfo;
    [SerializeField] List<PlayerInfo> _otherPlayerInfos = new();

    [Header("AI Controller")][Space]
    [SerializeField] AIController _aiControllerPrefab;

    [Header("Player Controller")][Space]
    [SerializeField] PlayerController _playerController;

    List<JCharacterController> _controllerLaneOrder = new(4);

    void Awake()
    {
      // 플레이어 생성 및 컨트롤러 연결

      // 플레이어용 컨트롤러 초기화
      // _playerController.Initialize(_playerInfo);
      _controllerLaneOrder.Add(_playerController);

      // 다른 플레이어 생성 및 컨트롤러 연결

      // 다른 플레이어 목록 중 랜덤으로 3개 추출
      HashSet<int> selectedIndices = new();
      while (selectedIndices.Count < 3) 
        selectedIndices.Add(Random.Range(0, _otherPlayerInfos.Count));

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
        JCharacterController controller = _controllerLaneOrder[laneIndex];

        if (controller is PlayerController)
        {
          controller.Initialize(laneIndex, _playerInfo);
        }
        else
        {
          controller.Initialize(laneIndex, _otherPlayerInfos[randomAIPlayerIndices[otherIndex]]);
          ++otherIndex;
        }
      }
    }

    void Start()
    {
      // 레인 매니저가 싱글턴이라 여기서 수행
      for (int laneIndex = 0; laneIndex < 4; ++laneIndex)
      {
        _controllerLaneOrder[laneIndex].MoveControllingCharacterToLaneStart();
        LaneManager.Instance.SetLanePlayer(laneIndex, _controllerLaneOrder[laneIndex]);
      }
    }
  }
}
