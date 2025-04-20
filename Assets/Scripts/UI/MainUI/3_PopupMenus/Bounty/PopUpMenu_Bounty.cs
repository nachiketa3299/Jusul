using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  public class PopUpMenu_Bounty : PopUpMenuBase
  {
    [SerializeField] HorizontalLayoutGroup _buttonGroup;
    [SerializeField] BountySpawnButton _buttonPrefab;
    [SerializeField] BountyTable _bountyTable;

    public override void InitializeOnAwake()
    {
      base.InitializeOnAwake();

      foreach (BountyEnemy bountyEnemyPrefab in _bountyTable.BountyList)
      {
        BountySpawnButton spawnButton
          = Instantiate(_buttonPrefab, _buttonGroup.transform);

        spawnButton.InitializeOnAwake(bountyEnemyPrefab);
      }
    }
  }
}