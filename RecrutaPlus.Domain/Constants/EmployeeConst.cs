namespace RecrutaPlus.Domain.Constants
{
    public struct EmployeeConst
    {
        public static string MSG_SELECIONE = "Selecione...";

        //Mensage Template
        public static string LOG_INDEX = "Controller: Employees | Action: Index | User: {User} | Created: {Created}";
        public static string LOG_CREATE = "Controller: Employees | Action: Create | User: {User} | Created: {Created}";
        public static string LOG_EDIT = "Controller: Employees | Action: Edit | User: {User} | Created: {Created}";
        public static string LOG_DELETE = "Controller: Employees | Action: Delete | User: {User} | Created: {Created}";
        public static string LOG_DETAILS = "Controller: Employees | Action: Details | User: {User} | Created: {Created}";

        public static string LOG_EXCEPTION = "Controller: Employees | Action: Details | User: {User} | Created: {Created} | Exception";
        public static string LOG_EXCEPTION_MSG = "Controller: Employees | Action: Details | User: {User} | Created: {Created} | Exception: {Exception}";

        public static string LOG_TABLE_ADD = "Table: Employees | Action: Add | Created: {Created} | ConcurrencyStamp: {ConcurrencyStamp} | Id: {Id} | {@Employee}";
        public static string LOG_TABLE_UPDATE = "Table: Employees | Action: Update | Created: {Created} | ConcurrencyStamp: {ConcurrencyStamp} | Id: {Id} | {@Employee}";
        public static string LOG_TABLE_REMOVE = "Table: Employees | Action: Remove | Created: {Created} | ConcurrencyStamp: {ConcurrencyStamp} | Id: {Id} | {@Employee}";
    }
}
