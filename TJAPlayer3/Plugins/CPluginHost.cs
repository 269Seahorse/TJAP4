using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;
using FDK;

namespace TJAPlayer4
{
	internal class CPluginHost : IPluginHost
	{
		// コンストラクタ

		public CPluginHost()
		{
			this._DTXManiaVersion = new CDTXVersion( TJAPlayer4.VERSION );
		}


		// IPluginHost 実装

		public CDTXVersion DTXManiaVersion
		{
			get { return this._DTXManiaVersion; }
		}
		public Device D3D9Device
		{
			get { return (TJAPlayer4.app != null ) ? TJAPlayer4.app.Device.UnderlyingDevice : null; }
		}
		public Format TextureFormat
		{
			get { return TJAPlayer4.TextureFormat; }
		}
		public CTimer Timer
		{
			get { return TJAPlayer4.Timer; }
		}
		public CSound管理 Sound管理
		{
			get { return TJAPlayer4.Sound管理; }
		}
		public Size ClientSize
		{
			get { return TJAPlayer4.app.Window.ClientSize; }
		}
		public CStage.Eステージ e現在のステージ
		{
			get { return ( TJAPlayer4.r現在のステージ != null ) ? TJAPlayer4.r現在のステージ.eステージID : CStage.Eステージ.何もしない; }
		}
		public CStage.Eフェーズ e現在のフェーズ
		{
			get { return ( TJAPlayer4.r現在のステージ != null ) ? TJAPlayer4.r現在のステージ.eフェーズID : CStage.Eフェーズ.共通_通常状態; }
		}
		public bool t入力を占有する(IPluginActivity act)
		{
			if (TJAPlayer4.act現在入力を占有中のプラグイン != null)
				return false;

			TJAPlayer4.act現在入力を占有中のプラグイン = act;
			return true;
		}
		public bool t入力の占有を解除する(IPluginActivity act)
		{
			if (TJAPlayer4.act現在入力を占有中のプラグイン == null || TJAPlayer4.act現在入力を占有中のプラグイン != act)
				return false;

			TJAPlayer4.act現在入力を占有中のプラグイン = null;
			return true;
		}
		public void tシステムサウンドを再生する( Eシステムサウンド sound )
		{
			if( TJAPlayer4.Skin != null )
				TJAPlayer4.Skin[ sound ].t再生する();
		}
		
		
		// その他

		#region [ private ]
		//-----------------
		private CDTXVersion _DTXManiaVersion;
		//-----------------
		#endregion
	}
}
