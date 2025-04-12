using System;
using System.Collections.Generic;
using System.Text;
using Models.Enums;

namespace Models.Actions
{

    public class ModelActions
    {
        public ModelActions() { }

        public int DataAction(DataActions action) //T dataObject, 
        {
            int lid = 0;

            try
            {
                switch (action)
                {
                    case DataActions.Insert:
                        {
                            //lid = new DbLib<T>(dataObject, this._connectionType, this._remoteDatabaseName, this._transaction).Insert();
                            break;
                        }
                    case DataActions.Update:
                        {
                            //lid = new DbLib<T>(dataObject, this._connectionType, this._remoteDatabaseName, this._transaction).Update();
                            break;
                        }
                    case DataActions.Delete:
                        {
                            //lid = new DbLib<T>(dataObject, this._connectionType, this._remoteDatabaseName, this._transaction).Delete();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                lid = 0;
                throw new Exception("El objeto '"  + "'  no se ha guardado. " + ex.Message); // +dataObject.GetName()
                //MessageBox.Show("Ya existe un objeto con el nombre '" + dataObject.GetName() + "'. \n\nEl objeto no se ha guardado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return lid;
        }
    }

}