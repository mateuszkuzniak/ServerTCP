using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using DatabaseLibrary;
using MessageLibrary;

namespace ServerLibrary
{
    public class LoginServerProtocol : CommunicationProtocol
    {
        Dictionary<Request, Response> responses;
        readonly Dictionary<string, int> opcodes;
        Account user;

        public LoginServerProtocol() : base()
        {
            responses = new Dictionary<Request, Response>();
            opcodes = new Dictionary<string, int>();
            user = new Account();

            #region OPCODE
            opcodes["LOGIN"] = 0;
            opcodes["REGISTER"] = 1;
            opcodes["CHANGE_PWD"] = 2;
            opcodes["GETUSER"] = 3;
            opcodes["UPDATEUSERDATA"] = 4;
            opcodes["FILEADD"] = 5;
            opcodes["FILEALL"] = 6;
            opcodes["FILEDELETE"] = 7;
            opcodes["FILEUPDATE"] = 8;
            opcodes["FILEOPEN"] = 9;
            opcodes["GETLOGS"] = 10;

            #endregion

            #region LOGIN_AND_REGISTRATION
            responses[new Request(opcodes["LOGIN"], "LOGIN", null, null)] = new Response(0,
                (userName, pass) =>
                {
                    loginAttempServer();
                    if (CheckUser(userName))
                    {
                        userName = userName.ToLower().Trim(new char[] { '\r', '\n', '\0' });
                        if (!UsersDatabase.UserIsLogged(userName))
                        {
                            if (UsersDatabase.CheckPassword(userName, pass))
                            {
                                user = UsersDatabase.GetUserWithDatabase(userName);
                                UsersDatabase.UpdateLoginStatus(user);
                                user.Status = Account.StatusCode.logged;
                                loginUserServer(user.Id, user.Login);
                            }
                            else
                            {
                                user.Status = Account.StatusCode.inv_pass;
                            }

                        }
                        else
                        {
                            user.Status = Account.StatusCode.user_is_logged;
                        }
                    }
                },
                null,
               (args) =>
               {
                   return GetLogStatus();
               });

            responses[new Request(opcodes["REGISTER"], "REGISTER", null, null)] = new Response(0,
                (userName, pass) =>
                {
                    registrationAttempServer();
                    if (!CheckUser(userName) && user.Status == Account.StatusCode.user_does_not_exist)
                    {
                        if (pass.Length == 64)
                        {
                            UsersDatabase.AddUser(userName, pass);
                            user.Status = Account.StatusCode.successful_registration;
                        }
                        else
                            user.Status = Account.StatusCode.inv_pass;
                    }

                    if (user.Status != Account.StatusCode.successful_registration)
                    {
                        if (userName.Length > 0)
                            user.Status = Account.StatusCode.user_exists;
                        else
                            user.Status = Account.StatusCode.inv_user;
                    }

                },
                null,
               (args) =>
               {
                   return GetLogStatus();
               });

            responses[new Request(opcodes["CHANGE_PWD"], "CHANGE_PWD", null, null)] = new Response(0,
                (oldPass, newPass) =>
                {
                    if (user.IsLogged)
                    {
                        if (UsersDatabase.CheckPassword(user.Login, oldPass))
                        {
                            if (UsersDatabase.ChangePassword(user.Login, oldPass, newPass))
                                user.Status = Account.StatusCode.change_pwd;
                            else
                                user.Status = Account.StatusCode.change_pwd_error;
                        }
                        else
                            user.Status = Account.StatusCode.inv_pass;
                    }
                    else
                        user.Status = Account.StatusCode.must_be_logged;
                },
                null,
                (args) =>
                {
                    return GetLogStatus(); ;
                });

            responses[new Request(opcodes["GETUSER"], "GETUSER")] = new Response(0,
               () =>
               {
                   if (user.IsLogged)
                   {
                       user.Status = Account.StatusCode.get_all_user_data;
                   }
                   else
                       user.Status = Account.StatusCode.must_be_logged;
               },
               (args) =>
               {
                   return GetLogStatus();
               });
            responses[new Request(opcodes["UPDATEUSERDATA"], "UPDATEUSERDATA", null)] = new Response(0,
                (string data) =>
                {
                    var tokens = SplitUserData(data);
                    if (user.IsLogged)
                    {
                        if (ValidUserData(tokens))
                            UsersDatabase.UpdateUserData((int)user.Id, tokens);
                        else
                            user.Status = Account.StatusCode.user_inv_data;
                    }
                    else
                        user.Status = Account.StatusCode.must_be_logged;
                },
                (args) =>
                {
                    return GetLogStatus();
                });

            #endregion

            #region FILE
            responses[new Request(opcodes["FILEADD"], "FILEADD", null, null)] = new Response(0,
                (fileName, text) =>
                {
                    if (!CheckFile(fileName) && user.FileStatus == Account.FileCode.file_does_not_exist)
                    {
                        userAddFileServer(user.Id, user.Login, fileName);
                        FileDatabase.AddFile(fileName, text, (int)user.Id);
                        user.FileStatus = Account.FileCode.file_added;
                    }

                },
                null,
                (args) =>
                {
                    return GetFileStatus();
                });


            responses[new Request(opcodes["FILEALL"], "FILEALL")] = new Response(0,
                () =>
                {
                    if (user.IsLogged)
                    {
                        user.FileStatus = Account.FileCode.get_all;
                    }
                    else
                        user.FileStatus = Account.FileCode.must_be_logged;
                },
                (args) =>
                {
                    return GetFileStatus();
                });

            responses[new Request(opcodes["FILEDELETE"], "FILEDELETE", null)] = new Response(0,
                (fileName) =>
                {

                    if (CheckFile(fileName))
                    {
                        FileDatabase.DeleteFile(fileName, (int)user.Id);
                        if (!FileDatabase.FileExists(fileName, (int)user.Id))
                        {
                            userDelFileServer(user.Id, user.Login, fileName);
                            user.FileStatus = Account.FileCode.file_deleted;
                        }
                        else
                            user.FileStatus = Account.FileCode.file_deleted_error;
                    }

                },
                (args) =>
                {
                    return GetFileStatus();
                });


            responses[new Request(opcodes["FILEUPDATE"], "FILEUPDATE", null, null)] = new Response(0,
                (fileName, text) =>
                {

                    if (CheckFile(fileName))
                    {
                        userUpdateFileServer(user.Id, user.Login, fileName);
                        FileDatabase.UpdateFile(fileName, (int)user.Id, text);
                        user.FileStatus = Account.FileCode.file_update;
                    }

                },
                 null,
                (args) =>
                {
                    return GetFileStatus();
                });

            responses[new Request(opcodes["FILEOPEN"], "FILEOPEN", null)] = new Response(0,
                 (fileName) =>
                 {
                     if (CheckFile(fileName))
                     {
                         userOpenFileServer(user.Id, user.Login, fileName);
                         user.FileStatus = Account.FileCode.file_open;
                     }
                 },
                 (fileName) =>
                 {
                     if (user.FileStatus == Account.FileCode.file_open)
                         return FileDatabase.openFile(fileName, (int)user.Id);
                     return GetFileStatus();
                 });

            responses[new Request(opcodes["GETLOGS"], "GETLOGS")] = new Response(0,
               () =>
               {
                   if (user.IsLogged)
                   {
                       user.FileStatus = Account.FileCode.get_logs;
                   }
                   else
                       user.FileStatus = Account.FileCode.must_be_logged;
               },
               (args) =>
               {
                   return GetFileStatus();
               });
            #endregion
        }

