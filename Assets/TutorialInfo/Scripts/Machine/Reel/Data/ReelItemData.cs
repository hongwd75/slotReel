using System;
using UnityEngine;

namespace GameSystem.Reel
{
    [Serializable]
    public class ReelItemData
    {
        public enum ItemType
        {
            Type1,
            Type2,
            Type3,
            Type4,
            Type5,
            Type6
        }
        
        [SerializeField] private Sprite normal;
        [SerializeField] private Sprite activite;
        [SerializeField] private int score;
        [SerializeField] private ItemType type;

        public Sprite NormalSprite => normal;
        public Sprite activieSprite => activite;
        public int Score => score;
        public ItemType IsType => type;
    }
}