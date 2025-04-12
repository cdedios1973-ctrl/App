namespace App.DAO
{
    using App.Enums;
    using App.Logic.IO;
    using App.Models;
    using System;

    public static class DAOParser
    {

        private const string INCORRECT_SYNTAX = "Incorrect syntax ";
        private const string TABLE_NAME_MISSING = "Must select a table before do the action.";
        private const string INDEX_OUT_OF_RANGE = "Index was outside the bounds of the array.";
        private const string INCORRECT_FIELD_NUMBER = "Need to fill all fields.";

        #region Database data string parser.

        internal static DDBBParams GetStringParser(ref UsersPermission permission, string input)
        {
            try
            {
                if (permission.PermissionName == "" || permission == null)
                    throw new Exception(TABLE_NAME_MISSING);

                if (input.Length < 6)
                    throw new Exception(INCORRECT_SYNTAX);

                DataActions action = GetDDBBAction(ref input);

                long id = 0;
                string[] data = input.Split(',');
                id = (action != DataActions.Insert) ? id = long.Parse(data[0]) : 0;

                if (action == DataActions.Delete)
                    data = new string("0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,").Split(',');

                return new DDBBParams
                {
                    usersPermission = permission,
                    dataId = id,
                    data = data,
                    dataAction = action,
                    filter = new DataFilter()
                };
            }
            catch (Exception ex)
            {
                string msg = "Syntax: ";
                if (ex.Message == INDEX_OUT_OF_RANGE)
                    msg += INCORRECT_FIELD_NUMBER;
                else
                    msg += ex.Message;
                throw new Exception(ex.Message);
            }
        }

        private static DataActions GetDDBBAction(ref string input)
        {
            List<AppCommand> cmds = new AppCommand().GetDDBBCommandList;

            if ((input.Substring(3, 1) != "(") || !input.EndsWith(')'))
                throw new Exception(INCORRECT_SYNTAX + cmds[0].Description);

            DataActions action = DataActions.Select;

            switch (input.ToUpper().Substring(0, 3))
            {
                case "INS": action = DataActions.Insert; break;
                case "UPD": action = DataActions.Update; break;
                case "DEL": action = DataActions.Delete; break;
                default: action = DataActions.Select; break;
            }

            long id = 0;
            long.TryParse(input.Substring(4, 1), out id);

            if (action == DataActions.Insert && id > 0)
                throw new Exception(INCORRECT_SYNTAX + cmds[0].Description);

            if (action == DataActions.Update && id < 0)
                throw new Exception(INCORRECT_SYNTAX + cmds[1].Description);

            if (action == DataActions.Delete && id < 0)
                throw new Exception(INCORRECT_SYNTAX + cmds[2].Description);

            input = input.Remove(0, 4);                 //Remove the initials chars: 'IN(' // 'UP(' // 'DEL('
            input = input.Remove(input.Length - 1, 1);  //delete last parenthesis

            return action;
        }

        #endregion
    }
}
