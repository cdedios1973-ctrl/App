namespace App.DAO
{
    using System;
    using System.Data;
    using System.Reflection;
    using App.Enums;
    using App.Logic;
    using App.Interfaces;

    // TODO: Fix issues 'GetColums' Property can't read if source is XML (Permissions Issues)

    public class DAOSchemas<T> where T : IModel
    {
        private UserAccess _user;

        #region Constructors

        public DAOSchemas(ref UserAccess user)
        {
            IModel obj = (T)Activator.CreateInstance(typeof(T));
            //this._userAccessType = user.checkUserPermission(obj.getTableName());
            if (user.GetUserId <= 0) //|| this._userAccessType <= UserAccessType.Read)
                throw new Exception("DAOSchemas: This user has no permission to this module.");

            this._user = user;
        }

        #endregion

        #region Properties

        public ColumnsConfig GetColumnsConfig
        {
            get { return getColumnsConfig(); }
        }


        #region Function -> Get Table Columns.

        private DataTable getTableColumns()
        {
            DataTable dt = new DataTable();

            PropertyInfo[] properties = typeof(T).GetProperties();
            dt.TableName = typeof(T).Name;

            foreach (PropertyInfo pi in properties)
            {
                DataColumn dc = new DataColumn(pi.Name, pi.PropertyType);
                dt.Columns.Add(dc);
            }

            return dt;
        }

        #endregion

        public DataTable GetColums
        {
            get { return this.getTableColumns(); }
            //get { return new DAOData<T>(ref this._user, this._source).GetTableColumns(); }
        }

        #endregion

        #region Methods && Functions

        private ColumnsConfig getColumnsConfig()
        {
            ColumnsConfig cfg = new ColumnsConfig();
            DataTable dt = this.GetColums;
            var cols = new Dictionary<string, (int Value1, int Value2)>();
            string header = "";
            int i = 0;
            int fieldLenght = 6;

            foreach (DataColumn col in dt.Columns)
            {
                switch (col.DataType.Name.ToUpper())
                {
                    case "INT32":
                        fieldLenght = 10;
                        break;
                    default: //case "STRING"
                        fieldLenght = 20;
                        break;
                }

                int l = (fieldLenght < col.ColumnName.Length) ? col.ColumnName.Length : fieldLenght;

                string tmpHeader = (col.ColumnName + ((i < dt.Columns.Count) ? " | " : "")).PadLeft(l);

                cols.Add(col.ColumnName, (i, tmpHeader.Length));

                header += tmpHeader;

                i = i + 1;
            }

            header += Environment.NewLine + new string('=', header.Length);
            cfg.Column = cols;
            cfg.Header = header;
            return cfg;
        }

        #endregion
    }
}
