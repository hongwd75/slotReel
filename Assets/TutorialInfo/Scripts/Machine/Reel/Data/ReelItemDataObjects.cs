using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Reel.Data
{
    [CreateAssetMenu(fileName = "ReelItemPool", menuName = "Reel/Data/릴 아이템 데이터 파일 생성", order = 0)]
    public class ReelItemDataObjects : ScriptableObject
    {
        public List<ReelItemData> Objects = new List<ReelItemData>();

        public ReelItemData Get(ReelItemData.ItemType item)
        {
            return Objects.Find(x => x.IsType == item);
        }
    }
}