/*
using System;
using App.Models.DTO;
using App.Logic.Models.POCO;
using App.Definitions.Interfaces;

namespace App.Logic.Services
{
    internal class DataService<T> where T : IDataService
    {
        internal DataService() { }

        public bool Register(UserDTO dto, out string msgError)
        {
            msgError = "";
            try
            {
                User user = new User(dto.Id, dto.Name, dto.Surname, dto.Address, dto.CountryIdentity);
                //_dao.InsertData(user);
            }
            catch (ArgumentException ex)
            {
                // Get exception from Users constructor if something happens.
                msgError = ex.Message;
            }

            return msgError != "" ? false : true;

        }

    }
}
*/