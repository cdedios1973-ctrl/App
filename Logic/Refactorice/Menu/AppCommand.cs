namespace App.Logic.IO
{
    using System;
    using AppLogic.Services.Input;

    //TODO : change DS when "s" at setMenuCommandList()

    public sealed class AppCommand
    {
        private string _command = "";
        private string _name = "";
        private string _description = "";

        private static List<AppCommand> _header;
        private static List<AppCommand> _menuCommands;
        private static List<AppCommand> _DDBBCommands;
        private static List<AppCommand> _loginCommands;
        private static List<AppCommand> _testUsers;

        #region Constructors

        public AppCommand()
        {
            setCommandHeader();
        }

        private AppCommand(string command, string name, string _description)
        {
            this._command = command;
            this._name = name;
            this._description = _description;
        }

        #endregion

        #region Properties

        internal string Command
        {
            get { return this._command; }
            set { this._command = value; }
        }

        internal string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        internal string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        public List<AppCommand> GetHeaderList
        {
            get { return _header; }
        }

        public List<AppCommand> GetCommandList
        {
            get 
            {
                this.setMenuCommandList();
                return _menuCommands; 
            }
        }
        
        public List<AppCommand> GetUsersList
        {
            get
            {
                this.setTestUsers();
                return _testUsers;
            }
        }

        public List<AppCommand> GetDDBBCommandList
        {
            get 
            {
                this.setDDBBCommandList();
                return _DDBBCommands; 
            }
        }

        public List<AppCommand> GetLoginCommandList
        {
            get 
            {
                this.setLoginCommandList();
                return _loginCommands; 
            }
        }
        #endregion

        #region Methods - Functions

        private void setCommandHeader()
        {
            if (_header == null)
            {
                _header = new List<AppCommand>() {
                new("  ", "---------", "-----------------------------------------------------")};
            }
        }

        private void setDDBBCommandList()
        {
            if (_DDBBCommands == null)
            {
                _DDBBCommands = new List<AppCommand>() {
                new ("INS", "Insert  ", "Example: INS(0,Name,Surname,Address,Country) "),
                new ("UPD", "Update  ", "Example: UPD(9,Name,Surname,Address,Country) "),
                new ("DEL", "Delete  ", "Example: DEL(8) "),
                new ("SEL", "Select  ", "Example: SEL('ID','=','4')") };
            }
        }

        private void setDDBBCommandListHelp()
        {
            if (_DDBBCommands == null)
            {
                _DDBBCommands = new List<AppCommand>() {
                new ("INS", "Insert  ", "Syntax Example: IN(0,Name,Surname,Address,Country) "),
                new ("UPD", "Update  ", "Syntax Example: UP(9,Name,Surname,Address,Country) "),
                new ("DEL", "Delete  ", "Syntax Example: DE(8) "),
                new ("SEL", "Select  ", "Syntax Examples:"),
                new ("SEL", "Select  ", "      SE(Id,=,97) Search for: Id Equal to 97"),
                new ("SEL", "Select  ", "      SE(Id,<,256) Search for: Id Minor than 256"),
                new ("SEL", "Select  ", "      SE(Name,LIKE,Paul) (Search for: Name=Paul") };
            }
        }
        //TODO : change DS when "s"
        private void setMenuCommandList()
        {
            if (_menuCommands == null)
            {
                _menuCommands = new List<AppCommand>() {
                new("?", "(?)Help  ", "Show Application help.                               "),
                new("M", "(M)enu   ", "Show menu from current user.                         "),
                new("S", "(S)ource ", "Current Database Selection : { " + AppInputService.GetCurrentUser.DDBBType.ToString() + " } "),
                new("E", "(E)xport ", "Export current menu to XML format.                   "),
                new("I", "(I)nit   ", "Restore the application to default (delete all data)."),
                new("U", "(U)sers  ", "Users list (Test mode).                              "),
                new("Q", "(Q)uit   ", "Close the application.                               ")};
            }
        }

        private void setTestUsers()
        {
            if (_testUsers == null)
            {
                _testUsers = new List<AppCommand>() {
                new("00", "00        ", "Log in as 'Admin', has access to all modules         "),
                new("00", "admin,123 ", "Log in as 'Admin', has access to all modules         "),
                new("01", "01        ", "Log in as 'User', has little access.                 "),
                new("01", "user 2,123", "Log in as 'User', has little access.                 ")};
            }
        }

        private void setLoginCommandList()
        {
            if (_loginCommands == null)
            {
                _loginCommands = new List<AppCommand>() {
                new("  ", "---------", "-----------------------------------------------------"),
                new("  ", "Login    ", "Write username(comma)password to log in.(Ex: usr,123)"),
                new("  ", "---------", "-----------------------------------------------------")};
            }
        }

        #endregion

    }
}
