Imports System.Data
Imports Microsoft.VisualBasic

Namespace adv.Business
    Public Class RightForGroupUser
#Region "Khai báo"
        Private mUserGroupNo_ As String = ""
        Private mMenuNo_ As String = ""
        Private mViewRight As Integer = 0
        Private mAddRight As Integer = 0
        Private mEditRight As Integer = 0
        Private mDelRight As Integer = 0

        Property UserGroupNo_() As String
            Get
                Return mUserGroupNo_
            End Get
            Set(ByVal value As String)
                mUserGroupNo_ = value
            End Set
        End Property
        Property MenuNo_() As String
            Get
                Return mMenuNo_
            End Get
            Set(ByVal value As String)
                mMenuNo_ = value
            End Set
        End Property
        Property ViewRight() As Integer
            Get
                Return mViewRight
            End Get
            Set(ByVal value As Integer)
                mViewRight = value
            End Set
        End Property
        Property AddRight() As Integer
            Get
                Return mAddRight
            End Get
            Set(ByVal value As Integer)
                mAddRight = value
            End Set
        End Property
        Property EditRight() As Integer
            Get
                Return mEditRight
            End Get
            Set(ByVal value As Integer)
                mEditRight = value
            End Set
        End Property
        Property DelRight() As Integer
            Get
                Return mDelRight
            End Get
            Set(ByVal value As Integer)
                mDelRight = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal sGroupNo_ As String, ByVal sMenuNo_ As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [User Group Right] WHERE [User Group No_] = '" & sGroupNo_ & "' AND [Menu No_] = '" & sMenuNo_ & "'"
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mUserGroupNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mMenuNo_ = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mViewRight = GetValue(SqlReader, 2, typeOfColumn.GetInt32)
                    mAddRight = GetValue(SqlReader, 3, typeOfColumn.GetInt32)
                    mEditRight = GetValue(SqlReader, 4, typeOfColumn.GetInt32)
                    mDelRight = GetValue(SqlReader, 5, typeOfColumn.GetInt32)
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
        Public Function Save() As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(5) {}
            Try
                arParams(0) = DBHelper.createParameter("@UserGroupNo_", SqlDbType.NVarChar, ParameterDirection.Input, mUserGroupNo_)
                arParams(1) = DBHelper.createParameter("@MenuNo_", SqlDbType.NVarChar, ParameterDirection.Input, mMenuNo_)
                arParams(2) = DBHelper.createParameter("@ViewRight", SqlDbType.Int, ParameterDirection.Input, mViewRight)
                arParams(3) = DBHelper.createParameter("@AddRight", SqlDbType.Int, ParameterDirection.Input, mAddRight)
                arParams(4) = DBHelper.createParameter("@EditRight", SqlDbType.Int, ParameterDirection.Input, mEditRight)
                arParams(5) = DBHelper.createParameter("@DelRight", SqlDbType.Int, ParameterDirection.Input, mDelRight)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_UserGroupRight", arParams)
                Return True
            Catch ex As Exception

                Return False
            End Try
        End Function
#End Region

    End Class
End Namespace