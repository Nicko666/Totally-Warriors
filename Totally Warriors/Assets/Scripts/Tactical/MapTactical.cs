
using UnityEngine;

public class MapTactical : MonoBehaviour
{
    [SerializeField]
    Transform[] _upPositions;
    public Transform[] UpPositions => _upPositions;

    [SerializeField]
    Transform[] _downPositions;
    public Transform[] DownPositions => _downPositions;

    private void OnMouseUp()
    {        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var playerUnits = SceneTactical.Instance.CharacterPlayer.Units;

            if (playerUnits.Length == 0) return;

            foreach (var unit in playerUnits)
            {
                SceneTactical.Instance.MapClick(hit.point);

            }

        }

    }


}
