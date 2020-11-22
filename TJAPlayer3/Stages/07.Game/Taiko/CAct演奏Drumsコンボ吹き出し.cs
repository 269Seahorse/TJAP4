using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using SlimDX;
using FDK;

namespace TJAPlayer4
{
	internal class CAct演奏Drumsコンボ吹き出し : CActivity
	{
		// コンストラクタ

        /// <summary>
        /// 100コンボごとに出る吹き出し。
        /// 本当は「10000点」のところも動かしたいけど、技術不足だし保留。
        /// </summary>
		public CAct演奏Drumsコンボ吹き出し()
		{
			base.b活性化してない = true;
		}
		
		
		// メソッド
        public virtual void Start( int nCombo, int player )
		{
            this.ct進行[ player ] = new CCounter( 1, 103, 20, TJAPlayer4.Timer );
            this.nCombo_渡[ player ] = nCombo;
		}

		// CActivity 実装

		public override void On活性化()
		{
            for( int i = 0; i < 2; i++ )
            {
                this.nCombo_渡[ i ] = 0;
                this.ct進行[ i ] = new CCounter();
            }

            base.On活性化();
		}
		public override void On非活性化()
		{
            for( int i = 0; i < 2; i++ )
            {
                this.ct進行[ i ] = null;
            }
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                //this.tx吹き出し本体[ 0 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_combo balloon.png" ) );
                //if (CDTXMania.stage演奏ドラム画面.bDoublePlay)
                //    this.tx吹き出し本体[ 1 ] = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_combo balloon_2P.png" ) );
                //this.tx数字 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_combo balloon_number.png" ) );
				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                //CDTXMania.tテクスチャの解放( ref this.tx吹き出し本体[ 0 ] );
                //if (CDTXMania.stage演奏ドラム画面.bDoublePlay)
                //    CDTXMania.tテクスチャの解放( ref this.tx吹き出し本体[ 1 ] );
                //CDTXMania.tテクスチャの解放( ref this.tx数字 );
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
                for( int i = 0; i < 2; i++ )
                {
                    if( !this.ct進行[ i ].b停止中 )
                    {
                        this.ct進行[ i ].t進行();
                        if( this.ct進行[ i ].b終了値に達した )
                        {
                            this.ct進行[ i ].t停止();
                        }
                    }

                    if( TJAPlayer4.Tx.Balloon_Combo[ i ] != null )
                    {
                        //半透明4f
                        if( this.ct進行[0].n現在の値 == 1 || this.ct進行[0].n現在の値 == 103 )
                        {
                            TJAPlayer4.Tx.Balloon_Combo[0].Opacity = 64;
                            TJAPlayer4.Tx.Balloon_Number_Combo[0].Opacity = 64;
                        }
                        else if( this.ct進行[0].n現在の値 == 2 || this.ct進行[0].n現在の値 == 102 )
                        {
                            TJAPlayer4.Tx.Balloon_Combo[0].Opacity = 128;
                            TJAPlayer4.Tx.Balloon_Number_Combo[0].Opacity = 128;
                        }
                        else if( this.ct進行[0].n現在の値 == 3 || this.ct進行[0].n現在の値 == 101 )
                        {
                            TJAPlayer4.Tx.Balloon_Combo[0].Opacity = 192;
                            TJAPlayer4.Tx.Balloon_Number_Combo[0].Opacity = 192;
                        }
                        else if( this.ct進行[0].n現在の値 >= 4 && this.ct進行[0].n現在の値 <= 100 )
                        {
                            TJAPlayer4.Tx.Balloon_Combo[0].Opacity = 255;
                            TJAPlayer4.Tx.Balloon_Number_Combo[0].Opacity = 255;
                        }

                        if( this.ct進行[0].b進行中 )
                        {
                            TJAPlayer4.Tx.Balloon_Combo[0].t2D描画( TJAPlayer4.app.Device, TJAPlayer4.Skin.Game_Balloon_Combo_X[0], TJAPlayer4.Skin.Game_Balloon_Combo_Y[0] );
                            if( this.nCombo_渡[0] < 1000 ) //2016.08.23 kairera0467 仮実装。
                            {
                                this.t小文字表示1( TJAPlayer4.Skin.Game_Balloon_Combo_Number_X[0], TJAPlayer4.Skin.Game_Balloon_Combo_Number_Y[0], string.Format( "{0,4:###0}", this.nCombo_渡[0] ) );
                                TJAPlayer4.Tx.Balloon_Number_Combo[0].t2D描画( TJAPlayer4.app.Device, TJAPlayer4.Skin.Game_Balloon_Combo_Text_X[0], TJAPlayer4.Skin.Game_Balloon_Combo_Text_Y[0], new Rectangle(0, 67, 78, 27));
                            }
                            else
                            {
                                this.t小文字表示1( TJAPlayer4.Skin.Game_Balloon_Combo_Number_Ex_X[0], TJAPlayer4.Skin.Game_Balloon_Combo_Number_Ex_Y[0], string.Format( "{0,4:###0}", this.nCombo_渡[0] ) );
                                TJAPlayer4.Tx.Balloon_Number_Combo[0].t2D描画( TJAPlayer4.app.Device, TJAPlayer4.Skin.Game_Balloon_Combo_Text_Ex_X[0], TJAPlayer4.Skin.Game_Balloon_Combo_Text_Ex_Y[0], new Rectangle( 0, 67, 78, 27 ) );
                            }
                        }

                        if (this.ct進行[1].n現在の値 == 1 || this.ct進行[1].n現在の値 == 103)
                        {
                            TJAPlayer4.Tx.Balloon_Combo[1].Opacity = 64;
                            TJAPlayer4.Tx.Balloon_Number_Combo[1].Opacity = 64;
                        }
                        else if (this.ct進行[1].n現在の値 == 2 || this.ct進行[1].n現在の値 == 102)
                        {
                            TJAPlayer4.Tx.Balloon_Combo[1].Opacity = 128;
                            TJAPlayer4.Tx.Balloon_Number_Combo[1].Opacity = 128;
                        }
                        else if (this.ct進行[1].n現在の値 == 3 || this.ct進行[1].n現在の値 == 101)
                        {
                            TJAPlayer4.Tx.Balloon_Combo[1].Opacity = 192;
                            TJAPlayer4.Tx.Balloon_Number_Combo[1].Opacity = 192;
                        }
                        else if (this.ct進行[1].n現在の値 >= 4 && this.ct進行[1].n現在の値 <= 100)
                        {
                            TJAPlayer4.Tx.Balloon_Combo[1].Opacity = 255;
                            TJAPlayer4.Tx.Balloon_Number_Combo[1].Opacity = 255;
                        }
                        if (this.ct進行[1].b進行中)
                        {
                            TJAPlayer4.Tx.Balloon_Combo[1].t2D描画(TJAPlayer4.app.Device, TJAPlayer4.Skin.Game_Balloon_Combo_X[1], TJAPlayer4.Skin.Game_Balloon_Combo_Y[1]);
                            if (this.nCombo_渡[1] < 1000) //2016.08.23 kairera0467 仮実装。
                            {
                                this.t小文字表示2(TJAPlayer4.Skin.Game_Balloon_Combo_Number_X[1], TJAPlayer4.Skin.Game_Balloon_Combo_Number_Y[1], string.Format("{0,4:###0}", this.nCombo_渡[1]));
                                TJAPlayer4.Tx.Balloon_Number_Combo[1].t2D描画(TJAPlayer4.app.Device, TJAPlayer4.Skin.Game_Balloon_Combo_Text_X[1], TJAPlayer4.Skin.Game_Balloon_Combo_Text_Y[1], new Rectangle(0, 67, 78, 27));
                            }
                            else
                            {
                                this.t小文字表示2(TJAPlayer4.Skin.Game_Balloon_Combo_Number_Ex_X[1], TJAPlayer4.Skin.Game_Balloon_Combo_Number_Ex_Y[1], string.Format("{0,4:###0}", this.nCombo_渡[1]));
                                TJAPlayer4.Tx.Balloon_Number_Combo[1].t2D描画(TJAPlayer4.app.Device, TJAPlayer4.Skin.Game_Balloon_Combo_Text_Ex_X[1], TJAPlayer4.Skin.Game_Balloon_Combo_Text_Ex_Y[1], new Rectangle(0, 67, 78, 27));
                            }
                        }
                    }
                }
			}
			return 0;
		}
		

