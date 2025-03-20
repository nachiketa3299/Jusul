using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  public class PopUpMenu_Bounty : PopUpMenu
  {
    [SerializeField] HorizontalLayoutGroup _buttonGroup;
    [SerializeField] BountySpawnButton _buttonPrefab;
    [SerializeField] BountyTable _bountyTable;

    public override void Initialize()
    {
      base.Initialize();

      foreach (BountyEnemy bountyEnemyPrefab in _bountyTable.BountyList)
      {
        BountySpawnButton spawnButton
          = Instantiate(_buttonPrefab, _buttonGroup.transform);

        spawnButton.Initialize(bountyEnemyPrefab);
      }
    }
  }
}