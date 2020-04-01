# AE_Remap_Exceed
For After Effects Timeremap support  
アニメ撮影でコマ打ちを行うアプリです。昔作ったやつですが、Windows10とCC2020の組み合わせでで動かなくなってしまったので、作り直しています。  
 
今はとりあえず対応させただけなものです。   
[AE_RemapExceed2.zip](https://github.com/bryful/AE_RemapExceed/raw/master/%E9%85%8D%E5%B8%83%E7%94%A8%E3%82%A2%E3%83%BC%E3%82%AB%E3%82%A4%E3%83%96/AE_RemapExceed2.zip)  

# Usage
* スクリプトのインストール  
C:\Users\(User Nmae)\AppData\Roaming\Adobe\After Effects\17.0\Scripts\ScriptUI Panels  
へスクリプトのAE_RemapExceed.jsxとaeclip.exeをコピーしてください。  
実行ファイルのAE_RemapExceed.exeはどこか適当なところへ  
* シートを打つ　　
AE_RemapExceed.exeで適当にシートを打ちます。  
保存は今のところ昔通りですが、改定する予定です（まぁ昔のも読めるようにします）  
* クリップボードへ  
レイヤメニューの「セルデータをクリップボードへ」を実行します。  
今回からすべてのセルレイヤをコピーしています。
* スクリプト  
AE_RemapExceed.jsxを実行させ、獲得ボタンを押すとセルデータが読み込まれます。  
ラジオボタンでセルレイヤを選び、適応でコマ打ちを反映させます。  

# 未完成部分
* セーブデータをjson形式に。元のardファイルも読み書きできるように
* cellRemapのardjファイルにも対応
* stsやりまぴんのファイルも読み書き可能に
* xmpメタファイルにも対応（スクリプト）
* 使わない機能の削除


# Dependency
Visual studio 2017 C#


# Setup


# License
This software is released under the MIT License, see LICENSE

# Authors

bry-ful(Hiroshi Furuhashi) http://bryful.yuzu.bz/  
twitter:[bryful](https://twitter.com/bryful)  
bryful@gmail.com  

# References

