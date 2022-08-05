

namespace MJ_CAIS.DTO.AbstractMessageType
{
    public class EcrisNotificationDecision
    {
        public string DecisionChangeType { get; set; }
        //Код на съдебния орган:
        public string DecisionDate { get; set; }
        //Решение - Забележки:
        public string DecidingAuthorityName { get; set; }

        //Изтриване от Регистъра:
        //Дата на крайно решение:


    }
}
