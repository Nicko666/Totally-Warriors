using System.Linq;
using UnityEngine;

public class Icon : MonoBehaviour
{
    [SerializeField]
    private Unit _unit;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private float _baseHighlite;

    private void OnEnable()
    {
        _baseHighlite = _spriteRenderer.color.a;

    }

    private void Update()
    {
        if (!_unit.IsDefeated)
        {
            transform.position = new(_unit.GetUnitPosition().x, transform.position.y, _unit.GetUnitPosition().z);
        }

    }

    private void OnMouseUpAsButton() => Selected();

    public void Selected()
    {
        _unit.OnSelected();

        if (_unit.IsSelected)
        {
            Highlite(true);
        }

    }

    public void Highlite(bool value)
    {
        _spriteRenderer.color = new(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, (value)? _baseHighlite * 2 : _baseHighlite);
    }

}
