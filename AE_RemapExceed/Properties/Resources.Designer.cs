﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace AE_RemapExceed.Properties {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AE_RemapExceed.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   すべてについて、現在のスレッドの CurrentUICulture プロパティをオーバーライドします
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   AE_Remap Exceed に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string AppName {
            get {
                return ResourceManager.GetString("AppName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   //JavaScript
        /////******************************************************************
        /////最初の行は&quot;//JavaScript&quot;としてヘッダとする。
        /////クリップボードから読み込んだ時の識別用。
        /////******************************************************************
        ///var &lt;RX&gt; = new Object;
        /////******************************************************************
        /////変数の定義
        ///&lt;RX&gt;.enabled	= false;
        ///&lt;RX&gt;.frameCount	= &lt;frameCount&gt;;	//フレーム数
        ///&lt;RX&gt;.frameRate	= &lt;frameRate&gt;;	//フレームレート
        ///&lt;RX&gt;.caption	= &lt;caption&gt;;
        ///&lt;RX&gt;.cellData	= new Array;	//セル番号
        ////*
        ///	object.frame フレーム番号（0スタート） [残りの文字列は切り詰められました]&quot;; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string ScriptLayer {
            get {
                return ResourceManager.GetString("ScriptLayer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   //JavaScript
        /////最初の行は&quot;//JavaScript&quot;としてヘッダとする。
        /////クリップボードから読み込んだ時の識別用。
        /////---------------------------------------------------------------
        ///var &lt;RX&gt; = new Object;
        /////---------------------------------------------------------------
        /////変数の定義
        ///&lt;RX&gt;.enabled	= true;
        ///&lt;RX&gt;.cellIndex	= &lt;cellIndex&gt;;	//選択されているレイヤ
        ///&lt;RX&gt;.cellCount	= &lt;cellCount&gt;;	//セルレイヤ数
        ///&lt;RX&gt;.frameCount	= &lt;frameCount&gt;;	//フレーム数
        ///&lt;RX&gt;.frameRate	= &lt;frameRate&gt;;	//フレームレート
        ///
        ///&lt;RX&gt;.cellCaption	= [&lt;RX&gt;.cellCount];	//セル名の配列
        ///&lt;RX&gt;.cellData		= [&lt;RX&gt;.cellCount];	// [残りの文字列は切り詰められました]&quot;; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string ScriptLayerAll {
            get {
                return ResourceManager.GetString("ScriptLayerAll", resourceCulture);
            }
        }
        
        /// <summary>
        ///   V2.00 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string VersionStr {
            get {
                return ResourceManager.GetString("VersionStr", resourceCulture);
            }
        }
    }
}
