// ***************************************************************************
/*
    AE_Remap Exceedをafter Effectsから制御するスクリプト

*/
// ***************************************************************************

//JSON関係
if ( typeof (FsJSON) !== "object"){//デバッグ時はコメントアウトする
	FsJSON = {};
}//デバッグ時はコメントアウトする

(function(me){
    //各種プロトタイプを設定
    String.prototype.trim = function(){
        if (this=="" ) return ""
        else return this.replace(/[\r\n]+$|^\s+|\s+$/g, "");
    }
    String.prototype.getParent = function(){
        var r=this;var i=this.lastIndexOf("/");if(i>=0) r=this.substring(0,i);
        return r;
    }
    //ファイル名のみ取り出す（拡張子付き）
    String.prototype.getName = function(){
        var r=this;var i=this.lastIndexOf("/");if(i>=0) r=this.substring(i+1);
        return r;
    }
    //拡張子のみを取り出す。
    String.prototype.getExt = function(){
        var r="";var i=this.lastIndexOf(".");if (i>=0) r=this.substring(i);
        return r;
    }
    //指定した書拡張子に変更（dotを必ず入れること）空文字を入れれば拡張子の消去。
    String.prototype.changeExt=function(s){
        var i=this.lastIndexOf(".");
        if(i>=0){return this.substring(0,i)+s;}else{return this; }
    }
    //文字の置換。（全ての一致した部分を置換）
    String.prototype.replaceAll=function(s,d){ return this.split(s).join(d);}

    FootageItem.prototype.nameTrue = function(){ var b=this.name;this.name=""; var ret=this.name;this.name=b;return ret;}

    String.prototype.replaceAll=function(s,d){ return this.split(s).join(d);}
	
    
    //グローバルな変数
	var scriptName = File.decode($.fileName.getName().changeExt(""));
	var aeremapCallPath = File.decode($.fileName.getParent()+"/AE_RemapCall.exe");
	
    //読み込む出るデータ
    var cellData = null;
    var cellDataV = null;
    //セル指定用のラジオボタン配列
    var rbtns = [];
    //選ばれたラジオボタン
    var selectedIndex = -1;
	//------------------------
	//-------------------------------------------------------------------------
    //json utils 
	function toJSON(obj)
	{
		var ret = "";
		if (typeof(obj) === "boolean"){
			ret = obj.toString();
		}else if (typeof(obj)=== "number"){
			ret = obj.toString();
		}else if (typeof(obj)=== "string"){
			ret = "\""+ obj +"\"";
		}else if ( obj instanceof Array){
			var r = "";
			for ( var i=0; i<obj.length;i++){
				if ( r !== "") r +=",";
				r += toJSON(obj[i]);
			}
			ret = "[" + r + "]";
		}else{
			for ( var a in obj)
			{
				if ( ret !=="") ret +=",";
				ret += "\""+a + "\":" + toJSON(obj[a]);
			}
			ret = "{" + ret + "}";
			
		}
		if ( (ret[0] === "(")&&(ret[ret.length-1]===")"))
		{
			ret = ret.substring(1,ret.length-1);
		}
		return ret;
	}
	if ( typeof(FsJSON.toJSON) !== "function"){
		FsJSON.toJSON = toJSON;
	}
	//------------------------
	function parse(s)
	{
		var ret = null;
		if ( typeof(s) !== "string") return ret;
		//前後の空白を削除
		s = s.replace(/[\r\n]+$|^\s+|\s+$/g, "");
		s = s.split("\r").join("").split("\n").join("");
		if ( s.length<=0) return ret;
		
		var ss = "";
		var idx1 =0;
		var len = s.length;
		while(idx1<len)
		{
			var idx2 = -1;
			if ( s[idx1]==="\""){
				var idx2 = s.indexOf("\"",idx1+1);
				if ((idx2>idx1)&&(idx2<s.length)){
					if ( s[idx2+1] !== ":") idx2 = -1;
				}
			}
			if ( idx2 ==-1) {
				ss += s[idx1];
				idx1++;
			}else{
				ss += s.substring(idx1+1,idx2)+":";
				idx1 = idx2+2;
			}
		}
		if ( ss[0]=="{"){
			ss ="("+ss+")";
		}
		try{
			ret = eval(ss);
		}catch(e){
			ret = null;
		}
		return ret;
	}
	if ( typeof(FsJSON.parse) !== "function"){
		FsJSON.parse = parse;
	}
	// ********************************************************************************
	var getActiveComp = function()
	{
		var ret = null;
		ret = app.project.activeItem;
		
		if ( (ret instanceof CompItem)===false)
		{
			ret = null;
			alert("コンポをアクティブにしてください！");
		}
		return ret;
	}
	// ********************************************************************************
    var getLayer = function(cmp)
	{
		var ret = null;
		if ( (cmp ==null)||(cmp ==undefined)||( (cmp instanceof CompItem)==false)) {
			var ac = getActiveComp();
			if (ac == null) return ret;
			cmp = ac;
		}
		var lyrs = cmp.selectedLayers;
		if(lyrs.length<=0){
            alert("レイヤを選んで")
        }else{
            ret = lyrs;
        }
		return ret;
	}	
    //-------------------------------------------------------------------------
    var anlysisCellData = function(obj)
    {
        var ret = {};
        var c = obj.cellCount;
        var f = obj.frameCount;
        var fr = obj.frameRate;
        ret.frameCount = obj.frameCount;
        ret.caption = [];
        ret.cell = [];
        ret.duration = f/fr;

        for ( var i=0; i<c; i++)
        {
            var cd = obj.cell[i];
            if ((cd.length==1)&&(cd[0][0]==0)&&(cd[0][1]==0)) continue;
            ret.caption.push(obj.caption[i]);

            var times = [];
            var values = [];
            for ( var j=0; j<cd.length;j++)
            {
                times.push(cd[j][0]/fr);
                values.push((cd[j][1]-1)/fr);
            }
            var ary = [];
            ary.push(times);
            ary.push(values);
            ret.cell.push(ary);
        }
         return ret;
    }
    //-------------------------------------------------------------------------
    var fontSet = function(prop,str)
    {
        var td = prop.value;
        td.resetCharStyle(); 
        td.fontSize = 30;
        td.fillColor = [1, 1, 1];
        td.font = "System"; 
        //if ((str!=undefined)||(str!=null)&&(str!="")) td.text = str;
        prop.setValue(td);
    }
     //-------------------------------------------------------------------------
    var newTD = function(str)
    {
        var td = new TextDocument(str);
        td.resetCharStyle(); 
        td.fontSize = 30;
        td.fillColor = [1, 1, 1];
        td.font = "System"; 
        return td;
    }
    //-------------------------------------------------------------------------
    var findComp = function(op)
    {
        function makeComp()
        {
            var ret = app.project.items.addComp(compName,1600,900,1,1,24);
            if(ret!=null) {

                var str ="このコンポはAE_Remap.jsxによって作られたものです。\r\n"
                +"テキストレイヤーにはシート情報が保存されています。\r\n"
                +"一番上にあるレイヤが読みこまれます。必要に応じて順番を変えてください。\r\n"
                var txt = ret.layers.addBoxText([1600,900],str);
                txt.name = "説明";
            }
            return ret;
        }
        var compName = "ae_remap_data";
        var ret = null;
        var cnt = app.project.numItems;
        if(cnt<=0) {
            if(op==true) {
                ret = makeComp();
            }
            return ret;
        }
        for ( var i=1; i<=cnt; i++)
        {
            var a = app.project.items[i];
            if (a instanceof CompItem)
            {
                if (a.name == compName){
                    ret = a;
                    break;
                }
            }
        }
        if (ret ==null){
            if(op==true) {
                ret = makeComp();
            }
        }
        return ret;
    }
    //-------------------------------------------------------------------------
    var objToComp = function()
    {
        if(cellData==null){
            alert("セル情報が読み込まれていません");
            return;
        }
        try{
            var cmp = findComp(true);
            var js = FsJSON.toJSON(cellData);
            var txt = cmp.layers.addBoxText([1600,900],js);

             txt.enabled = false;
            txt.name = cellData.sheetName;
            alert(cmp.name + "に保存しました");
        }catch(e){
            alert(e.toString());
        }
    }
     //-------------------------------------------------------------------------
     var compToObj = function()
     {
        function getText(lyr)
         {
             var ret = "";
             if ( (lyr instanceof TextLayer)==false) return ret;
             var t = lyr.property("ADBE Text Properties");
             var td = t.property("ADBE Text Document");
             return td.value.text;
         }
         var cmp = findComp(false);
        if ((cmp==null)||(cmp.numLayers<=0))
        {
            alert("記憶されたコンポジションがありません");
            return;
        }
        for (var i=1; i<=cmp.numLayers;i++)
        {
            var lyr = cmp.layer(i);
            var js = getText(lyr);
            if (js == "") continue;
            var obj = FsJSON.parse(js);
            if (obj.header =="ardjV2")
            {
                clearAll();
                edInfo.text = obj.sheetName;
                cellData =  obj;
                try{
                    cellDataV =anlysisCellData(obj);
                }catch(e){
                    alert("compToObj 01\r\n" + e.toString());
                }
                makeRbtn(cellDataV.caption);
                break;
            }
        }


     }
    //-------------------------------------------------------------------------
    //AEを起動させる
	var execAE_Reamp = function()
	{
        var ret = false;
		var aeremapCall = new File(aeremapCallPath);
		var cmd =  "\"" + aeremapCall.fsName +"\"";
		if (aeremapCall.exists==true){
			try{
                var r = system.callSystem(cmd + " /exenow");
                r = r.trim().toLowerCase();
                if (r=="false") {
                    r = system.callSystem(cmd + " /call");
                    if (r.indexOf("err")>=0){
                        alert(r);
                        ret = false;
                    }else{
                        ret = true;
                    }
                }else{
                    var r = system.callSystem(cmd + " /active");
                }

			}catch(e){
				alert("execAE_Reamp\r\n" + e.toString());
                ret = false;
			}
		}
        return ret;
    }
    //-------------------------------------------------------------------------
	var execAE_Export = function()
	{
        var ret = false;
		var aeremapCall = new File(aeremapCallPath);
        var cmd =  "\"" + aeremapCall.fsName +"\"";
        clearAll();
		if (aeremapCall.exists==true){
			try{
                var r = system.callSystem(cmd + " /export");
                if(r.indexOf("err")>=0) {
                    alert("01" +r)
                    return;
                }
                r = r.trim();
                if(r=="")
                {
                    alert("接続が切れました。\r\nAE_Remap Exceedを再起動させてください");
                    return;
                }
                var f = new File(r);
                if(f.exists==true){
                    try{
                        if(f.open("r"))
                        {
                            var str = f.read();
                            var obj = FsJSON.parse(str);

                            if((obj.header =="ardjV2")) {
                                edInfo.text = obj.sheetName;
                                cellData =  obj;
                                try{
                                    cellDataV =anlysisCellData(obj);
                                }catch(e){
                                    alert("execAE_Reamp 01\r\n" + e.toString());
                                }
                                makeRbtn(cellDataV.caption);
                            }
                        }
                    }finally{
                        f.close();
                    }
                }
                ret = true;

			}catch(e){
				alert("execAE_Reamp\r\n" + e.toString());
                ret = false;
			}
		}
        return ret;
    }
    
 	//-------------------------------------------------------------------------
 	//-------------------------------------------------------------------------
	var winObj = ( me instanceof Panel) ? me : new Window("palette", "AE_RemapExceed", [ 0,  0,  250,  220]  ,{resizeable:true, maximizeButton:true, minimizeButton:true});
	//-------------------------------------------------------------------------
	var px = 10;
    var py = 10;
    var btnW = 90;
    var btnH = 25;
    var stCaption = winObj.add("statictext", [  px, py, px+ 220, py+ 20], "AE_Remap Exceed");
    py =40;
	var btnExec_AERemap = winObj.add("button", [px,py,px+btnW, py+btnH], "AE_Remap起動" );
    py+=30;
	var btnGetClip = winObj.add("button", [px,py,px+btnW, py+btnH], "セル情報獲得" );
    py+=30;
	var btnToComp = winObj.add("button", [px,py,px+btnW, py+btnH], "保存");
    py+=30;
	var btnFromComp = winObj.add("button", [px,py,px+btnW, py+btnH], "読み込み");
    py+=30;
	var btnClear = winObj.add("button", [px,py,px+btnW, py+btnH], "Clear");
    py+=60;
    px = 110;
    py = 40;
	var edInfo = winObj.add("edittext", [  px,   py,   px+ 150,   py+  25], "", { readonly:true });
	py +=30;
	var btnApply = winObj.add("button", [px,py,px+150, py+25], "適応" ); 
    py +=25;
	var gp = winObj.add("panel", [  px,   py,   px+ 150,   py+ 100],"Cells" );
    //-------------------------------------------------------------------------
    btnExec_AERemap.onClick = execAE_Reamp;
    btnGetClip.onClick = execAE_Export;
    btnToComp.onClick = objToComp;
    btnFromComp.onClick = compToObj;
	//-------------------------------------------------------------------------
    var clearRbtns = function()
    {
        if (rbtns.length>0){
            for (var i=rbtns.length-1; i>=0;i--){
                rbtns[i].visible = false;
                delete rbtns[i];
                rbtns[i] = null;
                rbtns.pop();
            }
            rbtns = [];
        }
        selectedIndex=-1;
    }
	//-------------------------------------------------------------------------
    var clearAll  = function()
    {
        edInfo.text = "";
        clearRbtns();
        selectedIndex = -1;
        cellData = null;
        cellDataV = null;
    }
	//-------------------------------------------------------------------------
    btnClear.onClick=function()
    {
        clearAll();
    }
	//-------------------------------------------------------------------------
    var makeRbtn = function(ary)
    {
        clearRbtns();
        if (ary.length>0)
        {
            var x = 5;//85;
            var y = 5;//90;
            for (var i=0; i<ary.length;i++)
            {
                var p = gp.add("radiobutton", [  x,   y,   x+ 150,   y+ 20],ary[i] );
                p.idx = i;
                p.onClick=function(){
                    selectedIndex=this.idx;
                    btnApply.text = this.idx + ": " + cellData.caption[this.idx] + "を適応";
                }
               rbtns.push(p);
               y+=23;
            }
        }
    }

    //-------------------------------------------------------------------------
  
	//-------------------------------------------------------------------------
    var applyCells = function()
    {
        if ((cellDataV==null)||(cellData==null))
        {
            alert("セル情報が読み込まれていません");
            return;
        }
        // -----------------------------
        var findProp = function(pb,mn,na)
        {
            var ret = null;
            if (pb.numProperties>0)
            {
                for (var i=1; i<=pb.numProperties;i++)
                {
                    if ( (pb.property(i).matchName == mn)&&(pb.property(i).name == na))
                    {
                        ret = pb.property(i);
                        break;
                    }
                }
            }
            return ret;
        }
        // -----------------------------
        var applySub = function(lyr,times,values,emptys,emptyTimes)
        {
            if ( lyr.canSetTimeRemapEnabled == false) {
                return;
            }
            try{
                var rp = lyr.property(2);
                if (rp.numKeys>0) for ( var i=rp.numKeys; i>=1;i--) rp.removeKey(i);
                lyr.timeRemapEnabled = true;
 		        if (rp.numKeys>0) for ( var i=rp.numKeys; i>1;i--) rp.removeKey(i);
                lyr.startTime = 0;
                lyr.inPoint = 0;
                lyr.outPoint = lyr.containingComp.duration;
                rp.setValuesAtTimes(times,values);
		        for (var i=1 ; i<=rp.numKeys ; i++) {
                    rp.setInterpolationTypeAtKey(i,KeyframeInterpolationType.HOLD,KeyframeInterpolationType.HOLD);
                }
                var eg = lyr.property("ADBE Effect Parade");
                var mn = "ADBE Block Dissolve";
                var na = "EmptyCell";
                if (eg.canAddProperty(mn)==true)
                {
                    var bp = findProp(eg,mn,na); 
                    if (bp==null){
                        bp = eg.addProperty(mn);
                        bp.name = na;
                    }
                    var bpv = bp.property(1);
                    if (bpv.numKeys>0) for ( var i=bpv.numKeys; i>=1;i--) bpv.removeKey(i);
                    bpv.setValuesAtTimes(emptyTimes,emptys);
                    for (var i=1 ; i<=bpv.numKeys ; i++) {
                        bpv.setInterpolationTypeAtKey(i,KeyframeInterpolationType.HOLD,KeyframeInterpolationType.HOLD);
                    }
                }
                lyr.outPoint = lyr.containingComp.duration;


            }catch(e){
                alert(e.toString());
            }
        }
        if(selectedIndex<0){
            alert("セルを選んでください");
            return;
        }
        var lyrs = getLayer();
       if ((lyrs==null)||(lyrs.length<=0)){
            return;
        }
        app.beginUndoGroup("AE_Remap");
        
        //コンポの長さを設定
        var cmp = lyrs[0].containingComp;
        var duration = cellDataV.duration;
        if (cmp.duration != duration) cmp.duration = duration; 

       for (var i=0; i<lyrs.length;i++)
        {
            var lyr = lyrs[i];

            var times = [];
            var values = [];
            var emptys = [];
            var emptyTimes = [];
            var fr = cellDataV.frameRate;
            var rp = lyrs[i].property(2);
            var maxV = rp.maxValue;
            for (var j=0; j<cellDataV.cell[selectedIndex][1].length;j++)
            {
                var tim = cellDataV.cell[selectedIndex][0][j];
                var num = cellDataV.cell[selectedIndex][1][j];
                if (num<0) num = maxV;
                else if (num>maxV) num = maxV;
                times.push(tim);
                values.push(num);
                if(num>=maxV)
                {
                    emptys.push(100);
                }else{
                    emptys.push(0);
                }
                emptyTimes.push(tim);
            }
            var cnt = emptys.length;
            for (var j = cnt-1; j>=1;j--)
            {
                if (emptys[j-1] == emptys[j]){
                    emptys.splice(j,1);
                    emptyTimes.splice(j,1);
                }
            }

            applySub(lyrs[i],times,values,emptys,emptyTimes);
            lyrs[i].inPoint = 0;
            lyrs[i].outPoint = duration;
        }
        app.endUndoGroup();
    }
    btnApply.onClick = applyCells;

	//-------------------------------------------------------------------------
    var resizeLayout = function()
    {
        //edInfo.visible = false;
        //lbCells.visible = false;
        var winb = winObj.bounds;

        //ウィンドウの大きさを求める
        var w = winb[2] - winb[0];
        var h = winb[3] - winb[1];

        var infob = edInfo.bounds;
        //横方向のみ
        infob[0] = 110;
        infob[1] = 40;
        infob[2] =  winb.width -10;
        infob[3] = 40 + 25;
        edInfo.bounds = infob;

        var applyb = btnApply.bounds;
        //横方向のみ
        applyb[0] = 110;
        applyb[1] = 70;
        applyb[2] =  winb.width -10;
        applyb[3] = 70 + 25;
        btnApply.bounds = applyb;

    
        var gpb = gp.bounds;
        gpb[0] = 110;
        gpb[1] = 100;
        gpb[2] = winb.width -10;
        gpb[3] = winb.height -10;
        gp.bounds = gpb;
        //edInfo.visible = true;
        //lbCells.visible = true;
        //winObj.text = winb.toString() +"/" + edInfo.bounds.toString();
        
    }
    resizeLayout();
    winObj.onResize = resizeLayout;
    //-------------------------------------------------------------------------
	if ( ( me instanceof Panel) == false){
		winObj.center(); 
		winObj.show();
	}
	//-------------------------------------------------------------------------

})(this);