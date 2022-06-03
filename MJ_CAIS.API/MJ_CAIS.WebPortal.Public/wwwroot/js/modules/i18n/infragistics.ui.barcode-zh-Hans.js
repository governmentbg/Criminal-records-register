/*!@license
* Infragistics.Web.ClientUI Barcode localization resources 21.1.11
*
* Copyright (c) 2011-2021 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/
(function(factory){if(typeof define==="function"&&define.amd){define(["jquery"],factory)}else{return factory(jQuery)}})(function($){$.ig=$.ig||{};$.ig.locale=$.ig.locale||{};$.ig.locale["zh-Hans"]=$.ig.locale["zh-Hans"]||{};$.ig.Barcode=$.ig.Barcode||{};$.ig.locale["zh-Hans"].Barcode={aILength:"AI 必须至少有 2 位数字。",badFormedUCCValue:"UCC 条形码的数据属性值格式不正确。它看起来应该像 (AI)GTIN。",code39_NonNumericError:"字符 '{0}' 对于 CODE39 数据属性值无效。有效值为: {1}",countryError:"转换国家/地区属性值代码时出错。它应该是一个数值。",emptyValueMsg:"数据属性值为空。",encodingError:"转换错误。有关有效属性值，请参阅文档。",errorMessageText:"无效值！请参考文档，了解有效的条形码数据属性值结构。",gS1ExMaxAlphanumNumber:"GS1 DataBar 扩展系列最多可编码 41 个字母数字字符。",gS1ExMaxNumericNumber:"GS1 DataBar 扩展系列最多可编码 74 个数字字符。",gS1Length:"GS1 DataBar Data 属性值用于 GTIN -8，12，13，14，其长度应为 7，11，12 或 13。最后一位保留用于校验和。",gS1LimitedFirstChar:"GS1 DataBar Limited 条形码的第一位数应为 0 或 1。当对指标值大于 1 的 GTIN-14 数据进行编码时，必须使用全向，堆叠，堆叠全向或截断条形码类型。",i25Length:"Interleaved2of5 条码的位数应为偶数。如果它们是奇数，则可以在其前面放置 0。",intelligentMailLength:"Intelligent Mail 条码数据属性值的长度应为 20，25，29 或 31 个字符，跟踪代码为 20 个数字 (2 个用于条码标识符，3 个用于服务类型标识符，6 或 9 个用于邮件标识符，9 或 6 个用于序列号) 以及 0，5，9 或 11 个邮政编码符号。",intelligentMailSecondDigit:"第二位数字应在 0-4 范围内。",invalidAI:"无效的应用程序标识符元素字符串。请确保数据属性值中的 AI 字符串格式正确。",invalidCharacter:"字符 '{0}' 不适用于当前条形码类型。有效值为: {1}",invalidDimension:"由于 Stretch，BarsFillMode 和 Xdimension 属性值的组合不正确，无法确定条形码尺寸。",invalidHeight:"此条码网格行的数量 ({0}) 不适合这样的高度 ({1} 像素)。",invalidLength:"条形码数据属性值应为 {0} 位。",invalidPostalCode:"无效的邮递区号值-模式 2 最多可编码 9 位邮政编码 (美国邮政编码)，而模式 3 最多可编码 6 个字符的字母数字代码。",invalidPropertyValue:"{0} 属性值应在 {1}-{2} 范围内。",invalidVersion:"SizeVersion 属性值编号未生成足够的单元格，无法使用当前编码模式和纠错级别对数据进行编码。",invalidWidth:"此条码网格列的数量 ({0}) 不适合这样的宽度 ({1} 像素)。检查 XDimension 或/和 WidthToHeightRatio 属性值。",invalidXDimensionValue:"当前条形码类型的 XDimension 属性值应在 {0} 至 {1} 的范围内。",maxLength:"文本的长度 {0} 超过当前条形码类型的最大可编码范围。最多可编码 {1} 个字符。",notSupportedEncoding:"不支持 {0} {1} 下的编码。",pDF417InvalidRowsColumnsCombination:"代码字 (数据和纠错) 的数量超过可以用矩阵 {0}x{1} 编码的符号。",primaryMessageError:"无法从数据属性值中提取主要消息。请参阅文档了解其结构。",serviceClassError:"转换服务类别时出错。它应该是一个数值。",smallSize:"无法使用已定义的拉伸设置按照 Size({0}, {1}) 调整网格。",unencodableCharacter:"字符 '{0}' 无法编码。",uPCEFirstDigit:"根据规范，第一个 UPCE 数字应始终为零。",warningString:"条形码警告: ",wrongCompactionMode:"无法使用 {0} 模式压缩 Data 属性值。",notLoadedEncoding:"未加载 {0} 编码。"};$.ig.Barcode.locale=$.ig.Barcode.locale||$.ig.locale["zh-Hans"].Barcode;return $.ig.locale["zh-Hans"].Barcode});