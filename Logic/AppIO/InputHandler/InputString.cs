namespace App.Logic.IO.Handler
{
    using System;
    using App.Enums;
    using App.Logic;
    using App.Models;

    public static class InputString
    {
        private const string ERROR_PERMISSIONS_NOT_GRANTED = "Current user has no access to this module.";
        private const string INPUT_FORBIDDEN_STRING = "Input string has forbidden chars.";
        private const string INPUT_USER_NOT_FOUND = "User not found.";

        #region CheckInyection => Check for forbidden strings

        public static bool CheckInyection(ref string input)
        {
            bool ok = true;
            if (input.Contains("*")
                || input.Contains("%")
                || input.Contains("LIKE %")
                || input.Contains("LIKE *")
                || input.Contains(">")
                || input.Contains("<"))
                ok = false;

            return ok;
        }

        public static bool CheckInput(ref string input, bool BeforeLogin, bool test = true)
        {
            bool ok = true;
            if (CheckInyection(ref input) == false)
                throw new Exception(INPUT_FORBIDDEN_STRING);

            if(input.Length == 1 && input == "," )
                throw new Exception(INPUT_USER_NOT_FOUND);

            if (BeforeLogin == true)
                if ((input == "00" && test == true) || (input == "01" && test == true))
                    return true;

            return ok;
        }

        #endregion

        #region CheckSource => Return DataBaseType by ConsoleKey

        public static DataBaseType CheckSource(ConsoleKey k)
        {
            DataBaseType type = DataBaseType.Class;
            switch (k)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    type = DataBaseType.SqlServer;
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    type = DataBaseType.SqLite;
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    type = DataBaseType.XML;
                    break;
            }
            return type;
        }

        #endregion

        #region CheckInputPermission => Return Permission (chech for both, also UserAccess has it implemented)

        public static UsersPermission CheckInputPermission(ref UserAccess userAccess, ref string input)
        {
            long id;
            string sel = input.ToUpper() == "M" ?  "UsersPermission" : "";
            
            UsersPermission per;

            if (long.TryParse(input, out id))
                per = userAccess.GetUserPermissions.Find(item => item.PermissionId == id);
            else
                per = userAccess.GetUserPermissions.Find(item => item.PermissionName == sel);
            
            if (per == null)
                throw new Exception(ERROR_PERMISSIONS_NOT_GRANTED);

            return per;
        }

        #endregion

    }
}
