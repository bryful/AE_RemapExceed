
if ( typeof (FsJSON) !== "object"){//デバッグ時はコメントアウトする
	FsJSON = {};
}//デバッグ時はコメントアウトする

(function(me){
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
	
    var cellItem = [  ];

	var scriptName = File.decode($.fileName.getName().changeExt(""));
	var aeclipPath = File.decode($.fileName.getParent()+"/aeclip.exe");
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
	}	//-------------------------------------------------------------------------
	var fromClipboard = function()
	{
        var ret = "";
		var fclip = new File(aeclipPath);
        var temp = new File(Folder.temp.fullName +"/ae_temp.json");
		var cmd =  "\"" + fclip.fsName +"\"";
        cmd += " /o ";
        cmd += "\"" + temp.fsName + "\""; 
		if (fclip.exists==true){
			try{
				system.callSystem(cmd);
                if (temp.exists) {
                    temp.encoding = "utf-8";
                    if (temp.open("r")){
                        try{
                        ret = temp.read();
                        temp.remove();
                        }catch(e){
                            alert("readError!");
                            return ret;
                        }finally{
                            temp.close();
                        }
                    }
                }else{
				alert("fromClipbord\r\n" + temp.fullName);
                }

			}catch(e){
				alert("fromClipbord\r\n" + e.toString());
			}
		}
        return ret;
    }
 	//-------------------------------------------------------------------------
    var cellData = null;
    var rbtns = [];
    var selectedIndex = -1;
 	//-------------------------------------------------------------------------
	var winObj = ( me instanceof Panel) ? me : new Window("palette", "AE_RemapExceed", [ 0,  0,  240,  180]  ,{resizeable:true, maximizeButton:true, minimizeButton:true});
	//-------------------------------------------------------------------------
	var stCaption = winObj.add("statictext", [  10,   10,   10+ 220,   10+  20], "AE_Remap Exceed");
	var btnGetClip = winObj.add("button", [  10,   40,   10+  60,   40+  25], "獲得" );
	var btnClear = winObj.add("button", [  10,   70,   10+  60,   70+  25], "Clear");
	var btnApply = winObj.add("button", [  10,   150,   10+  60,   150+  25], "適応" ); 
	var edInfo = winObj.add("edittext", [  80,   40,   80+ 150,   40+  25], "", { readonly:true });
	var stSelected = winObj.add("statictext", [  80,   70,   80+ 220,   70+  20], "");
	var gp = winObj.add("panel", [  80,   95,   80+ 150,   95+ 100],"Cell" );
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
    btnClear.onClick=function()
    {

        edInfo.text = "";
        stSelected.text = "";
        clearRbtns();
        selectedIndex = -1;
        cellData = null;
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
                    stSelected.text = this.idx + ": " + cellData.caption[this.idx] + "が選ばれた";
                }
               rbtns.push(p);
               y+=23;
            }
        }
    }

    //-------------------------------------------------------------------------
    var clickflg = false;
    btnGetClip.onClick = function(){
        if (clickflg==true) return;
        clickflg = true;
        try{
            edInfo.text = "";
            stSelected.text = "";
            clearRbtns();
            var code =  fromClipboard().trim();
            if ((code=="")||(code[0]!="{")) {
                alert("textError!");
                return;
            }
            var obj = FsJSON.parse(code);
            if((obj.header =="ardjV2")) {
                edInfo.text = obj.sheetName;
                makeRbtn(obj.caption);
                cellData =  obj;
            }
            }catch(e){
            alert(e.toString);
        }finally{
            clickflg = false;
        }
    };
	//-------------------------------------------------------------------------
    var applyCells = function()
    {
        var applySub = function(lyr,times,values,maxV)
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
                rp.setValuesAtTimes(times,values);
		        for (var i=1 ; i<=rp.numKeys ; i++) {
                    rp.setInterpolationTypeAtKey(i,KeyframeInterpolationType.HOLD,KeyframeInterpolationType.HOLD);
                }

                //からセルの処理
                var t = [];
                var v = [];
                for (var i=0; i<values.length;i++){
                    t.push(times[i]);
                    if(values[i]>=maxV){
                        v.push(0);
                    }else{
                        v.push(100);
                    }
                }
                var t2 = [];
                var v2 = [];
                v2.push(v[0]);
                t2.push(t[0]);
                var ff = v[0]
                for (var i=1; i<v.length-1;i++)
                {
                    if (ff != v[i]){
                        v2.push(v[i]);
                        t2.push(t[i]);
                        ff = v[i];
                    }
                }
                var op =  lyr.property(6).property(11);
                op.setValuesAtTimes(t2,v2);
                for (var i=1 ; i<=op.numKeys ; i++) {
                    op.setInterpolationTypeAtKey(i,KeyframeInterpolationType.HOLD,KeyframeInterpolationType.HOLD);
                }
            }catch(e){
                alert(e.toString());
            }
        }
        if (cellData==null) return;
        if(selectedIndex<0){
            alert("セルを選んで");
            return;
        }
        var lyrs = getLayer();
       if (lyrs==null){
            return;
        }
        app.beginUndoGroup("AE_Remap");
        //コンポの長さを設定
        var cmp = lyrs[0].containingComp;
        var ddu = cellData.frameCount / cellData.frameRate;
        if (cmp.duration != ddu) cmp.duration = ddu; 

       for (var i=0; i<lyrs.length;i++)
        {
            var lyr = lyrs[i];

            var times = cellData.cells[selectedIndex][0];
            var values =[];
            var fr = cellData.frameRate;
            var rp = lyrs[i].property(2);
            var maxV = rp.maxValue;
            for (var j=0; j<cellData.cells[selectedIndex][1].length;j++)
            {
                var num = cellData.cells[selectedIndex][1][j];
                if (num<0) num = maxV;
                else if (num>maxV) num = maxV;
                 values.push(num);
            }
            if(times.length==values.length) {
                applySub(lyrs[i],times,values,maxV);
                lyrs[i].inPoint = 0;
                lyrs[i].outPoint = ddu;
            }
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
        infob[0] = 80;
        infob[1] = 40;
        infob[2] =  winb.width -10;
        infob[3] = 40 + 25;
        edInfo.bounds = infob;

    
        var gpb = gp.bounds;
        gpb[0] = 80;
        gpb[1] = 95;
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