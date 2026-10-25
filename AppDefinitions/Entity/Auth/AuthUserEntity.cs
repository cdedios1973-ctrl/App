using System;

// Ejemplo: una línea con constructor, propiedades readonly e inmutabilidad total
// public record AuthUserEntity(int Id, string Name);

// NOTA Importante: Esta clase NO es serializabe, es para uso interno de la aplicacion, Auditoria de registros.

namespace App.Definitions.Auth
{
    public class AuthUserEntity
    {
        private readonly long _userId;
        private readonly string _userName;

        #region Constructor

        public AuthUserEntity(long userId, string userName)
        {
            this._userId = userId;
            this._userName = userName;
        }

        #endregion

        #region Properties

        public long UserId
        { get { return this._userId; } }

        public string UserName 
        { get { return this._userName; } }

        #endregion
    
    }
}
