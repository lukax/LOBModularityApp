﻿#region Usings

using System;

#endregion

namespace LOB.Dao.Contract {
    public interface IUnityOfWork : IDisposable {
        ///// <summary>
        /////     Tests the database connection if it's working.
        /////     Invokes Event OnError if connection failed with error message.
        ///// </summary>
        ///// <returns>True if connection to the database sucessed</returns>
        //bool TestConnection();
        object Orm { get; }

        IUnityOfWork Initialize();
        void Commit();
        void Rollback();
        void Flush();
        bool IsInitialized();
    }
}