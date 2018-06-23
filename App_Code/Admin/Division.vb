Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class Division
#Region "Khai báo"
        Private mNo_ As String = ""
        Private mName As String = ""
        Private mLastDateModify As String = ""
        Private mUserID As String = ""
        Private mPageNo_ As String = ""

        Property No_() As String
            Get
                Return mNo_
            End Get
            Set(ByVal value As String)
                mNo_ = value
            End Set
        End Property
        Property Name() As String
            Get
                Return mName
            End Get
            Set(ByVal value As String)
                mName = value
            End Set
        End Property
        Property LastDateModify() As String
            Get
                Return mLastDateModify
            End Get
            Set(ByVal value As String)
                mLastDateModify = value
            End Set
        End Property
        Property UserID() As String
            Get
                Return mUserID
            End Get
            Set(ByVal value As String)
                mUserID = value
            End Set
        End Property
        Property PageNo_() As String
            Get
                Return mPageNo_
            End Get
            Set(ByVal value As String)
                mPageNo_ = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal sNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Division] WHERE No_ = '" & sNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mLastDateModify = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mUserID = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mPageNo_ = GetValue(SqlReader, 4, typeOfColumn.GetString)
                    SqlReader.Close()
                    Return True
                Else
                    SqlReader.Close()
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function Save(ByVal sOldNo_ As String) As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(5) {}
            Try
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@Name", SqlDbType.NVarChar, ParameterDirection.Input, mName)
                arParams(2) = DBHelper.createParameter("@LastDateModify", SqlDbType.NVarChar, ParameterDirection.Input, mLastDateModify)
                arParams(3) = DBHelper.createParameter("@UserID", SqlDbType.NVarChar, ParameterDirection.Input, mUserID)
                arParams(4) = DBHelper.createParameter("@PageNo_", SqlDbType.NVarChar, ParameterDirection.Input, mPageNo_)
                arParams(5) = DBHelper.createParameter("@OldNo_", SqlDbType.NVarChar, ParameterDirection.Input, sOldNo_)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_Division", arParams)
                Return True
            Catch ex As Exception

                Return False
            End Try
        End Function
#End Region

    End Class
End Namespace