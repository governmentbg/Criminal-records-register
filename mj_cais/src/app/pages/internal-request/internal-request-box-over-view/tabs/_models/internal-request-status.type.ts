export enum InternalRequestStatusType {
  Draft = "Draft", // Нова заявка, видимо само за мен, като изпращач
  Outbox = "Cancelled,Ready",// Всички мои изпратени, който са били обработени от отсрещната страна
  OutboxAll = "Cancelled,Ready,Sent",// Всички мои изпратени без значение дали са обработени от др страна
  Inbox = "Sent",// Всички до мен
  InboxAll = "Cancelled,Ready,ReadCancelled,ReadReady",// Всички до мен, които съм обработил
}
