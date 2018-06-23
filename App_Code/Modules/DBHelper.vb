Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data

Public Class DBHelper
    Dim DBAccessType As Integer = 1
    Public Overloads Shared Function ExecuteReader(ByVal connectionString As String, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String, _
                                                   ByVal ParamArray commandParameters() As IDataParameter) As IDataReader

        Select Case adv.Business.DBAccessType
            Case adv.Business.modGeneral.enumDBAccessType.sql
                Dim arParms() As SqlParameter
                arParms = PrepareSqlParameter(commandParameters)
                Return SqlHelper.ExecuteReader(connectionString, commandType, commandText, arParms)

            Case adv.Business.modGeneral.enumDBAccessType.oledb
                Dim arParms() As OleDbParameter
                arParms = PrepareOleDbParameter(commandParameters)
                Return OleDBHelper.ExecuteReader(connectionString, commandType, commandText, arParms)
        End Select
    End Function
   

    Public Shared Function createParameter(ByVal parameterName As String, ByVal parameterType As System.Data.DbType, ByVal paraDirection As System.Data.ParameterDirection, ByVal value As Object) As IDataParameter
        Dim objParam As IDataParameter
        Select Case adv.Business.DBAccessType
            Case adv.Business.modGeneral.enumDBAccessType.sql
                objParam = New SqlParameter(parameterName, parameterType)
                objParam.Direction = paraDirection
                objParam.Value = value

            Case adv.Business.modGeneral.enumDBAccessType.oledb
                objParam = New OleDbParameter(parameterName, parameterType)
                objParam.Direction = paraDirection
                objParam.Value = value

        End Select
        Return objParam
    End Function

    Public Overloads Shared Function ExecuteScalar(ByVal connectionString As String, _
                                                  ByVal commandType As CommandType, _
                                                  ByVal commandText As String, _
                                                  ByVal ParamArray commandParameters() As IDataParameter) As Object
        Select Case adv.Business.DBAccessType
            Case adv.Business.modGeneral.enumDBAccessType.sql
                Dim arParms() As SqlParameter
                arParms = PrepareSqlParameter(commandParameters)
                Return SqlHelper.ExecuteScalar(connectionString, commandType, commandText, arParms)
            Case adv.Business.modGeneral.enumDBAccessType.oledb
                Dim arParms() As OleDbParameter
                arParms = PrepareOleDbParameter(commandParameters)
                Return OleDBHelper.ExecuteScalar(connectionString, commandType, commandText, arParms)
        End Select
    End Function ' ExecuteScalar

    Public Overloads Shared Function ExecuteNonQuery(ByVal connectionString As String, _
                                                     ByVal commandType As CommandType, _
                                                     ByVal commandText As String, _
                                                     ByVal ParamArray commandParameters() As IDataParameter) As Integer
        Select Case adv.Business.DBAccessType
            Case adv.Business.modGeneral.enumDBAccessType.sql

                Dim arParms() As SqlParameter
                arParms = PrepareSqlParameter(commandParameters)

                Return SqlHelper.ExecuteNonQuery(connectionString, commandType, commandText, arParms)
            Case adv.Business.modGeneral.enumDBAccessType.oledb

                Dim arParms() As OleDbParameter
                arParms = PrepareOleDbParameter(commandParameters)

                Return OleDBHelper.ExecuteNonQuery(connectionString, commandType, commandText, arParms)
        End Select

    End Function
    Private Overloads Shared Function PrepareSqlParameter(ByVal ParamArray commandParameters() As IDataParameter) As SqlParameter()
        Dim iloop As Integer
        Dim arParms() As SqlParameter = New SqlParameter(commandParameters.Length - 1) {}
        Try
            While iloop < arParms.Length
                arParms(iloop) = commandParameters(iloop)
                iloop = iloop + 1
            End While
            Return arParms
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Overloads Shared Function PrepareOleDbParameter(ByVal ParamArray commandParameters() As IDataParameter) As OleDbParameter()
        Dim iloop As Integer
        Dim arParms() As OleDbParameter = New OleDbParameter(commandParameters.Length - 1) {}
        Try

            While iloop < arParms.Length
                arParms(iloop) = commandParameters(iloop)
                iloop = iloop + 1
            End While
            Return arParms
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overloads Shared Function ExecuteDataset(ByVal connectionString As String, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String, _
                                                   ByVal ParamArray commandParameters() As IDataParameter) As DataSet
        Select Case adv.Business.DBAccessType
            Case adv.Business.modGeneral.enumDBAccessType.sql
                Dim arParms() As SqlParameter
                arParms = PrepareSqlParameter(commandParameters)
                Return SqlHelper.ExecuteDataset(adv.Business.modGeneral.ConnectionString, commandType, commandText, arParms)
            Case adv.Business.modGeneral.enumDBAccessType.oledb
                Dim arParms() As OleDbParameter
                arParms = PrepareOleDbParameter(commandParameters)
                Return OleDBHelper.ExecuteDataset(connectionString, commandType, commandText, arParms)
        End Select
    End Function

End Class
