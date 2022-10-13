using UnityEngine;
using System.Collections;
using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer.Configuration;
using ZXing;
using ZXing.QrCode;

//https://negipoyoc.com/blog/making-qrcode-with-unity/
namespace Nissensai2022.Internal
{
    internal class QRCodeUtil
    {
        private const int Size = 300;
        /// <summary>
        /// QRコード作成
        /// </summary>
        /// <param name="inputString">QRコード生成元の文字列</param>
        /// <param name="textture">QRの画像がここに入る</param>
        internal static Texture2D CreateTexture(string inputString, int width, int height)
        {
            var texture = new Texture2D(width, height);
            var qrCodeColors = Write(inputString, width, height);
            texture.SetPixels32(qrCodeColors);
            texture.Apply();
            return texture;
        }

        internal static Sprite CreateSprite(string content)
        {
            var tex = CreateTexture(content, Size, Size);
            return Sprite.Create(tex, new Rect(0, 0, Size, Size), Vector2.one * 0.5f);
        }


        private static Color32[] Write(string content, int width, int height)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width
                }
            };

            return writer.Write(content);
        }
    }
}