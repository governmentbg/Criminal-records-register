/*!@license
* Infragistics.Web.ClientUI Grid localization resources 21.1.11
*
* Copyright (c) 2011-2021 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/
(function(factory){if(typeof define==="function"&&define.amd){define(["jquery"],factory)}else{return factory(jQuery)}})(function($){$.ig=$.ig||{};$.ig.locale=$.ig.locale||{};$.ig.locale["zh-Hant"]=$.ig.locale["zh-Hant"]||{};$.ig.Grid=$.ig.Grid||{};$.ig.locale["zh-Hant"].Grid={noSuchWidget:"{featureName} 無法識別。確認存在此功能且拼寫正確。",autoGenerateColumnsNoRecords:"autoGenerateColumns 已啟用，但資料來源中沒有記錄。加載帶有記錄的資料來源，以便確定列。",optionChangeNotSupported:"初始化後無法編輯 {optionName}。其值應在初始化期間設定。",optionChangeNotScrollingGrid:"初始化後無法編輯 {optionName}，因為您的網格最初不捲動且需要全部重新轉譯。此選項應在初始化期間設定。",widthChangeFromPixelsToPercentagesNotSupported:"無法將網格的選項寬度從像素動態更改為百分比。",widthChangeFromPercentagesToPixelsNotSupported:"無法動態更改網格的選項寬度，從百分比到像素。",noPrimaryKeyDefined:"沒有為網格定義主鍵。定義主鍵以使用諸如網格編輯之類的功能。",indexOutOfRange:"指定的行索引超出範圍。應提供介於 0 和 {max} 之間的行索引。",noSuchColumnDefined:"指定的列鍵無效。應提供與已定義網格列之一的鍵相匹配的列鍵。",columnIndexOutOfRange:"指定的列索引超出範圍。列索引應在 0 和 {max} 之間。",recordNotFound:"在資料檢視中找不到 ID 為 {id} 的記錄。驗證用於搜索的 ID，並在必要時進行調整。",columnNotFound:"找不到包含鍵 {key} 的列。驗證用於搜索的關鍵字，並在必要時進行調整。",colPrefix:"資料行 ",columnVirtualizationRequiresWidth:"虛擬化和列虛擬化要求設定網格或其列的寬度。提供網格寬度，defaultColumnWidth 或每列寬度的值。",virtualizationRequiresHeight:"虛擬化要求設定網格的高度。應提供網格高度的值。",colVirtualizationDenied:"columnVirtualization 需要不同的 virtualizationMode 設定。虛擬化模式應設定為 'fixed'。",noColumnsButAutoGenerateTrue:"autoGenerateColumns 已停用，且未為網格定義任何列。啟用 autoGenerateColumns 或手動指定列。",expandTooltip:"展開行",collapseTooltip:"折疊行",movingNotAllowedOrIncompatible:"指定的列無法移動。驗證是否存在這樣的列，並且其結束位置不會破壞列的佈局。",allColumnsHiddenOnInitialization:"初始化期間無法隱藏所有列。至少一列應設定為可見。",virtualizationNotSupportedWithAutoSizeCols:"虛擬化要求的列寬設定不同於 '*'。列寬應設定為以像素為單位的數字。",columnVirtualizationNotSupportedWithPercentageWidth:"列虛擬化需要不同的網格寬度設定。列寬應設定為以像素為單位的數字。",mixedWidthsNotSupported:"要求所有列的寬度設定都相同。設定所有列寬為百分比或以像素為單位的數字。",multiRowLayoutColumnError:"無法將鍵為 {key1} 的列添加到多行版面設定中，因為鍵為 {key2} 的列已佔據了它在版面中的位置。",multiRowLayoutNotComplete:"多行佈局不完整。列定義創建的版面具有空白且無法正確轉譯。",multiRowLayoutMixedWidths:"多行版面設定不支援混合寬度 (百分比和像素)。請以像素或百分比定義所有列寬。 ",multiRowLayoutHidingNotSupported:"多行佈局不支援隱藏列。請從列定義中移除隱藏的列。",scrollableGridAreaNotVisible:"固定的頁眉和頁腳區域大於可用的網格高度。可捲動區域不可見。請設定較大的網格高度。",featureNotSupportedWithMRL:"多行佈局不支援 {featureName}。請從功能清單中移除該功能。",editorTypeCannotBeDetermined:"正在更新，沒有足夠的資訊來正確確定要用於列的編輯器類型: "};$.ig.HierarchicalGrid=$.ig.HierarchicalGrid||{};$.ig.locale["zh-Hant"].HierarchicalGrid={noPrimaryKey:"igHierarchicalGrid 需要定義主鍵。應該提供一個主鍵。",expandTooltip:"展開行",collapseTooltip:"折疊行"};$.ig.GridFeatureChooser=$.ig.GridFeatureChooser||{};$.ig.locale["zh-Hant"].GridFeatureChooser={featureChooserTooltip:"功能選擇器"};$.ig.GridFiltering=$.ig.GridFiltering||{};$.ig.locale["zh-Hant"].GridFiltering={optionChangeNotSupported:"初始化後無法編輯 {optionName}。其值應在初始化期間設定。",startsWithNullText:"開始於...",endsWithNullText:"結束於...",containsNullText:"包含...",doesNotContainNullText:"不包含...",equalsNullText:"等於...",doesNotEqualNullText:"不等於...",greaterThanNullText:"大於...",lessThanNullText:"小於...",greaterThanOrEqualToNullText:"大於或等於...",lessThanOrEqualToNullText:"小於或等於...",onNullText:"在… (日期)",notOnNullText:"未開啟...",afterNullText:"之後",beforeNullText:"之前",emptyNullText:"空白",notEmptyNullText:"非空",nullNullText:"空",notNullNullText:"不為空",emptyLabel:"空白",notEmptyLabel:"非空",nullLabel:"空",notNullLabel:"不為空",startsWithLabel:"開始於",endsWithLabel:"結束於",containsLabel:"包含",doesNotContainLabel:"不包含",equalsLabel:"等於",doesNotEqualLabel:"不等於",greaterThanLabel:"大於",lessThanLabel:"小於",greaterThanOrEqualToLabel:"大於或等於",lessThanOrEqualToLabel:"小於或等於",trueLabel:"True",falseLabel:"False",afterLabel:"之後",beforeLabel:"之前",todayLabel:"今天",yesterdayLabel:"昨天",thisMonthLabel:"本月",lastMonthLabel:"上月",nextMonthLabel:"下個月",thisYearLabel:"今年",lastYearLabel:"去年",nextYearLabel:"明年",atLabel:"在",atNullText:"在...",notAtLabel:"不在",notAtNullText:"不在...",atBeforeLabel:"不晚於",atBeforeNullText:"不晚於...",atAfterLabel:"不早於",atAfterNullText:"不早於...",clearLabel:"清除篩選條件",noFilterLabel:"否",onLabel:"開啟",notOnLabel:"未開啟",advancedButtonLabel:"進階",filterDialogCaptionLabel:"進階篩選條件",filterDialogConditionLabel1:"顯示記錄匹配 ",filterDialogConditionLabel2:" 符合以下條件",filterDialogConditionDropDownLabel:"篩選條件",filterDialogOkLabel:"搜尋",filterDialogCancelLabel:"取消",filterDialogAnyLabel:"任何",filterDialogAllLabel:"全部",filterDialogAddLabel:"新增",filterDialogErrorLabel:"您已達到支援的最大篩選條件數量。",filterDialogCloseLabel:"關閉篩選對話方塊",filterSummaryTitleLabel:"搜索結果",filterSummaryTemplate:"${matches} 個匹配記錄",filterDialogClearAllLabel:"清除全部",tooltipTemplate:"${condition} 篩選條件已套用",featureChooserText:"隱藏篩選條件",featureChooserTextHide:"顯示篩選條件",featureChooserTextAdvancedFilter:"進階篩選條件",virtualizationSimpleFilteringNotAllowed:"列虛擬化需要不同類型的過濾。將篩選模式設為 'advanced' 或停用 advancedModeEditorsVisible",multiRowLayoutSimpleFilteringNotAllowed:"多行佈局需要不同類型的過濾。將篩選模式設為 'advanced'",featureChooserNotReferenced:"缺少對功能選擇器的引用。在您的專案中加入 infragistics.ui.grid.featurechooser.js，使用載入程式或合併的指令碼檔案之一。",conditionListLengthCannotBeZero:"columnSettings 中的 conditionList 陣列為空。應為 conditionList 提供合適的陣列。",conditionNotValidForColumnType:"條件 '{0}' 對當前設定無效。應使用適合 {1} 列類型的條件替換它。",defaultConditionContainsInvalidCondition:"'{0}' 列的 defaultExpression 包含不允許的條件。應該用適合 {0} 列類型的條件替換它。",initialConditionIsNotInTheConditionsListArrayOrIsNotInTheDefaultConditions:"列 '{0}' 的 columnSettings 中設定的初始條件不是預設值 (或自訂條件)，或 columnSettins 中設定的 conditionList 陣列中不存在初始條件。請套用有效條件。"};$.ig.GridGroupBy=$.ig.GridGroupBy||{};$.ig.locale["zh-Hant"].GridGroupBy={optionChangeNotSupported:"初始化後無法編輯 {optionName}。其值應在初始化期間設定。",emptyGroupByAreaContent:"將列拖曳至此處或將{0}拖曳至分組依據",emptyGroupByAreaContentSelectColumns:"選擇列",emptyGroupByAreaContentSelectColumnsCaption:"選擇列",expandTooltip:"展開已分組的行",collapseTooltip:"折疊分組行",removeButtonTooltip:"移除已分組的列",modalDialogCaptionButtonDesc:"升序排列",modalDialogCaptionButtonAsc:"降序排序",modalDialogCaptionButtonUngroup:"取消分組",modalDialogGroupByButtonText:"分組根據",modalDialogCaptionText:"新增至分組依據",modalDialogDropDownLabel:"顯示:",modalDialogClearAllButtonLabel:"清除全部",modalDialogRootLevelHierarchicalGrid:"根",modalDialogDropDownButtonCaption:"顯示/隱藏",modalDialogButtonApplyText:"套用",modalDialogButtonCancelText:"取消",fixedVirualizationNotSupported:"分組依據需要其他虛擬化設定。虛擬化模式應設定為 'continuous'。",summaryRowTitle:"分組摘要行",summaryIconTitle:"{0} 的摘要: {1}"};$.ig.GridHiding=$.ig.GridHiding||{};$.ig.locale["zh-Hant"].GridHiding={optionChangeNotSupported:"初始化後無法編輯 {optionName}。其值應在初始化期間設定。",columnChooserDisplayText:"列選擇器",hiddenColumnIndicatorTooltipText:"隱藏列",columnHideText:"隱藏",columnChooserCaptionLabel:"列選擇器",columnChooserCloseButtonTooltip:"關閉",hideColumnIconTooltip:"隱藏",featureChooserNotReferenced:"缺少對功能選擇器的引用。在您的專案中加入 infragistics.ui.grid.featurechooser.js 或使用其中一個合併的指令檔案。",columnChooserShowText:"顯示",columnChooserHideText:"隱藏",columnChooserResetButtonLabel:"重設",columnChooserButtonApplyText:"套用",columnChooserButtonCancelText:"取消"};$.ig.GridResizing=$.ig.GridResizing||{};$.ig.locale["zh-Hant"].GridResizing={optionChangeNotSupported:"初始化後無法編輯 {optionName}。其值應在初始化期間設定。",noSuchVisibleColumn:"指定鍵沒有可見列。在嘗試調整列大小之前，應在列上使用 showColumn() 方法。",resizingAndFixedVirtualizationNotSupported:"調整列大小需要不同的虛擬化設定。使用 rowVirtualization 並將 virtualizationMode 設定為 'continuous'。"};$.ig.GridPaging=$.ig.GridPaging||{};$.ig.locale["zh-Hant"].GridPaging={optionChangeNotSupported:"初始化後無法編輯 {optionName}。其值應在初始化期間設定。",pageSizeDropDownLabel:"顯示 ",pageSizeDropDownTrailingLabel:"記錄",nextPageLabelText:"下一步",prevPageLabelText:"上一個",firstPageLabelText:"",lastPageLabelText:"",currentPageDropDownLeadingLabel:"第",currentPageDropDownTrailingLabel:"的 ${count}",currentPageDropDownTooltip:"選擇頁面索引",pageSizeDropDownTooltip:"選擇每頁記錄數",pagerRecordsLabelTooltip:"當前記錄範圍",prevPageTooltip:"上一頁",nextPageTooltip:"下一頁",firstPageTooltip:"第一頁",lastPageTooltip:"最後一頁",pageTooltipFormat:"頁面 ${index}",pagerRecordsLabelTemplate:"${recordCount} 條記錄中的 ${startRecord}-${endRecord} 條",invalidPageIndex:"指定的頁面索引無效。提供的頁面索引大於或等於0且小於頁面總數。"};$.ig.GridSelection=$.ig.GridSelection||{};$.ig.locale["zh-Hant"].GridSelection={optionChangeNotSupported:"初始化後無法編輯 {optionName}。其值應在初始化期間設定。",persistenceImpossible:"持續選擇需要不同的組態。應該設定網格的主鍵選項。"};$.ig.GridRowSelectors=$.ig.GridRowSelectors||{};$.ig.locale["zh-Hant"].GridRowSelectors={optionChangeNotSupported:"初始化後無法編輯 {optionName}。其值應在初始化期間設定。",selectionNotLoaded:"igGridSelection 尚未初始化。應該為網格啟用選擇。",columnVirtualizationEnabled:"行選擇器需要不同的虛擬化設定。使用 rowVirtualization 或將 virtualizationMode 設定為 'continuous'。",selectedRecordsText:"您已選擇 ${checked} 條記錄。",deselectedRecordsText:"您已取消選擇 ${unchecked} 條記錄。",selectAllText:"選擇所有 ${totalRecordsCount} 條記錄",deselectAllText:"取消選擇所有 ${totalRecordsCount} 條記錄",requireSelectionWithCheckboxes:"啟用複選框時，需要選擇"};$.ig.GridSorting=$.ig.GridSorting||{};$.ig.locale["zh-Hant"].GridSorting={optionChangeNotSupported:"初始化後無法編輯 {optionName}。其值應在初始化期間設定。",sortedColumnTooltip:"已排序 ${direction}",unsortedColumnTooltip:"排序列",ascending:"升序",descending:"降序",modalDialogSortByButtonText:"排序根據",modalDialogResetButton:"重設",modalDialogCaptionButtonDesc:"點選排序降序",modalDialogCaptionButtonAsc:"點選排序升序",modalDialogCaptionButtonUnsort:"點選刪除排序",featureChooserText:"排序多個",modalDialogCaptionText:"排序多個",modalDialogButtonApplyText:"套用",modalDialogButtonCancelText:"取消",sortingHiddenColumnNotSupport:"指定的列已隱藏，因此無法排序。在嘗試對其排序之前，請對它使用 showColumn() 方法。",featureChooserSortAsc:"升序排列",featureChooserSortDesc:"降序排序"};$.ig.GridSummaries=$.ig.GridSummaries||{};$.ig.locale["zh-Hant"].GridSummaries={optionChangeNotSupported:"初始化後無法編輯 {optionName}。其值應在初始化期間設定。",featureChooserText:"隱藏摘要",featureChooserTextHide:"顯示摘要",dialogButtonOKText:"確定",dialogButtonCancelText:"取消",emptyCellText:"",summariesHeaderButtonTooltip:"顯示/隱藏摘要",defaultSummaryRowDisplayLabelCount:"計數",defaultSummaryRowDisplayLabelMin:"最小",defaultSummaryRowDisplayLabelMax:"最大",defaultSummaryRowDisplayLabelSum:"總和",defaultSummaryRowDisplayLabelAvg:"平均",defaultSummaryRowDisplayLabelCustom:"客製化",calculateSummaryColumnKeyNotSpecified:"缺少列鍵。應指定列鍵以計算匯總。",featureChooserNotReferenced:"缺少對功能選擇器的引用。在您的專案中加入 infragistics.ui.grid.featurechooser.js 或使用其中一個合併的指令檔案。"};$.ig.GridUpdating=$.ig.GridUpdating||{};$.ig.locale["zh-Hant"].GridUpdating={optionChangeNotSupported:"初始化後無法編輯 {optionName}。其值應在初始化期間設定。",doneLabel:"完成",doneTooltip:"停止編輯和更新",cancelLabel:"取消",cancelTooltip:"停止編輯而不更新",addRowLabel:"新增行",addRowTooltip:"開始新增新行",deleteRowLabel:"刪除行",deleteRowTooltip:"刪除行",igTextEditorException:"當前無法更新網格中的字串列。ui.igTextEditor 應該先被載入。",igNumericEditorException:"當前無法更新網格中的數字列。ui.igNumericEditor 應該先被載入。",igCheckboxEditorException:"當前無法更新網格中的複選框列。ui.igCheckboxEditor 應該先載入。",igCurrencyEditorException:"當前無法在網格中使用貨幣格式更新數字列。ui.igCurrencyEditor 應該先被載入。",igPercentEditorException:"當前無法在網格中以百分比格式更新數字列。ui.igPercentEditor 應該先載入。",igDateEditorException:"當前無法更新網格中的日期列。ui.igDateEditor 應該先被載入。",igDatePickerException:"當前無法更新網格中的日期列。ui.igDatePicker 應該先被載入。",igTimePickerException:"當前無法更新網格中的日期列。ui.igTimePicker 應該先被載入。",igComboException:"當前無法在網格中使用連擊。ui.igCombo 應該先載入。",igRatingException:"當前無法在網格中使用 igRating 作為編輯器。ui.igRating 應該先載入。",igValidatorException:"目前不支援 igGridUpdating 中定義的選項的驗證。應該先加載入 ui.igValidator。",noPrimaryKeyException:"為了支援刪除行後的更新操作，應用程式應在 igGrid 的選項中定義 primaryKey。",hiddenColumnValidationException:"無法編輯具有隱藏列且啟用驗證的行。",dataDirtyException:"網格具有暫停的交易，這可能會影響資料的轉譯。為了防止異常，應用程式可以啟用 igGrid 的 autoCommit 選項，或處理 igGridUpdating 的 dataDirty 事件並返回 false。在處理該事件時，應用程式也可以在 igGrid 中進行 commit() 資料。",recordOrPropertyNotFoundException:"未找到指定的記錄或屬性。驗證搜索條件，必要時進行調整。",rowUpdatingNotSupportedWithColumnVirtualization:'使用 editMode 更新: "row" 需要不同的設定。columnVirtualization 應該被禁用。',rowEditDialogCaptionLabel:"編輯行資料",excelNavigationNotSupportedWithCurrentEditMode:"Excel Navigation 需要不同的設定。editMode 應該設定為 'cell' 或 'row'。",columnNotFound:"在可見列的集合中未找到指定的列鍵，或指定的索引超出範圍。",rowOrColumnSpecifiedOutOfView:"當前無法編輯指定的行或列。它應顯示在當前頁面和虛擬化框架上。",editingInProgress:"當前正在編輯行或儲存格。當前編輯完成前，無法開始其他更新程序。",undefinedCellValue:"無法將未定義設為儲存格值。",addChildTooltip:"新增子行",multiRowGridNotSupportedWithCurrentEditMode:"當網格啟用多行佈局時，僅支援對話框編輯模式。",virtualizationNotSupportedWithoutAutoCommit:" 在 autoCommit 設定為 false 時，不支援啟用更新和虛擬化。請將表格的 autoCommit 選項設為 true。"};$.ig.CellMerging=$.ig.CellMerging||{};$.ig.locale["zh-Hant"].CellMerging={optionChangeNotSupported:"初始化後無法編輯 {optionName}。其值應在初始化期間設定。",mergeStrategyNotAFunction:"無法將指定的 mergeStrategy 識別為有效的預定義值，或找不到具有該名稱的函數。"};$.ig.ColumnMoving=$.ig.ColumnMoving||{};$.ig.locale["zh-Hant"].ColumnMoving={optionChangeNotSupported:"初始化後無法編輯 {optionName}。其值應在初始化期間設定。",movingDialogButtonApplyText:"套用",movingDialogButtonCancelText:"取消",movingDialogCaptionButtonDesc:"下移",movingDialogCaptionButtonAsc:"向上移動",movingDialogCaptionText:"移動列",movingDialogDisplayText:"移動列",movingDialogDropTooltipText:"移至此處",movingDialogCloseButtonTitle:"關閉移動對話框",dropDownMoveLeftText:"向左移動",dropDownMoveRightText:"向右移動",dropDownMoveFirstText:"首先移動",dropDownMoveLastText:"移至最後",featureChooserNotReferenced:"缺少對功能選擇器的引用。在您的專案中加入 infragistics.ui.grid.featurechooser.js 或使用其中一個合併的指令檔案。",movingToolTipMove:"移動",featureChooserSubmenuText:"移動至",columnVirtualizationEnabled:"列移動需要不同的虛擬化設定。使用 rowVirtualization 或將 virtualizationMode 設定為 'continuous'。"};$.ig.ColumnFixing=$.ig.ColumnFixing||{};$.ig.locale["zh-Hant"].ColumnFixing={optionChangeNotSupported:"初始化後無法編輯 {optionName}。其值應在初始化期間設定。",headerFixButtonText:"固定此列",headerUnfixButtonText:"取消固定此列",featureChooserTextFixedColumn:"固定列",featureChooserTextUnfixedColumn:"取消固定列",groupByNotSupported:"列固定需要其他設定。應禁用分組依據功能。",virtualizationNotSupported:"列固定需要不同的虛擬化設定。應該改用 rowVirtualization。",columnVirtualizationNotSupported:"列固定需要不同的虛擬化設定。columnVirtualization 應該被禁用。",columnMovingNotSupported:"列固定需要其他設定。列移動應被禁用。",hidingNotSupported:"列固定需要其他設定。隱藏功能應停用。",hierarchicalGridNotSupported:"igHierarchicalGrid 不支援列固定。列固定功能應停用。",responsiveNotSupported:"列固定需要其他設定。應禁用響應功能。",noGridWidthNotSupported:"列固定需要其他設定。網格寬度應設定為百分比或像素數。",gridHeightInPercentageNotSupported:"列固定需要其他設定。網格高度應以像素為單位來設定。",defaultColumnWidthInPercentageNotSupported:"列固定需要其他設定。預設列寬應設定為以像素為單位的數字。",columnsWidthShouldBeSetInPixels:"列固定需要不同的列寬設定。鍵 {key} 的列的寬度應以像素為單位設定。",unboundColumnsNotSupported:"列固定需要其他設定。未綁定的列應被禁用。",excelNavigationNotSupportedWithCurrentEditMode:"Excel Navigation 需要不同的設定。editMode 應該設定為 'cell' 或 'row'。",initialFixingNotApplied:"初始固定無法應用於帶鍵的列: {0}. 原因: {1}",setOptionGridWidthException:"選項網格寬度的值不正確。當存在固定列時，未固定列的可見區域的寬度應大於或等於 minimumVisibleAreaWidth 的值。",noneError:"您的網格設定成功！",notValidIdentifierError:"指定的列鍵無效。提供與已定義網格列之一鍵匹配的列鍵。",fixingRefusedError:"當前不支援固定此列。取消固定其他可見列，或先對任何隱藏的未固定列使用 showColumn() 方法。",fixingRefusedMinVisibleAreaWidthError:"無法固定此列。它的寬度超出了在網格中固定列的可用空間。",alreadyHiddenError:"目前無法固定/取消固定此列。首先應在列上使用 showColumn() 方法。",alreadyUnfixedError:"已取消固定此列。",alreadyFixedError:"已固定此列。",unfixingRefusedError:"目前無法取消固定此列。首先應在任何隱藏的固定列上使用 showColumn() 方法。",targetNotFoundError:"找不到包含鍵 {key} 的目標列。驗證用於搜索的關鍵字，並在必要時進行調整。"};$.ig.GridAppendRowsOnDemand=$.ig.GridAppendRowsOnDemand||{};$.ig.locale["zh-Hant"].GridAppendRowsOnDemand={optionChangeNotSupported:"初始化後無法編輯 {optionName}。其值應在初始化期間設定。",loadMoreDataButtonText:"載入更多資料",appendRowsOnDemandRequiresHeight:"按需追加行需要不同的設定。應設定網格高度。",groupByNotSupported:"按需追加行需要不同的設定。分組依據應被禁用。",pagingNotSupported:"按需追加行需要不同的設定。分頁應禁用。",cellMergingNotSupported:"按需追加行需要不同的設定。單元合併應該被禁用。",virtualizationNotSupported:"按需追加行需要不同的設定。應禁用虛擬化。"};$.ig.igGridResponsive=$.ig.igGridResponsive||{};$.ig.locale["zh-Hant"].igGridResponsive={optionChangeNotSupported:"初始化後無法編輯 {optionName}。其值應在初始化期間設定。",fixedVirualizationNotSupported:'響應功能需要不同的虛擬化設定。虛擬化模式應設定為 "continuous"。'};$.ig.igGridMultiColumnHeaders=$.ig.igGridMultiColumnHeaders||{};$.ig.locale["zh-Hant"].igGridMultiColumnHeaders={optionChangeNotSupported:"初始化後無法編輯 {optionName}。其值應在初始化期間設定。",multiColumnHeadersNotSupportedWithColumnVirtualization:"多列標頭需要不同的設定。columnVirtualization 應該被禁用。",cannotExpandMultiColumnHeader:"多列標頭超出了最大允許的固定區域寬度，因此無法展開",atLeastOneColumnShouldBeShownWhenCollapseOrExpand:"展開或折疊多列標題時，至少應顯示一列。",collapsedColumnIconTooltip:"擴大",expandedColumnIconTooltip:"折疊"};$.ig.Grid.locale=$.ig.Grid.locale||$.ig.locale["zh-Hant"].Grid;$.ig.GridFiltering.locale=$.ig.GridFiltering.locale||$.ig.locale["zh-Hant"].GridFiltering;$.ig.GridGroupBy.locale=$.ig.GridGroupBy.locale||$.ig.locale["zh-Hant"].GridGroupBy;$.ig.GridHiding.locale=$.ig.GridHiding.locale||$.ig.locale["zh-Hant"].GridHiding;$.ig.GridResizing.locale=$.ig.GridResizing.locale||$.ig.locale["zh-Hant"].GridResizing;$.ig.GridPaging.locale=$.ig.GridPaging.locale||$.ig.locale["zh-Hant"].GridPaging;$.ig.GridSelection.locale=$.ig.GridSelection.locale||$.ig.locale["zh-Hant"].GridSelection;$.ig.GridRowSelectors.locale=$.ig.GridRowSelectors.locale||$.ig.locale["zh-Hant"].GridRowSelectors;$.ig.GridSorting.locale=$.ig.GridSorting.locale||$.ig.locale["zh-Hant"].GridSorting;$.ig.GridSummaries.locale=$.ig.GridSummaries.locale||$.ig.locale["zh-Hant"].GridSummaries;$.ig.GridUpdating.locale=$.ig.GridUpdating.locale||$.ig.locale["zh-Hant"].GridUpdating;$.ig.CellMerging.locale=$.ig.CellMerging.locale||$.ig.locale["zh-Hant"].CellMerging;$.ig.ColumnMoving.locale=$.ig.ColumnMoving.locale||$.ig.locale["zh-Hant"].ColumnMoving;$.ig.ColumnFixing.locale=$.ig.ColumnFixing.locale||$.ig.locale["zh-Hant"].ColumnFixing;$.ig.GridAppendRowsOnDemand.locale=$.ig.GridAppendRowsOnDemand.locale||$.ig.locale["zh-Hant"].GridAppendRowsOnDemand;$.ig.igGridResponsive.locale=$.ig.igGridResponsive.locale||$.ig.locale["zh-Hant"].igGridResponsive;$.ig.igGridMultiColumnHeaders.locale=$.ig.igGridMultiColumnHeaders.locale||$.ig.locale["zh-Hant"].igGridMultiColumnHeaders;$.ig.HierarchicalGrid.locale=$.ig.HierarchicalGrid.locale||$.ig.locale["zh-Hant"].HierarchicalGrid;return $.ig.locale["zh-Hant"]});