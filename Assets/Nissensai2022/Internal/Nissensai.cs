using System;
using Nissensai2022.Internal;

namespace Nissensai2022.Runtime
{
    public class Nissensai
    {
        /// <summary>
        /// 現在のプレイヤー情報
        /// </summary>
        public static Player CurrentPlayer => SystemStatusManager.CurrentPlayer;

        /// <summary>
        /// ゲームのリザルト(5段階評価)をサーバーに送信する。
        /// </summary>
        /// <param name="rank">5段階評価。ResultRank.AやResultRank.Eのように指定する。</param>
        public static void SendResult(ResultRank rank)
        {
            ResultUploader.UploadResult(rank);
        }

        /// <summary>
        /// 途中でゲームを中止した場合はこれを使ってサーバーの状態を更新してください。これを使わないとQRコードが表示されない。
        /// </summary>
        public static void GiveUp()
        {
            ResultUploader.UploadResult(ResultRank.E);
        }

        /// <summary>
        /// 手動でQRコードを表示する。
        /// </summary>
        public static void ShowQrCode()
        {
            SystemStatusManager.ShowQrCode();
        }

        /// <summary>
        /// 手動でQRコードを非表示する。
        /// </summary>
        public static void HideQrCode()
        {
            SystemStatusManager.HideQrCode();
        }

        /// <summary>
        /// 現在のプレイヤーの名前を取得
        /// </summary>
        /// <returns>string 現在のプレイヤーの名前</returns>
        public static string GetPlayerName()
        {
            return SystemStatusManager.CurrentPlayer.Name;
        }

        /// <summary>
        /// 新しいゲームトークンを取得する
        /// </summary>
        public static void GetNewToken()
        {
            SystemStatusManager.RunTask(SystemStatusManager.GetNewGameToken());
        }

        /// <summary>
        /// コンソールコマンドを新規追加する。
        /// </summary>
        /// <param name="command">コマンドの文字列(なんと打てば実行されるか)</param>
        /// <param name="method">文字列を返すメソッド。例えば：string Method(){}</param>
        public static void AddConsoleMethod(string command, Func<string> method)
        {
            Nissensai2022.Console.Console.AddMethod(command, method);
        }
        
        /// <summary>
        /// コンソールコマンドを新規追加する。
        /// </summary>
        /// <param name="command">コマンドの文字列(なんと打てば実行されるか)</param>
        /// <param name="method">例えば：string Method(string paras){}</param>
        public static void AddConsoleMethod(string command, Func<string,string> method)
        {
            Nissensai2022.Console.Console.AddMethod(command, method);
        }
        
    }
}