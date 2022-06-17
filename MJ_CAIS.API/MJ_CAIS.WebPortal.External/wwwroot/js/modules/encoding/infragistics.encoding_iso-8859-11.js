/*!@license
* Infragistics.Web.ClientUI infragistics.encoding_iso-8859-11.js 21.1.20211.72
*
* Copyright (c) 2011-2021 Infragistics Inc.
*
* http://www.infragistics.com/
*
* Depends:
*     jquery-1.4.4.js
*     jquery.ui.core.js
*     jquery.ui.widget.js
*     infragistics.util.js
*     infragistics.ext_core.js
*     infragistics.ext_collections.js
*     infragistics.ext_text.js
*     infragistics.encoding.core.js
*/
(function(factory){if(typeof define==="function"&&define.amd){define(["./infragistics.util","./infragistics.ext_core","./infragistics.ext_collections","./infragistics.ext_text","./infragistics.encoding.core"],factory)}else{factory(igRoot)}})(function($){$.ig=$.ig||{};var $$t={};$.ig.globalDefs=$.ig.globalDefs||{};$.ig.globalDefs.$$bd=$$t;$.ig.$currDefinitions=$$t;$.ig.util.bulkDefine(["Iso8859Dash11:a","SingleByteEncoding:b","Encoding:c","Object:d","Type:e","Boolean:f","ValueType:g","Void:h","IConvertible:i","IFormatProvider:j","Number:k","String:l","IComparable:m","Number:n","IComparable$1:o","IEquatable$1:p","Number:q","Number:r","Number:s","NumberStyles:t","Enum:u","Array:v","IList:w","ICollection:x","IEnumerable:y","IEnumerator:z","Error:aa","Error:ab","Number:ac","String:ad","StringComparison:ae","RegExp:af","CultureInfo:ag","DateTimeFormat:ah","Calendar:ai","Date:aj","Number:ak","DayOfWeek:al","DateTimeKind:am","CalendarWeekRule:an","NumberFormatInfo:ao","CompareInfo:ap","CompareOptions:aq","IEnumerable$1:ar","IEnumerator$1:as","IDisposable:at","StringSplitOptions:au","Number:av","Number:aw","Number:ax","Number:ay","Number:az","Number:a0","Assembly:a1","Stream:a2","SeekOrigin:a3","RuntimeTypeHandle:a4","MethodInfo:a5","MethodBase:a6","MemberInfo:a7","ParameterInfo:a8","TypeCode:a9","ConstructorInfo:ba","PropertyInfo:bb","UTF8Encoding:bc","InvalidOperationException:bd","NotImplementedException:be","Script:bf","Decoder:bg","UnicodeEncoding:bh","Math:bi","AsciiEncoding:bj","ArgumentNullException:bk","DefaultDecoder:bl","ArgumentException:bm","IEncoding:bn","Dictionary$2:bo","IDictionary$2:bp","ICollection$1:bq","KeyValuePair$2:br","IDictionary:bs","IEqualityComparer$1:bt","EqualityComparer$1:bu","IEqualityComparer:bv","DefaultEqualityComparer$1:bw","Thread:bx","ThreadStart:by","MulticastDelegate:bz","IntPtr:b0","StringBuilder:b1","Environment:b2","RuntimeHelpers:b3","RuntimeFieldHandle:b4","AbstractEnumerable:b5","Func$1:b6","AbstractEnumerator:b7","GenericEnumerable$1:b8","GenericEnumerator$1:b9"]);var $a=$.ig.intDivide,$b=$.ig.util.cast,$c=$.ig.util.defType,$d=$.ig.util.defEnum,$e=$.ig.util.getBoxIfEnum,$f=$.ig.util.getDefaultValue,$g=$.ig.util.getEnumValue,$h=$.ig.util.getValue,$i=$.ig.util.intSToU,$j=$.ig.util.nullableEquals,$k=$.ig.util.nullableIsNull,$l=$.ig.util.nullableNotEquals,$m=$.ig.util.toNullable,$n=$.ig.util.toString$1,$o=$.ig.util.u32BitwiseAnd,$p=$.ig.util.u32BitwiseOr,$q=$.ig.util.u32BitwiseXor,$r=$.ig.util.u32LS,$s=$.ig.util.unwrapNullable,$t=$.ig.util.wrapNullable,$u=String.fromCharCode,$v=$.ig.util.castObjTo$t;$c("Iso8859Dash11:a","SingleByteEncoding",{ai:null,ac:function(){return this.ai},init:function(){this.ai=["\0","\x01","\x02","\x03","\x04","\x05","\x06","\x07","\b","\t","\n","\x0B","\f","\r","\x0e","\x0f","\x10","\x11","\x12","\x13","\x14","\x15","\x16","\x17","\x18","\x19","\x1a","\x1b","\x1c","\x1d","\x1e","\x1f"," ","!",'"',"#","$","%","&","'","(",")","*","+",",","-",".","/","0","1","2","3","4","5","6","7","8","9",":",";","<","=",">","?","@","A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z","[","\\","]","^","_","`","a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","{","|","}","~","\x7f","\u20ac","\x81","\x82","\x83","\x84","\u2026","\x86","\x87","\x88","\x89","\x8a","\x8b","\x8c","\x8d","\x8e","\x8f","\x90","\u2018","\u2019","\u201c","\u201d","\u2022","\u2013","\u2014","\x98","\x99","\x9a","\x9b","\x9c","\x9d","\x9e","\x9f","\xa0","\u0e01","\u0e02","\u0e03","\u0e04","\u0e05","\u0e06","\u0e07","\u0e08","\u0e09","\u0e0a","\u0e0b","\u0e0c","\u0e0d","\u0e0e","\u0e0f","\u0e10","\u0e11","\u0e12","\u0e13","\u0e14","\u0e15","\u0e16","\u0e17","\u0e18","\u0e19","\u0e1a","\u0e1b","\u0e1c","\u0e1d","\u0e1e","\u0e1f","\u0e20","\u0e21","\u0e22","\u0e23","\u0e24","\u0e25","\u0e26","\u0e27","\u0e28","\u0e29","\u0e2a","\u0e2b","\u0e2c","\u0e2d","\u0e2e","\u0e2f","\u0e30","\u0e31","\u0e32","\u0e33","\u0e34","\u0e35","\u0e36","\u0e37","\u0e38","\u0e39","\u0e3a","\uf8c1","\uf8c2","\uf8c3","\uf8c4","\u0e3f","\u0e40","\u0e41","\u0e42","\u0e43","\u0e44","\u0e45","\u0e46","\u0e47","\u0e48","\u0e49","\u0e4a","\u0e4b","\u0e4c","\u0e4d","\u0e4e","\u0e4f","\u0e50","\u0e51","\u0e52","\u0e53","\u0e54","\u0e55","\u0e56","\u0e57","\u0e58","\u0e59","\u0e5a","\u0e5b","\uf8c5","\uf8c6","\uf8c7","\uf8c8"];$$t.$b.init1.call(this,1,874,"iso-8859-11")},$type:new $.ig.Type("Iso8859Dash11",$$t.$b.$type)},true)});