        #region GET_STATUS_AND_CHECK

        string GetLogStatus()
        {
            if (user.Status == Account.StatusCode.inv_user)
                return ServerMessage.invUser;
            else if (user.Status == Account.StatusCode.user_is_logged)
                return ServerMessage.currentlyLogged;
            else if (user.Status == Account.StatusCode.must_be_logged)
                return ServerMessage.mustBelogged;
            else if (user.Status == Account.StatusCode.inv_pass)
                return ServerMessage.invPwd;
            else if (user.Status == Account.StatusCode.logged)
                return ServerMessage.logged;
            else if (user.Status == Account.StatusCode.already_logged)
                return ServerMessage.alreadyLogged;
            else if (user.Status == Account.StatusCode.user_exists)
                return ServerMessage.userExists;
            else if (user.Status == Account.StatusCode.user_does_not_exist)
                return ServerMessage.userDoesNotExists;
            else if (user.Status == Account.StatusCode.successful_registration)
                return ServerMessage.regOk;
            else if (user.Status == Account.StatusCode.change_pwd)
                return ServerMessage.changePwd;
            else if (user.Status == Account.StatusCode.change_pwd_error)
                return ServerMessage.changePwdError;
            else if (user.Status == Account.StatusCode.get_all_user_data)
                return UsersDatabase.GetListData((int)user.Id, DatabaseAbstract.DatabaseType.User);
            else if (user.Status == Account.StatusCode.inv_mail)
                return ServerMessage.invMail;
            else if (user.Status == Account.StatusCode.inv_first_name)
                return ServerMessage.invFirstName;
            else if (user.Status == Account.StatusCode.inv_second_name)
                return ServerMessage.invSecondName;
            else if (user.Status == Account.StatusCode.inv_phone_number)
                return ServerMessage.invPhoneNumber;
            else if (user.Status == Account.StatusCode.user_valid_data)
                return ServerMessage.userValidData;
            else if (user.Status == Account.StatusCode.user_inv_data)
                return ServerMessage.userInvData;
            return ServerMessage.unk;
        }

