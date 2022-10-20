using Nissensai2022.Runtime;
using UnityEngine;

namespace Nissensai2022.Sample
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        private float _timer = 5f;
        private int _count = 0;
        private bool _isGaming = false;
        private ResultRank rank = ResultRank.E;

        /// <summary>
        /// ゲーム開始時に実行される。SystemStatusManagerのInspector Windowでの設定が必要。
        /// </summary>
        public void OnGameStart()
        {
            _timer = 5f;
            _count = 0;
            rank = ResultRank.E;
            panel.SetActive(false);
            _isGaming = true;
        }
        
        /// <summary>
        /// ゲーム終了時(リザルトが送信される・中止関数が実行される)に実行される。SystemStatusManagerのInspector Windowでの設定が必要。
        /// </summary>
        public void OnGameStop()
        {
            panel.SetActive(true);
            Debug.Log("Result: " + rank);
        }

        void Update()
        {
            if (!_isGaming)
            {
                return;
            }

            if (Input.anyKeyDown)
            {
                Debug.Log(++_count);
            }
            
            _timer -= Time.deltaTime;
            if (_timer < 0)
            {
                _isGaming = false;
                if (_count > 10)
                    rank = ResultRank.A;
                else if (_count > 6)
                    rank = ResultRank.B;
                else if (_count > 4)
                    rank = ResultRank.C;
                else if (_count > 2)
                    rank = ResultRank.D;

                Nissensai.SendResult(rank);
            }
        }
    }
}