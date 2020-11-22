using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;

namespace TJAPlayer4
{
	internal class CActFIFOResult : CActivity
	{
		// メソッド

		public void tフェードアウト開始()
		{
			this.mode = EFIFOモード.フェードアウト;
			this.counter = new CCounter( 0, 100, 30, TJAPlayer4.Timer );
		}
		public void tフェードイン開始()
		{
			this.mode = EFIFOモード.フェードイン;
			this.counter = new CCounter( 0, 300, 2, TJAPlayer4.Timer );
		}
        public void tフェードイン完了()		// #25406 2011.6.9 yyagi
		{
			this.counter.n現在の値 = this.counter.n終了値;
		}

		// CActivity 実装

		public override void On非活性化()
		{
			if( !base.b活性化してない )
			{
                //CDTXMania.tテクスチャの解放( ref this.tx幕 );
				base.On非活性化();
			}
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				//this.tx幕 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\8_background_mask.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override int On進行描画()
		{
			if (base.b活性化してない || (this.counter == null))
			{
				return 0;
			}
			this.counter.t進行();

			// Size clientSize = CDTXMania.app.Window.ClientSize;	// #23510 2010.10.31 yyagi: delete as of no one use this any longer.
			if (TJAPlayer4.Tx.Tile_Black != null)
			{
				if (this.mode == EFIFOモード.フェードイン)
				{
					TJAPlayer4.Tx.Tile_Black.Opacity = (counter.n現在の値 >= 200) ? (((100 - (this.counter.n現在の値 - 200)) * 0xff) / 100) : 255;
				}
				else
				{
					TJAPlayer4.Tx.Tile_Black.Opacity = (((this.counter.n現在の値) * 0xff) / 100);
				}

				for (int i = 0; i <= (SampleFramework.GameWindowSize.Width / 64); i++)
				{
					for (int j = 0; j <= (SampleFramework.GameWindowSize.Height / 64); j++)
					{
						TJAPlayer4.Tx.Tile_Black.t2D描画(TJAPlayer4.app.Device, i * 64, j * 64);
					}
				}
			}
			if ( this.mode == EFIFOモード.フェードアウト )
            {
			    if( this.counter.n現在の値 != 100 )
			    {
				    return 0;
			    }
            }
            else if( this.mode == EFIFOモード.フェードイン )
            {
			    if( this.counter.n現在の値 != 300 )
			    {
				    return 0;
			    }
            }
			return 1;
		}


		// その他

		#region [ private ]
		//-----------------
		private CCounter counter;
		private EFIFOモード mode;
        //private CTexture tx幕;
		//-----------------
		#endregion
	}
}