        string GetFileStatus()
        {
            if (user.FileStatus == Account.FileCode.file_added)
                return ServerMessage.fileAdd;
            else if (user.FileStatus == Account.FileCode.file_exists)
                return ServerMessage.fileExists;
            else if (user.FileStatus == Account.FileCode.must_be_logged)
                return ServerMessage.mustBelogged;
            else if (user.FileStatus == Account.FileCode.inv_file_name)
                return ServerMessage.invFileName;
            else if (user.FileStatus == Account.FileCode.get_all)
                return FileDatabase.GetListData((int)user.Id, DatabaseAbstract.DatabaseType.File);
            else if (user.FileStatus == Account.FileCode.file_deleted)
                return ServerMessage.fileDeleted;
            else if (user.FileStatus == Account.FileCode.file_deleted_error)
                return ServerMessage.fileDeletedError;
            else if (user.FileStatus == Account.FileCode.file_update)
                return ServerMessage.fileUpdate;
            else if (user.FileStatus == Account.FileCode.get_logs)
                return getLogs(user.Login);
            return ServerMessage.unk;

        }

        bool CheckFile(string fileName)
        {
            if (fileName.Length > 0)
            {
                if (user.IsLogged)
                {
                    if (FileDatabase.FileExists(fileName, (int)user.Id))
                    {
                        user.FileStatus = Account.FileCode.file_exists;
                        return true;
                    }
                    else
                        user.FileStatus = Account.FileCode.file_does_not_exist;
                }
                else
                    user.FileStatus = Account.FileCode.must_be_logged;
            }
            else
                user.FileStatus = Account.FileCode.inv_file_name;
            return false;
        }
        bool CheckUser(string userName)
        {

            if (!user.IsLogged)
            {
                if (userName.Length > 0)
                {
                    if (UsersDatabase.CheckUserExist(userName))
                        return true;
                    else
                        user.Status = Account.StatusCode.user_does_not_exist;
                }
                else
                    user.Status = Account.StatusCode.inv_user;
            }
            else
                user.Status = Account.StatusCode.already_logged;
            return false;
        }





        #endregion

        #region GET_SET
        public override bool GetUserStatus()
        {
            return user.IsLogged;
        }

        public override Account GetUser()
        {
            return user;
        }

        public override void SetDatabaseFile(FileDb database)
        {
            FileDatabase = database;
        }

        public override void SetDatabaseUser(User database)
        {
            UsersDatabase = database;
        }
        #endregion

        #region ADDITIONAL_FUNC
        string[] SplitUserData(string data)
        {
            string[] tokens = data.Split(new char[] { ' ' });
            string[] temp = new string[4];
            if (tokens.Length < 4)
            {
                temp = new string[4];
                for (int i = 0; i < 4; i++)
                {
                    if (i > tokens.Length - 1)
                        temp[i] = "";
                    else
                    {
                        temp[i] = tokens[i];
                    }
                }
                return temp;
            }
            else
                return tokens;
        }

        bool ValidUserData(string[] data)
        {
            if (data[0].Length > 0 && (!Regex.IsMatch(data[0], @"[@]") || !Regex.IsMatch(data[0], @"[.]")))
                user.Status = Account.StatusCode.inv_mail;
            else if (data[1].Length > 0 && !Regex.IsMatch(data[1], @"^[a-zA-Z]+$"))
                user.Status = Account.StatusCode.inv_first_name;
            else if (data[2].Length > 0 && !Regex.IsMatch(data[2], @"^[a-zA-Z]+(\-[a-zA-Z]+)?$"))
                user.Status = Account.StatusCode.inv_second_name;
            else if (data[3].Length > 0 && (!Regex.IsMatch(data[3], @"^[0-9]+$") || data[3].Length < 9 || data[3].Length > 9))
                user.Status = Account.StatusCode.inv_phone_number;
            else
            {
                user.Status = Account.StatusCode.user_valid_data;
                return true;
            }
            return false;
        }
        #endregion

        public override string GenerateResponse(string message)
        {
            if (message == "")
                return "";
            string[] tokens = message.Split(new char[] { ';' });
            string opcode = tokens[0];
            string args1 = null, args2 = null;

            if (opcode == "EXIT")
            {
                if (user.Id != null)
                    exitClientServer((int)user.Id, user.Login);
                else
                    exitClientServer();
                throw new IOException();
            }
            else if (tokens.Length > 1)
            {
                args1 = tokens[1];
                if (tokens.Length > 2) args2 = tokens[2]; else args2 = null;
            }

            Response response;
            Request request;


            if (args1 == null && args2 == null && (opcode == "FILEALL" || opcode == "GETLOGS" || opcode == "GETUSER"))
            {
                request = new Request(opcodes[opcode], opcode);
                response = responses[request];
                response.Action();
            }
            else if (args1 != null && args2 == null && (opcode == "FILEDELETE" || opcode == "FILEOPEN" || opcode == "UPDATEUSERDATA"))
            {
                request = new Request(opcodes[opcode], opcode, args1);
                response = responses[request];
                response.Action1(args1);
            }
            else if (args1 != null && args2 != null && (opcode == "FILEADD" || opcode == "LOGIN" || opcode == "REGISTER" || opcode == "FILEUPDATE" || opcode == "CHANGE_PWD"))
            {

                request = new Request(opcodes[opcode], opcode, args1, args2);
                response = responses[request];
                response.Action2(args1, args2);
            }
            else
            {
                return ServerMessage.invComm;
            }

            response = responses[request];
            return response.GenerateResponse(args1);
        }
    }
}
