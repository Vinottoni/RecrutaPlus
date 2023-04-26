namespace RecrutaPlus.Domain.Constants
{
    public struct OfficeConst
    {
        public static string MSG_SELECIONE = "Selecione...";

        //Mensage Template
        public static string LOG_INDEX = "Controller: Offices | Action: Index | User: {User} | Created: {Created}";
        public static string LOG_CREATE = "Controller: Offices | Action: Create | User: {User} | Created: {Created}";
        public static string LOG_EDIT = "Controller: Offices | Action: Edit | User: {User} | Created: {Created}";
        public static string LOG_DELETE = "Controller: Offices | Action: Delete | User: {User} | Created: {Created}";
        public static string LOG_DETAILS = "Controller: Offices | Action: Details | User: {User} | Created: {Created}";

        public static string LOG_EXCEPTION = "Controller: Offices | Action: Details | User: {User} | Created: {Created} | Exception";
        public static string LOG_EXCEPTION_MSG = "Controller: Offices | Action: Details | User: {User} | Created: {Created} | Exception: {Exception}";

        public static string LOG_TABLE_ADD = "Table: Offices | Action: Add | Created: {Created} | ConcurrencyStamp: {ConcurrencyStamp} | Id: {Id} | {@Office}";
        public static string LOG_TABLE_UPDATE = "Table: Offices | Action: Update | Created: {Created} | ConcurrencyStamp: {ConcurrencyStamp} | Id: {Id} | {@Office}";
        public static string LOG_TABLE_REMOVE = "Table: Offices | Action: Remove | Created: {Created} | ConcurrencyStamp: {ConcurrencyStamp} | Id: {Id} | {@Office}";
    }
}
