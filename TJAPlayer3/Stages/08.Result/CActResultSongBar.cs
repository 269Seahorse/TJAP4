using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using SlimDX;
using FDK;

namespace TJAPlayer4
{
	internal class CActResultSongBar : CActivity
	{
		// コンストラクタ

		public CActResultSongBar()
		{
			base.b活性化してない = true;
		}


		// メソッド

		public void tアニメを完了させる()
		{
			this.ct登場用.n現在の値 = this.ct登場用.n終了値;
		}


		// CActivity 実装

		public override void On活性化()
		{
            if( !string.IsNullOrEmpty( TJAPlayer4.ConfigIni.FontName) )
            {
                this.pfMusicName = new CPrivateFastFont(new FontFamily(TJAPlayer4.ConfigIni.FontName), TJAPlayer4.Skin.Result_MusicName_FontSize);
                this.pfStageText = new CPrivateFastFont(new FontFamily(TJAPlayer4.ConfigIni.FontName), TJAPlayer4.Skin.Result_StageText_FontSize);
            }
            else
            {
                this.pfMusicName = new CPrivateFastFont(new FontFamily("MS UI Gothic"), TJAPlayer4.Skin.Result_MusicName_FontSize);
                this.pfStageText = new CPrivateFastFont(new FontFamily("MS UI Gothic"), TJAPlayer4.Skin.Result_StageText_FontSize);
            }

		    // After performing calibration, inform the player that
		    // calibration has been completed, rather than
		    // displaying the song title as usual.


		    var title = TJAPlayer4.IsPerformingCalibration
		        ? $"Calibration complete. InputAdjustTime is now {TJAPlayer4.ConfigIni.nInputAdjustTimeMs}ms"
		        : TJAPlayer4.DTX[0].TITLE;

		    using (var bmpSongTitle = pfMusicName.DrawPrivateFont(title, TJAPlayer4.Skin.Result_MusicName_ForeColor, TJAPlayer4.Skin.Result_MusicName_BackColor))

		    {
		        this.txMusicName = TJAPlayer4.tテクスチャの生成(bmpSongTitle, false);
		        txMusicName.vc拡大縮小倍率.X = TJAPlayer4.GetSongNameXScaling(ref txMusicName);
		    }

		    using (var bmpStageText = pfStageText.DrawPrivateFont(TJAPlayer4.Skin.Game_StageText, TJAPlayer4.Skin.Result_StageText_ForeColor, TJAPlayer4.Skin.Result_StageText_BackColor))
		    {
		        this.txStageText = TJAPlayer4.tテクスチャの生成(bmpStageText, false);
		    }

			base.On活性化();
		}
		public override void On非活性化()
		{
			if( this.ct登場用 != null )
			{
				this.ct登場用 = null;
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                TJAPlayer4.t安全にDisposeする(ref this.pfMusicName);
                TJAPlayer4.tテクスチャの解放( ref this.txMusicName );

                TJAPlayer4.t安全にDisposeする(ref this.pfStageText);
                TJAPlayer4.tテクスチャの解放(ref this.txStageText);
                base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( base.b活性化してない )
			{
				return 0;
			}
			if( base.b初めての進行描画 )
			{
				this.ct登場用 = new CCounter( 0, 270, 4, TJAPlayer4.Timer );
				base.b初めての進行描画 = false;
			}
			this.ct登場用.t進行();

            if (TJAPlayer4.Skin.Result_MusicName_ReferencePoint == CSkin.ReferencePoint.Center)
            {
                this.txMusicName.t2D描画(TJAPlayer4.app.Device, TJAPlayer4.Skin.Result_MusicName_X - ((this.txMusicName.szテクスチャサイズ.Width * txMusicName.vc拡大縮小倍率.X) / 2), TJAPlayer4.Skin.Result_MusicName_Y);
            }
            else if (TJAPlayer4.Skin.Result_MusicName_ReferencePoint == CSkin.ReferencePoint.Left)
            {
                this.txMusicName.t2D描画(TJAPlayer4.app.Device, TJAPlayer4.Skin.Result_MusicName_X, TJAPlayer4.Skin.Result_MusicName_Y);
            }
            else
            {
                this.txMusicName.t2D描画(TJAPlayer4.app.Device, TJAPlayer4.Skin.Result_MusicName_X - this.txMusicName.szテクスチャサイズ.Width * txMusicName.vc拡大縮小倍率.X, TJAPlayer4.Skin.Result_MusicName_Y);
            }

            if(TJAPlayer4.stage選曲.n確定された曲の難易度[0] != (int)Difficulty.Dan)
            {
                if (TJAPlayer4.Skin.Result_StageText_ReferencePoint == CSkin.ReferencePoint.Center)
                {
                    this.txStageText.t2D描画(TJAPlayer4.app.Device, TJAPlayer4.Skin.Result_StageText_X - ((this.txStageText.szテクスチャサイズ.Width * txStageText.vc拡大縮小倍率.X) / 2), TJAPlayer4.Skin.Result_StageText_Y);
                }
                else if (TJAPlayer4.Skin.Result_StageText_ReferencePoint == CSkin.ReferencePoint.Right)
                {
                    this.txStageText.t2D描画(TJAPlayer4.app.Device, TJAPlayer4.Skin.Result_StageText_X - this.txStageText.szテクスチャサイズ.Width, TJAPlayer4.Skin.Result_StageText_Y);
                }
                else
                {
                    this.txStageText.t2D描画(TJAPlayer4.app.Device, TJAPlayer4.Skin.Result_StageText_X, TJAPlayer4.Skin.Result_StageText_Y);
                }
            }


			if( !this.ct登場用.b終了値に達した )
			{
				return 0;
			}
			return 1;
		}


		// その他

		#region [ private ]
		//-----------------
		private CCounter ct登場用;

        private CTexture txMusicName;
        private CPrivateFastFont pfMusicName;

        private CTexture txStageText;
        private CPrivateFont pfStageText;
        //-----------------
		#endregion
	}
}
