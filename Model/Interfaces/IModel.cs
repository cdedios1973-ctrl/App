namespace App.Interfaces
{
    using System;
    using App.Enums;
    using App.Models;

    public interface IModel
    {
        public string IGetTableName();
        public long IGetId();
        public string IGetSQLJoin();
        public ModuleData IGetModule();
        public DFilter IGetFilterById(long Id);
        public DFilter? IGetFilterByUserId(long UserId);

    }
}