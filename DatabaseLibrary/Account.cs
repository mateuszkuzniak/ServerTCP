using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary
{
    public class Account
    {
        public enum StatusCode
        {
            inv_user,
            inv_pass,
            user_is_logged,
            logged,
            user_exists,
            already_logged,
            successful_registration,
            can_add,
            user_does_not_exist
        }

        public enum FileCode
        {
            file_exists,
            file_does_not_exist,
            must_be_logged,
            inv_file_name,
            file_added,
            get_all,
            file_deleted,
            file_deleted_error,
            file_update,
            file_open

        }

        private int? id;
        private string login;
        private bool isLogged;
        private StatusCode status;
        private FileCode fileStatus;

        public int? Id { get => id; set => id = value; }
        public string Login { get => login; set => login = value; }
        public bool IsLogged { get => isLogged; set => isLogged = value; }
        public StatusCode Status { get => status; set => status = value; }
        public FileCode FileStatus { get => fileStatus; set => fileStatus = value; }

        /// <summary>
        /// Funkcja czyszcząca obiekt
        /// </summary>
        public void Clear()
        {
            Id = null;
            Login = null;
            IsLogged = false;

        }
    }
}
