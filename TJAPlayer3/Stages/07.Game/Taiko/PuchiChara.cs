using TJAPlayer4;
using FDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TJAPlayer4
{
    class PuchiChara : CActivity
    {
        public PuchiChara()
        {
            base.b活性化してない = true;
        }

        public override void On活性化()
        {
            Counter = new CCounter(0, TJAPlayer4.Skin.Game_PuchiChara[2] - 1, TJAPlayer4.Skin.Game_PuchiChara_Timer, TJAPlayer4.Timer);
            SineCounter = new CCounter(0, 360, TJAPlayer4.Skin.Game_PuchiChara_SineTimer, CSound管理.rc演奏用タイマ);
            base.On活性化();
        }
        public override void On非活性化()
        {
            Counter = null;
            SineCounter = null;
            base.On非活性化();
        }
        
        public void ChangeBPM(double bpm)
        {
            Counter = new CCounter(0, TJAPlayer4.Skin.Game_PuchiChara[2] - 1, (int)(TJAPlayer4.Skin.Game_PuchiChara_Timer * bpm / TJAPlayer4.Skin.Game_PuchiChara[2]), TJAPlayer4.Timer);
            SineCounter = new CCounter(1, 360, TJAPlayer4.Skin.Game_PuchiChara_SineTimer * bpm / 180, CSound管理.rc演奏用タイマ);
        }

        /// <summary>
        /// ぷちキャラを描画する。(オーバーライドじゃないよ)
        /// </summary>
        /// <param name="x">X座標(中央)</param>
        /// <param name="y">Y座標(中央)</param>
        /// <param name="alpha">不透明度</param>
        /// <returns></returns>
        public int On進行描画(int x, int y, bool isGrowing, int alpha = 255, bool isBalloon = false)
        {
            if (!TJAPlayer4.ConfigIni.ShowPuchiChara) return base.On進行描画();
            if (Counter == null || SineCounter == null || TJAPlayer4.Tx.PuchiChara == null) return base.On進行描画();
            Counter.t進行Loop();
            SineCounter.t進行LoopDb();
            var sineY = Math.Sin(SineCounter.db現在の値 * (Math.PI / 180)) * (TJAPlayer4.Skin.Game_PuchiChara_Sine * (isBalloon ? TJAPlayer4.Skin.Game_PuchiChara_Scale[1] : TJAPlayer4.Skin.Game_PuchiChara_Scale[0]));
            TJAPlayer4.Tx.PuchiChara.vc拡大縮小倍率 = new SlimDX.Vector3((isBalloon ? TJAPlayer4.Skin.Game_PuchiChara_Scale[1] : TJAPlayer4.Skin.Game_PuchiChara_Scale[0]));
            TJAPlayer4.Tx.PuchiChara.Opacity = alpha;
            TJAPlayer4.Tx.PuchiChara.t2D中心基準描画(TJAPlayer4.app.Device, x, y + (int)sineY, new Rectangle(Counter.n現在の値 * TJAPlayer4.Skin.Game_PuchiChara[0], (isGrowing ? TJAPlayer4.Skin.Game_PuchiChara[1] : 0), TJAPlayer4.Skin.Game_PuchiChara[0], TJAPlayer4.Skin.Game_PuchiChara[1]));
            return base.On進行描画();
        }

        private CCounter Counter;
        private CCounter SineCounter;
    }
}