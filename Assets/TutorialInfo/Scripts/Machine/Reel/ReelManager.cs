using System;
using System.Collections.Generic;
using System.Linq;
using GameSystem.Reel.Data;
using UnityEngine;

namespace GameSystem.Reel
{
    public class ReelManager : MonoBehaviour
    {
        // 서버에서 데이터를 전달하는 방식으로 개발하는 것이 좋다.
        private int[] recive_ReelItemData = new[]
        {
            0, 1, 2, 3, 4,5,
            2, 4, 0, 1, 5,3,
            3, 4, 5, 0, 1,2,
            4, 3, 2, 1, 0,5,
            3, 5, 1, 0, 2,4
        };

        [SerializeField] private Reel[] ReelParent;
        private ReelItemDataObjects realitemDataObject;


        private void Start()
        {
            Init();
            OnReelData(recive_ReelItemData);
        }

        public void Init(string reelObjectFile = "Reel/ReelItemData")
        {
            realitemDataObject = Instantiate(Resources.Load<ReelItemDataObjects>(reelObjectFile));
        }

        public void OnSpin()
        {
            foreach (var reel in ReelParent)
            {
                reel.OnSpin();
            }
        }

        public void OnReelData(int[] reeldata)
        {
            for (int i = 0; i < 5; i++)
            {
                int[] row = reeldata.Skip(i * 6).Take(6).ToArray();
                AddReel(i,row);
            }            
        }
        
        private void AddReel(int index, int[] slotdata)
        {
            if (index >= ReelParent.Length) return;

            List<ReelItemData> reelitem = new List<ReelItemData>();
            
            foreach (var i in slotdata)
            {
                reelitem.Add(realitemDataObject.Get((ReelItemData.ItemType)i));
            }

            ReelParent[index].SetReel(reelitem);
        }
    }
}