		// その他

		#region [ private ]
		//-----------------
        private CCounter[] ct進行 = new CCounter[ 2 ];
        //private CTexture[] tx吹き出し本体 = new CTexture[ 2 ];
        //private CTexture tx数字;
        private int[] nCombo_渡 = new int[ 2 ];

        [StructLayout(LayoutKind.Sequential)]
        private struct ST文字位置
        {
            public char ch;
            public Point pt;
            public ST文字位置( char ch, Point pt )
            {
                this.ch = ch;
                this.pt = pt;
            }
        }
        private ST文字位置[] st小文字位置 = new ST文字位置[]{
            new ST文字位置( '0', new Point( 0, 0 ) ),
            new ST文字位置( '1', new Point( 51, 0 ) ),
            new ST文字位置( '2', new Point( 102, 0 ) ),
            new ST文字位置( '3', new Point( 153, 0 ) ),
            new ST文字位置( '4', new Point( 204, 0 ) ),
            new ST文字位置( '5', new Point( 255, 0 ) ),
            new ST文字位置( '6', new Point( 306, 0 ) ),
            new ST文字位置( '7', new Point( 357, 0 ) ),
            new ST文字位置( '8', new Point( 408, 0 ) ),
            new ST文字位置( '9', new Point( 459, 0 ) )
        };

		private void t小文字表示1( int x, int y, string str )
		{
			foreach( char ch in str )
			{
				for( int i = 0; i < this.st小文字位置.Length; i++ )
				{
					if( this.st小文字位置[ i ].ch == ch )
					{
                        for (int j = 0; j < 2; j++)
                        {
                            Rectangle rectangle = new Rectangle(this.st小文字位置[i].pt.X, this.st小文字位置[i].pt.Y, 51, 67);
                            if (TJAPlayer4.Tx.Balloon_Number_Combo[0] != null)
                            {
                                TJAPlayer4.Tx.Balloon_Number_Combo[0].t2D描画(TJAPlayer4.app.Device, x, y, rectangle);
                            }
                        }
						break;
					}
				}
                x += 46;
			}
        }
        private void t小文字表示2(int x, int y, string str)
        {
            foreach (char ch in str)
            {
                for (int i = 0; i < this.st小文字位置.Length; i++)
                {
                    if (this.st小文字位置[i].ch == ch)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            Rectangle rectangle = new Rectangle(this.st小文字位置[i].pt.X, this.st小文字位置[i].pt.Y, 51, 67);
                            if (TJAPlayer4.Tx.Balloon_Number_Combo[1] != null)
                            {
                                TJAPlayer4.Tx.Balloon_Number_Combo[1].t2D描画(TJAPlayer4.app.Device, x, y, rectangle);
                            }
                        }
                        break;
                    }
                }
                x += 46;
            }
        }
        //-----------------
        #endregion
    }
}
