# AE_Remap_Exceed
For After Effects Timeremap support<br>
アニメ撮影でコマ打ちを行うアプリです。昔作ったやつですが、Windows10とCC2020の組み合わせでで動かなくなってしまったので、作り直しています。<br>
 <br>
今はとりあえず最低限の事を実装した段階ですが、いろいろ差し迫った事情があってリリースします。
<br>
空セルがブロックディゾルブ止めてほしいという要望あったので対処しました。<br>
チェックボックスで使用・不使用を選べます。<br>
もともと空セル部分のリマップは最大値を使用しているので、ブロックディゾルブ（EmptyCell）を無効化しても大丈夫です。<br>
色々あって併用していました。<br>
<b>結局ブロックディゾルブを標準で使わないようにしました。</b>
<br>



バグフィックス<br>

* パネルからardjを保存できるボタンを増やした。
* 保存、読み込みがコンポに行う事を忘れていたのでわかりやすく表示を変えた。<br>保存時にテキストレイヤがデフォルトなので、標準以外のフォントを指定してると注意です。
* 空セルでブロックディゾルブを使わないようにした。

***

* helpキーを押すとハングするのに対処
* たまに獲得出来なくなることに対処。（まだ不完全）
* レイヤのクリア・全クリア機能の追加


Download<br>

[AE_RemapExceed2nd_04.zip](https://bit.ly/3DVWMVV)

# Usage
* スクリプトのインストール
C:\Users\(User Nmae)\AppData\Roaming\Adobe\After Effects\17.0\Scripts\ScriptUI Panels
へスクリプトのAE_RemapExceed.jsxとAE_RemapExceed.exeとAE_RemapCall.exeの三つをコピーしてください。

CS6の場合は、
C:\Program Files\Adobe\Adobe After Effects CS6\Support Files\Scripts\ScriptUI Panels
にコピーしてください。


* AE_RemapExceed.jsx
After Effectsを起動して「ウィンドウ」メニューからAE_RemapExceed.jsxを選んでください。
パネルが開くので適当なところに配置してください。
![ScreenClip00](https://user-images.githubusercontent.com/50650451/78471423-23dbc480-776c-11ea-9d6f-cc1dc2278630.png)

*  AE_RemapExceedの起動
「AE_Remap起動」ボタンでAE_RemapExceedを起動します。
![ScreenClip2](https://user-images.githubusercontent.com/50650451/78471879-72d72900-776f-11ea-828e-3dd80b932b06.png)
適当にシートを打ちます。
打ち終わったら、シートデータは保存しておきます。

* セル情報の獲得
「セル情報獲得」ボタンでAE_Remapで打ち込んだ情報がAEに取り込まれます。
シート打ちされたセルレイヤのみがラジオボタンに表示されます。
![ScreenClip01](https://user-images.githubusercontent.com/50650451/78471543-02c7a380-776d-11ea-972d-b7792e87ca0e.png)

ラジオボタンでセルレイヤを選び、適応させたいレイヤを選択して 「適応」ボタンで反映できます。

* 保存
AE_Remap事態でもデータの保存できますが、「保存」ボタンでプロジェクト内に"ae_ramap_data"というコンポジションを作りそこにも保存できます。
ボタンを押すたびに追加されていきますので、適当に整理が必要です。
* 読み込み
「読み込み」ボタンは、一番最初に見つけた"ae_ramap_data"コンポジションの中で一番上にあるテキストレイヤに保存されているデータを読み込みます。



# 未完成部分
* AE_RemapCallの/export_layerと/import_layerをAE_RemapExceedに対応させる
* stsやりまぴんのファイルも読み書き可能に
* xmpメタファイルにも対応（スクリプト）☆cellRemapによって保存されているデータの読み込み
* バグ修正

# Dependency
Visual studio 2017 C#


# License
This software is released under the MIT License, see LICENSE

# Authors

bry-ful(Hiroshi Furuhashi) http://bryful.yuzu.bz/
twitter:[bryful](https://twitter.com/bryful)
bryful@gmail.com

# References

