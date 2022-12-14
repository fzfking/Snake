using Sources.Architecture.Interfaces;
using Sources.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Presenters
{
    public class CellPresenter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer EntityView;
        private ICell _cell;
        private EntitySprites _sprites;

        public void Init(ICell cell, EntitySprites sprites)
        {
            _cell = cell;
            _sprites = sprites;
            transform.position = (Vector2)_cell.Position;
            _cell.EntityChanged += ChangeEntityView;
        }

        private void ChangeEntityView()
        {
            if (_cell.Entity == null)
            {
                EntityView.sprite = null;
                return;
            }
            EntityView.sprite = _sprites.GetSpriteByType(_cell.Entity.Type);
        }

        public void DeInit()
        {
            _cell.EntityChanged -= ChangeEntityView;
            _sprites = null;
            _cell = null;
        }
        
    }
}