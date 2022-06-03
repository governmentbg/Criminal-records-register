/*!@license
* Infragistics.Web.ClientUI infragistics.undo.js resources 21.1.20211.72
*
* Copyright (c) 2011-2021 Infragistics Inc.
*
* http://www.infragistics.com/
*
*/
(function(factory){if(typeof define==="function"&&define.amd){define([],factory)}else{factory()}})(function(){$=$||{};$.ig=$.ig||{};$.ig.locale=$.ig.locale||{};$.ig.locale.it=$.ig.locale.it||{};$.ig.locale.it.undo=$.ig.locale.it.undo||{};var l=$.ig.locale.it.undo;l["AddItemDescription"]="Aggiungi '{1}'";l["AddItemDescriptionDetailed"]="Aggiungi '{1}'";l["AddRangeDescription"]="Aggiungi {1} {2}";l["AddRangeDescriptionDetailed"]="Aggiungi {1} {2}";l["FallbackTransactionDescription"]="";l["LE_AddOpenTransaction"]="Impossibile aggiungere un UndoTransaction che non è stato aperto o è ancora aperto.";l["LE_AddTransactionDirect"]="Non è possibile aggiungere UndoTransaction. RootTransaction viene aggiunto automaticamente su Commit.";l["LE_AddUnitWhileTransactionOpen"]="Impossibile aggiungere un UndoUnit mentre la transazione contiene una transazione aperta nidificata '{0}'.";l["LE_ArgumentIsNegative"]="{0}' deve essere uguale o superiore a 0. Valore effettivo: '{1}'";l["LE_CannotExecuteOpenTransaction"]="Impossibile richiamare Execute mentre la transazione '{0}' è aperta.";l["LE_ChangeHistoryInMerge"]="Impossibile modificare la cronologia Annulla / Ripeti mentre viene richiamato Merge.";l["LE_ChangeHistoryInRemoveAll"]="Impossibile modificare la cronologia Annulla / Ripeti mentre viene richiamato RemoveAll.";l["LE_ChildTransactionNotInUnits"]="La transazione figlio specificata '{0}' non fa parte delle Unità di questa transazione.";l["LE_ClosingOtherTransaction"]="La transazione specificata '{0}' non è la transazione attualmente aperta '{1}'.";l["LE_EndTransactionWhileSuspended"]="Impossibile chiudere una transazione mentre UndoManager è sospeso.";l["LE_EnumEnded"]="L'enumeratore è stato completato.";l["LE_EnumFailedVersion"]="La raccolta è stata modificata dopo l'avvio dell'enumeratore.";l["LE_EnumNotStarted"]="L'enumeratore non è stato avviato. Chiama MoveNext.";l["LE_FactoryNullTransaction"]="UndoUnitFactory ha restituito UndoTransaction null.";l["LE_HasOpenTransaction"]="Una transazione è già stata aperta.";l["LE_HistoryItemNotInCurrentHistory"]="UndoHistoryItem non esiste nella cronologia Annulla o Ripeti associata in UndoManager.";l["LE_InvalidTransactionOwner"]="Il proprietario della transazione specificata non è questo oggetto.";l["LE_NeedAddRemoveAction"]="L'azione specificata deve essere 'Aggiungi' o 'Rimuovi'.";l["LE_NewTransactionWhileSuspended"]="Impossibile avviare una transazione mentre UndoManager è sospeso.";l["LE_RangeCollectionAction"]="Le azioni di intervallo non sono supportate.";l["LE_ReferenceNotRegistered"]="Il riferimento specificato '{0}' non è stato registrato con un'istanza UndoManager. Utilizzare il metodo RegisterReference per registrare il riferimento con UndoManager o passare null come 'riferimento' per utilizzare l'istanza statica / condivisa del thread UndoManager.Current.";l["LE_ReferenceRegisteredToOther"]="Il riferimento specificato '{0}' è registrato con un'altra istanza di UndoManager.";l["LE_RemoveAllFailedVersion"]="La raccolta è stata modificata durante la chiamata a RemoveAll.";l["LE_ResetCollectionAction"]="L'azione di ripristino non è supportata.";l["LE_TargetCollectionIsReadOnly"]="La raccolta specificata '{0}' non può essere di sola lettura.";l["LE_TransactionAlreadyOpened"]="La transazione è già stata aperta.";l["LE_TransactionClosed"]="La transazione non può essere modificata una volta chiusa.";l["LE_TransactionNotOpened"]="La transazione specificata '{0}' non è aperta.";l["LE_TransactionNotStarted"]="La transazione non può essere modificata fino a quando non è stata avviata.";l["LE_UndoManagerAsReference"]="Un'istanza 'UndoManager' non può essere un riferimento.";l["LE_UndoRedoInRollback"]="Impossibile eseguire un Annulla / Ripeti durante il rollback.";l["LE_UndoRedoInTransaction"]="Impossibile eseguire un Annulla / Ripeti durante l'apertura di una transazione.";l["LE_UndoRedoInUndoRedo"]="Impossibile eseguire Annulla / Ripeti mentre è in corso Annulla / Ripeti.";l["LE_UndoRedoWhileSuspended"]="Impossibile eseguire Undo / Redo mentre UndoManager è stato sospeso.";l["MoveItemDescription"]="Sposta '{1}'";l["MoveItemDescriptionDetailed"]="Sposta '{1}' da '{2}' a '{3}'";l["PropertyChangeDescription"]="Modifica '{0}' in '{1}'";l["PropertyChangeDescriptionDetailed"]="Cambia '{0}' in '{1}' in '{3}'";l["ReinitializeCollectionDescription"]="Modifica batch '{2}'";l["ReinitializeCollectionDescriptionDetailed"]="Modifica batch '{2}'";l["RemoveItemDescription"]="Rimuovi '{1}'";l["RemoveItemDescriptionDetailed"]="Rimuovi '{1}'";l["RemoveRangeDescription"]="Rimuovi {1} {2}";l["RemoveRangeDescriptionDetailed"]="Rimuovi {1} {2}";l["ReplaceItemDescription"]='Sostituisci "{1}"';l["ReplaceItemDescriptionDetailed"]="Sostituisci '{1}' con '{2}'";$.ig.undo=$.ig.undo||{};$.ig.undo.locale=$.ig.undo.locale||l;return l});