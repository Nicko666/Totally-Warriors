using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TacticalUI : MonoBehaviour
{
    [field: SerializeField] public UnitsCanvas UpUnitsCanvas { get; private set; }
    [field: SerializeField] public UnitsCanvas LowUnitsCanvas { get; private set; }
    [field: SerializeField] public TacticalStatus tacticalStatus { get; private set; }

    [field: SerializeField] public TacticalSceneManager SceneManager { get; private set; }

    public void SetStatus(TacticalSceneManager sceneManager)
    {
        SceneManager = sceneManager;

        foreach (var unit in sceneManager.playerUnits) unit.TakeDamageAction += UpdateStatus;
        foreach (var unit in sceneManager.aiUnits) unit.TakeDamageAction += UpdateStatus;

        tacticalStatus.SetColors(sceneManager.Player.Color, sceneManager.AI.Color);

        tacticalStatus.SetNumber(sceneManager.PowerBallance);

    }

    void UpdateStatus(List<int> health)
    {
        tacticalStatus.SetNumber(SceneManager.PowerBallance);

    }

}
